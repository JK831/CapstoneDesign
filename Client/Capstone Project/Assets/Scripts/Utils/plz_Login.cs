using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plz_Login : MonoBehaviour
{
    // Start is called before the first frame update
    public void plzlogin()
    { 
        Transform a = GameObject.Find("MainCanvas").transform;
        //a.Find("Study_Challenge_Select/Scroll View/Plz_Login");

        a = a.transform.Find("Study_Challenge_Select");
        Transform c = a;
        a = a.transform.Find("Scroll View");
        a = a.transform.Find("Plz_Login");

        Transform b = GameObject.Find("MainCanvas").transform;
        b = b.transform.Find("ChallengeStage_Select");
        
        if (Social.localUser.authenticated)
        {
            c.gameObject.SetActive(false);
            b.gameObject.SetActive(true);
        }
        else
            Social.localUser.Authenticate((bool success) =>
            {
                if (success)
                {
                    c.gameObject.SetActive(false);
                    b.gameObject.SetActive(true);

                }
                else
                {
                    a.gameObject.SetActive(true);


                }
            });
    }
}
