using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_StudyClearPopup : UI_Finished
{
    public override void Init()
    {
        Managers.Music.audioSource.gameObject.SetActive(false);
        Managers.Music.PlaySE("You Win (5)");
    }
}
