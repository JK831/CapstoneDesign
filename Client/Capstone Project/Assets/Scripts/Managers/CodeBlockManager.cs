using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeBlockManager : I_CheckClear
{
    int _blockRestriction = 10;

    GameObject _be2ProgEnv = null;

    public int BlockRestriction
    {
        get { return _blockRestriction; }
        set { _blockRestriction = value; }
    }

    public GameObject BE2ProgEnv
    {
        get { return _be2ProgEnv; }
        set { _be2ProgEnv = value; }
    }

    public bool CheckCleared()
    {
        BE2_ExecutionManager e2_ExecutionManager = GameObject.Find("Blocks Engine 2 with function").transform.Find("BE2 Execution Manager").GetComponent<BE2_ExecutionManager>();
        int currentNOCodeBlocks; // = e2_ExecutionManager.totalNumOfBlocks;
        
        currentNOCodeBlocks = CheckChildCount(_be2ProgEnv.transform.Find("Speeder TO and PE").Find("Canvas Programming Env").Find("Scroll View").Find("Viewport")
            .Find("ProgrammingEnv").Find("HorizontalBlock Ins WhenPlayClicked").Find("Section0").Find("Body")); // 재귀함수에 의해 Block 추가된 것 -1*/

        //Debug.Log(currentNOCodeBlocks);

        return currentNOCodeBlocks <= _blockRestriction;
    }

    int CheckChildCount(Transform transform) // 자식의 개수 재귀 호출 통하여 파악
    {
        int childCount = 0;

        if (transform == null)
            return 1;

        for (int i = 0; i < transform.childCount; i++)
        {
            Transform childTransform = transform.GetChild(i);
            childTransform = childTransform.Find("Section0").Find("Body");
            if (childTransform)
                childCount += CheckChildCount(childTransform) + 1; // 자식의 개수 + 자신(1)
            else
                childCount += 1;
        }

        return childCount;
    }

    public void Clear()
    {
        _blockRestriction = 10;
        _be2ProgEnv = null;
    }
}
