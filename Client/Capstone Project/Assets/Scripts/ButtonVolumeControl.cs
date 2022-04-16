using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ButtonVolumeControl : MonoBehaviour
{
    private Slider volumeSlider_EffectSounds = null;

    private void Start()
    {
        volumeSlider_EffectSounds = this.gameObject.GetComponent<Slider>();

        if (volumeSlider_EffectSounds != null)
            volumeSlider_EffectSounds.value = Managers.Music.SEPlayer.volume;

    }

    public void SetSEVolume(float volume)
    {
        Managers.Music.SEPlayer.volume = volume;
    }

}
