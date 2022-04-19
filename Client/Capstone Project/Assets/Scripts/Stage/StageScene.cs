using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageScene : BaseScene
{
    GameObject be2ProgEnv = null;
    public bool IncludeFuntionBlock = false;
    protected override void Init()
    {
        base.Init();
        SceneType = Define.Scene.Game;
        bool success = Managers.Map.GenerateMap();
        success = Managers.Coin.GenerateCoin();

        if (!success) // 맵, 코인 둘 중 하나라도 생성 실패 시
            Managers.Scene.LoadScene(Define.Scene.Lobby);
        else
        {
            GameObject character = Managers.TargetObject.GetTargetObject("Character");
            Vector3 startPosition = character.transform.position;
            if (startPosition == null)
            {
                Debug.Log("StartPosition wasn't set");
                Managers.Scene.LoadScene(Define.Scene.Lobby);
            }

            if (IncludeFuntionBlock)
                be2ProgEnv = Managers.Resource.Instantiate("Blocks Engine 2 with function");
            else
                be2ProgEnv = Managers.Resource.Instantiate("Blocks Engine 2");

            Managers.CodeBlock.BE2ProgEnv = be2ProgEnv;
            if (be2ProgEnv == null)
            {
                Debug.Log("Wrong engine name");
                Managers.Scene.LoadScene(Define.Scene.Lobby);
            }
        }
    }
    public override void Clear()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
