using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BE2_BlocksStack : MonoBehaviour, I_BE2_BlocksStack
{
    int _arrayLength;
    bool _isActive = false;

    public int Pointer { get; set; }
    public I_BE2_Instruction[] InstructionsArray { get; set; }
    public I_BE2_TargetObject TargetObject { get; set; }
    public I_BE2_Instruction TriggerInstruction { get; set; }
    public bool markToAdd;
    public bool MarkToAdd { get => markToAdd; set => markToAdd = value; }
    public bool IsActive
    {
        get => _isActive;
        set
        {
            //if (!IsActive && value)
            //{
            //    int instructionsCount = InstructionsArray.Length;
            //    for (int i = 0; i < instructionsCount; i++)
            //    {
            //        InstructionsArray[i].InstructionBase.OnStackActive();
            //    }

            //    // activate all shadows
            //    foreach (I_BE2_Instruction instruction in InstructionsArray)
            //    {
            //        instruction.InstructionBase.Block.SetShadowActive(true);
            //    }
            //}
            //else if (IsActive && !value)
            //{
            //    // deactivate all shadows
            //    foreach (I_BE2_Instruction instruction in InstructionsArray)
            //    {
            //        instruction.InstructionBase.Block.SetShadowActive(false);
            //    }
            //}

            _isActive = value;
        }
    }

    void Awake()
    {
        MarkToAdd = false;
        TriggerInstruction = GetComponent<I_BE2_Instruction>();
        IsActive = false;
    }

    void Start()
    {
        PopulateStack();
        BE2_MainEventsManager.Instance.StartListening(BE2EventTypes.OnPointerUpEnd, PopulateStack);
        BE2_MainEventsManager.Instance.StartListening(BE2EventTypes.OnStop, StopStack);
    }

    void OnDisable()
    {
        BE2_MainEventsManager.Instance.StopListening(BE2EventTypes.OnPointerUpEnd, PopulateStack);
        BE2_MainEventsManager.Instance.StopListening(BE2EventTypes.OnStop, StopStack);
        BE2_ExecutionManager.instance.RemoveFromBlocksStackList(this);
    }

    void StopStack()
    {
        Pointer = 0;
        IsActive = false;
    }

    //void Update()
    //{
    //    Debug.Log($"length: {InstructionsArray.Length}");
    //    for (int i = 0; i < InstructionsArray.Length; i++)
    //    {
    //        Debug.Log($"{i}: {InstructionsArray[i]}");
    //    }



    //}

    public int OverflowGuard { get; set; }
    int _overflowLimit = 100;

    // v2.4 - Execute method of Blocks Stack refactored 
    public void Execute()
    {
        if (IsActive && _arrayLength > Pointer)
        {
            if (Pointer == 0)
            {
                I_BE2_Block firstBlock = TriggerInstruction.InstructionBase.Block;
                BE2_MainEventsManager.Instance.TriggerEvent(BE2EventTypesBlock.OnStackExecutionStart, firstBlock);
            }

            InstructionsArray[Pointer].Function();
            OverflowGuard = 0;
        }

        if (InstructionsArray != null && Pointer == InstructionsArray.Length && InstructionsArray.Length > 0)
        {
            I_BE2_Block lastBlock = InstructionsArray[InstructionsArray.Length - 1].InstructionBase.Block;
            BE2_MainEventsManager.Instance.TriggerEvent(BE2EventTypesBlock.OnStackExecutionEnd, lastBlock);

            Pointer = 0;
            IsActive = false;
        }
    }



    public void PopulateStack()
    {
        functionAreaUpdate();

        InstructionsArray = new I_BE2_Instruction[0];
        PopulateStackRecursive(TriggerInstruction.InstructionBase.Block);
        _arrayLength = InstructionsArray.Length;


        //Debug.Log($"length: {InstructionsArray.Length}");
        //for (int i = 0; i < InstructionsArray.Length; i++)
        //{
        //    Debug.Log($"{i}: {InstructionsArray[i]}");
        //}
    }

    void functionAreaUpdate()
    {
        //ProgrammingEnv안의 ins_function들을 찾음
        GameObject[] function_blocks = GameObject.FindGameObjectsWithTag("FunctionBlock");

        //못 찾으면 패스
        //있으면 모든 if_function의 body에다가 functionarea의 body를 복사해서 넣
        if (function_blocks.Length != 0)
        {

            //functionArea의 body를 불러옴
            GameObject function_area_body = GameObject.Find("FunctionArea").transform.Find("Section0").Find("Body").gameObject;
            //function_area_body = function_area_body.transform.Find("Body").gameObject;


            //functionArea에 function블록이 재귀적으로 들어가지 못하도록 제거
            foreach (Transform ins in function_area_body.GetComponentsInChildren<Transform>())
            {
                if (ins.name.Equals("HorizontalBlock Ins Function"))
                    Managers.Resource.Destroy(ins.gameObject);
            }




            //모든 function 블록에 불러온 functionArea의 body를 넣어줌
            GameObject function_area_body_copy;
            //Debug.Log($"area childCount {AllChildrenCount(function_area_body)}");
            //Debug.Log($"block childCount {AllChildrenCount(function_blocks[function_blocks.Length - 1])}");
            //Debug.Log(AllChildrenCount(function_area_body) != AllChildrenCount(function_blocks[function_blocks.Length - 1]));
            if (AllInsCount(function_area_body) != AllInsCount(function_blocks[function_blocks.Length - 1]))
            {
                //Debug.Log($"function_blocks[0] {function_blocks[0].transform.childCount}");
                //Debug.Log($"function_area_body {function_area_body.transform.childCount}");

                for (int i = 0; i < function_blocks.Length; i++)
                {

                    function_area_body_copy = Instantiate(function_area_body);
                    foreach (Transform child in function_blocks[i].transform)
                    {
                        Managers.Resource.Destroy(child.gameObject);
                    }


                    while (function_area_body_copy.transform.childCount != 0)
                    {

                        Transform child = function_area_body_copy.transform.GetChild(0);
                        child.SetParent(function_blocks[i].transform, false);

                        //body에 들어간 블록 제거
                        child.transform.localScale = new Vector3(0, 0, 0);

                    }
                    Managers.Resource.Destroy(function_area_body_copy);
                }
            }
        }
    }
    int AllInsCount(GameObject g)
    {
        return g.GetComponentsInChildren<BE2_Block>().Length;
    }



    void PopulateStackRecursive(I_BE2_Block parentBlock)
    {
        int locationsCount = 0;

        I_BE2_Instruction parentInstruction = parentBlock.Instruction;
        I_BE2_InstructionBase parentInstructionBase = parentInstruction as I_BE2_InstructionBase;
        parentInstructionBase.TargetObject = TargetObject;
        parentInstructionBase.BlocksStack = this;

        // ---> add to layout number of bodies and use this number to create the locations array
        I_BE2_BlockSection[] tempSectionsArr = parentInstructionBase.Block.Layout.SectionsArray;
        parentInstructionBase.LocationsArray = new int[
            BE2_ArrayUtils.FindAll(ref tempSectionsArr, (x => x.Body != null)).Length + 1];

        InstructionsArray = BE2_ArrayUtils.AddReturn(InstructionsArray, parentInstruction);

        int sectionsCount = parentBlock.Layout.SectionsArray.Length;
        for (int i = 0; i < sectionsCount; i++)
        {
            I_BE2_BlockSection section = parentBlock.Layout.SectionsArray[i];
            if (section.Body != null)
            {

                if (parentBlock.Type != BlockTypeEnum.trigger)
                {
                    parentInstructionBase.LocationsArray[locationsCount] = InstructionsArray.Length;

                    locationsCount++;
                }
                else
                {
                    parentInstructionBase.LocationsArray[locationsCount] = 1;
                }

                section.Body.UpdateChildBlocksList();
                I_BE2_Block[] childBlocks = section.Body.ChildBlocksArray;

                int childBlocksCount = childBlocks.Length;


                for (int j = 0; j < childBlocksCount; j++)
                {
                    PopulateStackRecursive(childBlocks[j]);
                }

                if (parentBlock.Type != BlockTypeEnum.trigger)
                {
                    InstructionsArray = BE2_ArrayUtils.AddReturn(InstructionsArray, parentInstruction);
                }
            }
        }

        if (parentBlock.Type != BlockTypeEnum.trigger)
            parentInstructionBase.LocationsArray[locationsCount] = InstructionsArray.Length;

        parentInstructionBase.PrepareToPlay();
    }
}
