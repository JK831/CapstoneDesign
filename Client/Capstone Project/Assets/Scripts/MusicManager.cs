using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager
{
    public AudioSource audioSourceEffects;

    public AudioSource audioSource;
    public AudioSource buttonPlayer;
    public float bgmVolume;
    public float buttonVolume;

    public AudioSource MusicPlayer
    {
        get { return audioSource; }
        set { audioSource = value; }
    }

    public AudioSource SEPlayer
    {
        get { return buttonPlayer; }
        set { buttonPlayer = value; }
    }
    /*
    public void PlaySE()
    {
        audioSourceEffects.Play();
    }*/
    public void Init()
    {
        GameObject root = GameObject.Find("@MusicManager");
        if (root == null)
        {
            root = new GameObject() { name = "@MusicManager" };
            
        }
        Object.DontDestroyOnLoad(root);
        GameObject go = new GameObject { name = "bgm" };
        audioSource = go.AddComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.volume = 0.5f;
        go.transform.parent = root.transform;

        GameObject buttonManager = new GameObject { name = "se" };
        buttonPlayer = buttonManager.AddComponent<AudioSource>();
        buttonPlayer.volume = 0.5f;
        buttonPlayer.playOnAwake = false;
        buttonManager.transform.parent = root.transform;



    }
   // public AudioSource audioSourceEffects;

    /*
    public void PlaySE()
    {
        audioSourceEffects.Play();
    }
    */
    /*public void PlayBGM()
    {
        audioSourceBgm.Play();
    }*/

   /* public void SetButtonVolume(float volume)
    {
        audioSourceEffects.volume = volume;
    } */

    
    /*
    public float GetButtonVolume()
    {
        return audioSourceEffects.volume;
    }
    */
    public void SetBackGroundMusic(string bgmName)
    {
        audioSource.clip = Resources.Load<AudioClip>($"Casual Music Pack/Music Loops/{bgmName}");
    }

    
    /*
    public AudioSource GetSoundEffect(string effectName)
    {
        return Resources.Load($"Interface and Item Sounds/Interface/Pops/{effectName}") as AudioSource;
    }
    */

    public void PlaySE(string SEName)
    {
        buttonPlayer.PlayOneShot(Resources.Load<AudioClip>($"Interface and Item Sounds/Interface/Pops/{SEName}"));
    }

}
