using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButtonEvents : MonoBehaviour
{

    [SerializeField]
    private Transform _section = null;


    public void PlayEvents()
    {
        _section = GameObject.Find("HorizontalBlock Ins WhenPlayClicked").transform;
        _section = _section.GetChild(0).GetChild(1);

        
        //실행 시킬게 없다면 패스 
        if(_section.childCount != 0)
        {


            //카메라 전환
            GameObject.Find("QuaterView Camera").GetComponent<CameraController>().ChangeToQuarterView();


            //블록 선택 영역 제거
            GameObject.Find("Canvas Blocks Selection").GetComponent<Canvas>().enabled = false;


            //실행
            BE2_ExecutionManager e2ExecutionManager = GameObject.Find("BE2 Execution Manager").GetComponent<BE2_ExecutionManager>();
            e2ExecutionManager.totalNumOfBlocks = GameObject.Find("HorizontalBlock Ins WhenPlayClicked").GetComponent<BE2_BlocksStack>().InstructionsArray.Length; // 실행 시점의 블록 개수 저장
            //Debug.Log(e2ExecutionManager.totalNumOfBlocks);
            e2ExecutionManager.PlayAfterDelay();


            //시작 버튼 제거
            gameObject.SetActive(false);

        }




    }

}
