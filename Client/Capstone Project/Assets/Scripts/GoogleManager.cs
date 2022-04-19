using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using UnityEngine.UI;

public class GoogleManager : MonoBehaviour
{
    public Text LoginLogText;


    // Start is called before the first frame update
    void Start()
    {
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
    }

    void LogIn()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
