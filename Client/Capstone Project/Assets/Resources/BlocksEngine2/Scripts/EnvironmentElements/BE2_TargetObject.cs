using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class BE2_TargetObject : MonoBehaviour, I_BE2_TargetObject
{
    enum Direction
    {
        up = 0,
        right = 1,
        down = 2,
        left = 3,
        size = 4,

    }

    enum CharacterState
    {
        Idle = 0,
        Moving = 1,
        Turn = 2,

    }

    CharacterState _state;

    int currentPositionInMap;

    public int CurrentPositionInMap
    {
        get { return currentPositionInMap; }
        set { currentPositionInMap = value; }
    }

    int _velocity = 1;
    bool interactable = true;
    GameObject currentBlock = null;

    public GameObject CurrentBlock
    {
        get { return currentBlock; }
        set { currentBlock = value; }
    }


    int currentDirection = (int)Direction.up;

    public Transform Transform => transform;
    public I_BE2_ProgrammingEnv ProgrammingEnv { get; set; }

    Animator anim;

    Queue<Vector3> TargetQueue = new Queue<Vector3>();
    Vector3 _target;

    void Start()
    {
        _target = this.gameObject.transform.position;
        _state = CharacterState.Idle;
        anim = GetComponent<Animator>();
        startAnimation(_state);
    }
    
    void Update()
    {
        moveToTarget();
    }

    void startAnimation(CharacterState state)
    {
        switch (state)
        {

            case CharacterState.Idle:
                //idle
                anim.SetInteger("animation", 1);
                break;

            case CharacterState.Moving:
                //run
                anim.SetInteger("animation", 12);
                break;

            case CharacterState.Turn:
                break;
        }
    }

    //캐릭터가 이동하는 모습을 위한 메서드
    void moveToTarget()
    {
        Vector3 dir = _target - this.gameObject.transform.position;

        if (dir.magnitude < 0.001f)
        {
            _state = CharacterState.Idle;
            startAnimation(_state);

            if(TargetQueue.Count > 0)
                _target = TargetQueue.Dequeue();
        }
        else
        {
            _state = CharacterState.Moving;
            startAnimation(_state);
            this.gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, _target, 0.1f);
            this.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 20 * Time.deltaTime);
        }

        
    }

    [SerializeField]
    float _speed = 10.0f;
    float _counter = 1.0f;





    void I_BE2_TargetObject.Move()
    {
        GameObject newBlock = null;
        int blockId = Managers.Map.Move(currentDirection, _velocity);
        if (blockId != currentBlock.GetComponent<Block>().BlockId && Managers.Map.GetMap().TryGetValue(blockId, out newBlock))
        {
            if (newBlock == null)
                return;


            Vector3 targetPostion = newBlock.transform.position + new Vector3(0, 0.9f, 0);
            TargetQueue.Enqueue(targetPostion);

            Vector3 dir = targetPostion - this.gameObject.transform.position;

            currentBlock = newBlock;
        }

    }

    void I_BE2_TargetObject.Turn(bool clockWise)
    {
        if (clockWise)
        {



            currentDirection = (currentDirection + 1) % (int)Direction.size;
        }
        else
        {

            currentDirection = (currentDirection + (int)Direction.size - 1) % (int)Direction.size;
        }
    }





    bool I_BE2_TargetObject.AbleRight()
    {
        GameObject newBlock = null;
        int blockId = Managers.Map.MoveCheck((currentDirection + 1) % (int)Direction.size, _velocity);
        if (blockId != currentBlock.GetComponent<Block>().BlockId && Managers.Map.GetMap().TryGetValue(blockId, out newBlock))
        {
            if (newBlock == null)
            {
                //Debug.Log("오른쪽 불가능1");
                return false;

            }

            //Debug.Log("오른쪽 가능");
            return true;
        }

        //Debug.Log("오른쪽 불가능2");
        return false;
    }

    bool I_BE2_TargetObject.AbleLeft()
    {
        GameObject newBlock = null;
        int blockId = Managers.Map.MoveCheck((currentDirection - 1) % (int)Direction.size, _velocity);
        if (blockId != currentBlock.GetComponent<Block>().BlockId && Managers.Map.GetMap().TryGetValue(blockId, out newBlock))
        {
            if (newBlock == null)
            {
                //Debug.Log("왼쪽 불가능1");
                return false;

            }

            //Debug.Log("왼쪽 가능");
            return true;
        }

        //Debug.Log("왼쪽 불가능2");
        return false;
    }

    bool I_BE2_TargetObject.AbleForward()
    {
        GameObject newBlock = null;
        int blockId = Managers.Map.MoveCheck(currentDirection, _velocity);
        if (blockId != currentBlock.GetComponent<Block>().BlockId && Managers.Map.GetMap().TryGetValue(blockId, out newBlock))
        {
            if (newBlock == null)
            {
                //Debug.Log("전진 불가능1");
                return false;

            }

            //Debug.Log("전진 가능");
            return true;
        }

        //Debug.Log("전진 불가능2");
        return false;
    }

}
