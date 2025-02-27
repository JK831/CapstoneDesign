﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BE2_DragTrigger : MonoBehaviour, I_BE2_Drag
{
    BE2_DragDropManager _dragDropManager;
    RectTransform _rectTransform;
    I_BE2_BlocksStack _blocksStack;
    BE2_ExecutionManager _executionManager;

    Transform _transform;
    public Transform Transform => _transform ? _transform : transform;
    public Vector2 RayPoint => _rectTransform.position;
    public I_BE2_Block Block { get; set; }
    public List<I_BE2_Block> ChildBlocks { get; set; }

    void Awake()
    {
        _transform = transform;
        _rectTransform = GetComponent<RectTransform>();
        Block = GetComponent<I_BE2_Block>();
        _blocksStack = GetComponent<I_BE2_BlocksStack>();
    }

    void Start()
    {
        _dragDropManager = BE2_DragDropManager.instance;
        _executionManager = BE2_ExecutionManager.instance;
    }

    //void Update()
    //{
    //
    //}

    public void OnPointerDown()
    {
        _dragDropManager = BE2_DragDropManager.instance;

        // ---> maybe transfer this to OnDragStart()
        ChildBlocks = new List<I_BE2_Block>();
        ChildBlocks.AddRange(GetComponentsInChildren<I_BE2_Block>());
    }

    public void OnRightPointerDownOrHold()
    {
        BE2_UI_ContextMenuManager.instance.OpenContextMenu(0, Block);
    }

    public void OnDrag()
    {
        //if (Transform.parent != _dragDropManager.DraggedObjectsTransform)
        //    Transform.SetParent(_dragDropManager.DraggedObjectsTransform, true);
    }

    public void OnPointerUp()
    {
        I_BE2_Spot spot = _dragDropManager.Raycaster.GetSpotAtPosition(RayPoint);
        if (spot != null)
        {
            I_BE2_ProgrammingEnv programmingEnv = spot.Transform.GetComponentInParent<I_BE2_ProgrammingEnv>();
            if (programmingEnv == null && spot.Transform.GetChild(0) != null)
                programmingEnv = spot.Transform.GetChild(0).GetComponentInParent<I_BE2_ProgrammingEnv>();

            if (programmingEnv != null)
            {
                Transform.SetParent(programmingEnv.Transform);

                if (_blocksStack.MarkToAdd)
                {
                    _executionManager.AddToBlocksStackArray(Block.Instruction.InstructionBase.BlocksStack, programmingEnv.TargetObject);
                }
            }
            else
            {
                Destroy(Transform.gameObject);
            }
        }
        else
        {
            Destroy(Transform.gameObject);
        }
    }

    void OnDestroy()
    {
        //_executionManager.RemoveFromBlocksStackList(Block.Instruction);
    }
}
