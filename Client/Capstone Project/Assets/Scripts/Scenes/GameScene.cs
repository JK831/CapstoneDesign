using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    //Coroutine co; // 코루틴은 함수의 상태를 저장/복원이 가능하다.(오래 걸리는 작업을 끊거나 원하는 타이밍에 함수를 잠시 stop/복원 가능)
    ////return을 우리가 원하는 타입으로 가능(class도 가능)

    //protected override void Init()
    //{
    //    base.Init();

    //    SceneType = Define.Scene.Game;

    //   Managers.UI.ShowSceneUI<UI_Inven>();

    //    for (int i = 0; i < 5; i++)
    //        Managers.Resource.Instantiate("UnityChan");

    //    co = StartCoroutine("ExplodeAfterSeconds", 4.0f);
    //    StartCoroutine("CoStopExplode", 2.0f);

    //}

    //IEnumerator CoStopExplode(float seconds)
    //{
    //    Debug.Log("Stop Enter");
    //    yield return new WaitForSeconds(seconds);
    //    Debug.Log("Stop Execute !");
    //    if (co != null)
    //    {
    //        StopCoroutine(co);
    //        co = null;
    //    }
    //}

    //IEnumerator ExplodeAfterSeconds(float seconds)
    //{
    //    Debug.Log("Explode Enter");
    //    yield return new WaitForSeconds(seconds);
    //    Debug.Log("Execute !");
    //    co = null;
    //}

    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Game;
        Managers.UI.ShowSceneUI<UI_Inven>();

        Dictionary<int, Stat> dict = Managers.Data.StatDict;
    }

    public override void Clear()
    {
        
    }
}
