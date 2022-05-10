using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodingAreaManager
{
    [SerializeField]
    public Transform _mainAreaSaved { get; set; } = null;

    [SerializeField]
    public Transform _functionAreaSaved { get; set; } = null;


    public Transform _mainArea;
    public Transform _functionArea;


    public void Init()
    {
        GameObject g = GameObject.Find("ProgrammingEnv");

        _mainArea = g.transform.Find("HorizontalBlock Ins WhenPlayClicked").Find("Section0").Find("Body");

        _functionArea = g.transform.Find("FunctionArea").Find("Section0").Find("Body");
    }


    //저장된 코드들 게임에 넣기
    public void PutArea()
    {
        //Debug.Log($"{_mainAreaSaved == null}");
        if (_mainAreaSaved != null && _mainAreaSaved.childCount > 0)
        {
            //Debug.Log($"{_mainAreaSaved.childCount}");
            while (_mainAreaSaved.transform.childCount != 0)
            {
                Transform child = _mainAreaSaved.transform.GetChild(0);
                
                child.SetParent(_mainArea.transform, false);
            }
        }

        if (_functionAreaSaved != null && _functionAreaSaved.childCount > 0)
        {
            //Debug.Log($"{_functionAreaSaved.childCount}");
            while (_functionAreaSaved.transform.childCount != 0)
            {
                Transform child = _functionAreaSaved.transform.GetChild(0);
                child.SetParent(_functionArea.transform, false);
            }
        }

    }


    public void Clear()
    {
        if(_mainAreaSaved != null)
            Managers.Resource.Destroy(_mainAreaSaved.gameObject);
        if (_functionAreaSaved != null)
            Managers.Resource.Destroy(_functionAreaSaved.gameObject);

        _mainArea = null;
        _functionArea = null;
    }

}