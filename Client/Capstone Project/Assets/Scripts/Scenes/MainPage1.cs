using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPage1 : MonoBehaviour
{
    public AudioClip audioSourceBgm;

    //public AudioSource audioSourceEffects;
    // Start is called before the first frame update
    void Start()
    {
        Managers.Music.SetBackGroundMusic("Casual Game Music 02 - Oriental");
        Managers.Music.MusicPlayer.Play();
        //audioSourceEffects = Managers.Music.GetSoundEffect("Pop(1)");

        //audioSourceEffects.volume = Managers.Music.GetButtonVolume();


    }

    // Update is called once per frame
    void Update()
    {

    }
}
