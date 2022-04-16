using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    private Slider volumeSlider_bgm = null;


    private void Start()
    {
        volumeSlider_bgm = this.gameObject.GetComponent<Slider>();


        if (volumeSlider_bgm != null)
            volumeSlider_bgm.value = Managers.Music.audioSource.volume;


    }

    public void SetMusicVolume(float volume)
    {
        Managers.Music.audioSource.volume = volume;
    }


    
}
