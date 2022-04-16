using System.Collections;
using System.Collections.Generic;
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

    enum State
    {
        Idle = 0,
        Moving = 1,

    }

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

    void I_BE2_TargetObject.Move()
    {
        GameObject newBlock = null;
        int blockId = Managers.Map.Move(currentDirection, _velocity);
        if (blockId != currentBlock.GetComponent<Block>().BlockId && Managers.Map.GetMap().TryGetValue(blockId, out newBlock))
        {
            if (newBlock == null)
                return;

            this.gameObject.transform.position = newBlock.transform.position + new Vector3(0, 0.9f, 0);
            currentBlock = newBlock;
        }

    }

    void I_BE2_TargetObject.Turn(bool clockWise)
    {
        if (clockWise)
        {
            currentDirection = (currentDirection + 1) % (int)Direction.size;
            this.gameObject.transform.forward = this.gameObject.transform.right;
        }
        else
        {
            currentDirection = (currentDirection + (int)Direction.size - 1) % (int)Direction.size;
            this.gameObject.transform.forward = -this.gameObject.transform.right;
        }
    }

}
