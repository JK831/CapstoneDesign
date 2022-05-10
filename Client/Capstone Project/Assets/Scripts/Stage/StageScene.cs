using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageScene : BaseScene
{
    GameObject be2ProgEnv = null;

    protected override void Init()
    {
        base.Init();
        SceneType = Define.Scene.Game;
        bool success = Managers.Map.GenerateMap();
        success &= Managers.MapObject.GenerateObject();
        

        if (!success) // 맵, 코인 둘 중 하나라도 생성 실패 시
        {
            SceneManager.LoadScene("MainPage");
            StageManager.ToMain = true;
        }
        else
        {
            GameObject character = Managers.TargetObject.GetTargetObject("Character");
            Vector3 startPosition = character.transform.position;
            if (startPosition == null)
            {
                Debug.Log("StartPosition wasn't set");
                Managers.Scene.LoadScene(Define.Scene.Lobby);
            }

            be2ProgEnv = Managers.Resource.Instantiate("Blocks Engine 2 with function");

            Managers.CodeBlock.BE2ProgEnv = be2ProgEnv;
            if (be2ProgEnv == null)
            {
                Debug.Log("Wrong engine name");
                SceneManager.LoadScene("MainPage");
                StageManager.ToMain = true;
            }

            else
            {
                Transform quarterViewCamera = be2ProgEnv.transform.Find("QuaterView Camera");
                if (quarterViewCamera != null)
                    quarterViewCamera.GetComponent<CameraController>().Player = character;


                Managers.CodingArea.Init();
                Managers.CodingArea.PutArea();

                if (Managers.CodingArea._mainAreaSaved != null)
                    Destroy(Managers.CodingArea._mainAreaSaved.gameObject);
                if (Managers.CodingArea._functionAreaSaved != null)
                    Destroy(Managers.CodingArea._functionAreaSaved.gameObject);

                RefreshCodingArea(Managers.CodingArea._mainArea);
                RefreshCodingArea(Managers.CodingArea._functionArea);

            }

            Managers.Stage.ConditionSet();

            string sceneName = SceneManager.GetActiveScene().name;
            if (!sceneName.Contains("Challenge"))
            {
                GameObject.Find("Blocks Engine 2 with function").transform.Find("Canvas Control Buttons")
                    .Find("Button Mission").gameObject.SetActive(false);
            }
            else
            {
                GameObject BlockEngine = GameObject.Find("Blocks Engine 2 with function");
                //BlockEngine.transform.Find("Speeder TO and PE").gameObject.SetActive(false);
                //BlockEngine.transform.Find("Canvas Selections").gameObject.SetActive(false);
                //BlockEngine.transform.Find("Canvas Blocks Selection").gameObject.SetActive(false);
                Transform missionUI = BlockEngine.transform.Find("Stage_ClearMission");
                //missionUI.gameObject.SetActive(true);
                Text missionBlockText = missionUI.Find("bg_black").Find("bg_window").Find("GO (1)").Find("Text").GetComponent<Text>();

                Define.StageBlock stageBlocks;
                if (Enum.TryParse(sceneName, out stageBlocks))
                {
                    missionBlockText.text = $"블럭의 개수를 {(int)stageBlocks}개 이하로\n 사용하세요!";
                }
                    

            }
        }


    }

    void RefreshCodingArea(Transform codingBody)
    {

        //Debug.Log("refresh called");
        int count = codingBody.childCount;
        if (count != 0)
        {
            GameObject[] copyObject = new GameObject[count];

            //Debug.Log("${mainBody.childCount}");

            for (int i = 0; i < count; i++)
            {
                GameObject go = codingBody.GetChild(i).gameObject;
                copyObject[i] = GameObject.Instantiate(go);
                copyObject[i].name = go.name;
                Managers.Resource.Destroy(go);
            }

            for (int i = 0; i < count; i++)
            {
                copyObject[i].transform.SetParent(codingBody, false);
            }
        }

    }
    public override void Clear()
    {

    }

}
