using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 1. 위치 벡터
// 2. 방향 벡터

class Tank
{
    public float speed = 10.0f;
}


public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float _speed = 10.0f;

    Vector3 _destPos;


    public enum PlayerState
    {
        Die,
        Moving,
        Idle
    }

    PlayerState _state = PlayerState.Idle;
    void Start()
    {
        Managers.Input.MouseAction -= OnMouseClicked;
        Managers.Input.MouseAction += OnMouseClicked;


    }

    void UpdateDie()
    {

    }

    void UpdateMoving()
    {
        Vector3 dir = _destPos - transform.position;
        if (dir.magnitude < 0.0001f)
        {
            _state = PlayerState.Idle;
        }
        else
        {
            float moveDist = Mathf.Clamp(_speed * Time.deltaTime, 0, dir.magnitude);
            //이동할 거리를 미리 최솟값, 최댓값 사이로 정해준다.

            transform.position += dir.normalized * moveDist;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 20 * Time.deltaTime);
        }
        //애니메이션
        Animator anim = GetComponent<Animator>();
        //현재 게임 상태에 대한 정보를 넘겨준다
        anim.SetFloat("speed", _speed);
    }


    void UpdateIdle()
    {
        //애니메이션
        Animator anim = GetComponent<Animator>();

        anim.SetFloat("speed", 0);
    }

    //GameObject (Player)
    //Transform
    //PlayerController (8)
    float _yAngle = 0.0f;
    void Update()
    {
        
      

        switch(_state)
        {
            case PlayerState.Die:
                UpdateDie();
                break;
            case PlayerState.Moving:
                UpdateMoving();
                break;
            case PlayerState.Idle:
                UpdateIdle();
                break;
        }

        
    }

    void OnMouseClicked(Define.MouseEvent evt)
    {
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(Camera.main.transform.position, ray.direction * 100.0f, Color.red, 1.0f);

        
        //int mask = (1 << 8) | (1 << 9);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100.0f, LayerMask.GetMask("Wall")))
        {
            _destPos = hit.point; //월드좌표 기준 어떤 좌표인지 저장
            _state = PlayerState.Moving;
            //Debug.Log($"Raycast Camera @ {hit.collider.gameObject.tag}");
        }
    }

    /*void OnKeyboard()
    {
        _yAngle += Time.deltaTime * 100.0f;
        // 절대 회전값
        // transform.eulerAngles = new Vector3(0.0f, _yAngle, 0.0f);

        // +- delta
        //transform.Rotate(new Vector3(0.0f, Time.deltaTime * 100.0f, 0.0f));

        //transform.rotation = Quaternion.Euler(new Vector3(0.0f, _yAngle, 0.0f));


        if (Input.GetKey(KeyCode.W))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), 0.5f);
            //transform.Translate(Vector3.forward * Time.deltaTime * _speed);
            transform.position += Vector3.forward * Time.deltaTime * _speed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.back), 0.5f);
            //transform.Translate(Vector3.forward * Time.deltaTime * _speed);
            transform.position += Vector3.back * Time.deltaTime * _speed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.left), 0.5f);
            //transform.Translate(Vector3.forward * Time.deltaTime * _speed);
            transform.position += Vector3.left * Time.deltaTime * _speed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.right), 0.5f);
            //transform.Translate(Vector3.forward * Time.deltaTime * _speed);
            transform.position += Vector3.right * Time.deltaTime * _speed;
        }
        //transform
        _moveToDest = false;
    }
    */
}
