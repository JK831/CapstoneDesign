using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_FailedPopup : UI_Finished
{
    // Start is called before the first frame update
    

    public override void Init()
    {
        Managers.Music.audioSource.gameObject.SetActive(false);
        Managers.Music.PlaySE("You Lose (6)");
    }

}


