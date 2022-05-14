using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ClearPopup : UI_Finished
{
    int acquiredStars;
    
    public override void Init()
    {

        Managers.Music.audioSource.gameObject.SetActive(false);
        Managers.Music.PlaySE("You Win (5)");

        acquiredStars = Managers.Stage.CompletedConditionList.Count;
        Transform bg_window = gameObject.transform.Find("ChallegeStage_Complete").Find("bg_window");

        for (int i = 0; i < acquiredStars; i++)
        {
            string condition = null;

            if (Managers.Stage.CompletedConditionList[i].Contains("Code"))
            {
                bg_window.Find("Block_Check_fail").gameObject.SetActive(false);
                bg_window.Find("Block_Check_Success").gameObject.SetActive(true);
                condition = "CodeBlock";
            }
            else if (Managers.Stage.CompletedConditionList[i].Contains("Coin"))
            {
                bg_window.Find("Coin_Check_fail").gameObject.SetActive(false);
                bg_window.Find("Coin_Check_Success").gameObject.SetActive(true);
                condition = "Coin";
            }
            else
            {
                bg_window.Find("Destination_Check_fail").gameObject.SetActive(false);
                bg_window.Find("Destination_Check_Success").gameObject.SetActive(true);
                condition = "Destination";
            }

            bg_window.Find($"Toggle_star_{condition}").GetComponent<Toggle>().isOn = true;
        }

    }
   
    //public override void Init()
    //{
    //    base.Init();
    //    acquiredStars = Managers.Stage.CompletedConditionList.Count;
    //}
}
