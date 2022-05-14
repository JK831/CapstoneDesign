using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_2 : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySE()
    {
        Managers.Music.PlaySE("Pop (2)");
    }

    public void PlaySE2()
    {
        Managers.Music.PlaySE("Special & Powerup (5)");
    }
}
