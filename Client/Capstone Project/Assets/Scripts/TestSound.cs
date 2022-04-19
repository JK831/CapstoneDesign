using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSound : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public AudioClip audioClip;
    public AudioClip audioClip2;

    int i = 0;
    private void OnTriggerEnter(Collider other)
    {
        //AudioSource audio = GetComponent<AudioSource>();
        //audio.PlayClipAtPoint();
        //audio.PlayOneShot(audioClip);

        //GameObject.Destroy(gameObject, 0.25f);

        i++;
        if (i % 2 == 0)
            Managers.Sound.Play(audioClip, Define.Sound.Bgm);
        else
            Managers.Sound.Play(audioClip2, Define.Sound.Bgm);
    }
}
