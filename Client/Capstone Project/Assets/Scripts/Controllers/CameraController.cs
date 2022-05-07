using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    Define.CameraMode _mode = Define.CameraMode.OverView;
    [SerializeField]
    private Vector3 _delta = new Vector3(0.0f, 8.0f, -6.0f);
    [SerializeField]
    private GameObject _player = null;



    private Camera overViewCamera;
    private Camera quarterViewCamera;

    public GameObject Player
    {
        get { return _player; }
        set { _player = value;  }
    }

    public Define.CameraMode Mode
    {
        get { return _mode; }
        set { _mode = value; }
    }
    void Start()
    {
        overViewCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        quarterViewCamera = gameObject.GetComponent<Camera>();
        _mode = Define.CameraMode.OverView;



    }

    void LateUpdate()
    {
        if (_mode == Define.CameraMode.QuarterView)
        {
            overViewCamera.enabled = false;
            quarterViewCamera.enabled = true;

            transform.position = _player.transform.position + _delta;
            transform.LookAt(_player.transform);

        }

        else
        {
            quarterViewCamera.enabled = false;
            overViewCamera.enabled = true;
        }
    }


    //토글형식
    public void ChangeMode()
    {
        if (_mode == Define.CameraMode.QuarterView)
            _mode = Define.CameraMode.OverView;
        else
            _mode = Define.CameraMode.QuarterView;
    }

    public void ChangeToQuarterView()
    {
        _mode = Define.CameraMode.QuarterView;
    }

    public void ChangeToOverView()
    {
        _mode = Define.CameraMode.OverView;
    }

    public void ChangeToOverViewWithDelay(float time = 0.75f)
    {
        Invoke("ChangeToOverView", time);
    }

}
