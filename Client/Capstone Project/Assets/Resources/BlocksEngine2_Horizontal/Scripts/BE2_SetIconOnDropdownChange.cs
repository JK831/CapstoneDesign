using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BE2_SetIconOnDropdownChange : MonoBehaviour
{
    public Image image;
    public Dropdown dropdown;
    public Sprite spriteLeft;
    public Sprite spriteRight;

    void OnEnable()
    {
        dropdown.onValueChanged.AddListener(delegate
        {
            SetIcon();
        });
    }

    void OnDisable()
    {
        dropdown.onValueChanged.RemoveAllListeners();
    }

    void SetIcon()
    {
        string value = dropdown.options[dropdown.value].text;
        if(value == "Left")
        {
            image.sprite = spriteLeft;
        }
        else if(value == "Right")
        {
            image.sprite = spriteRight;
        }
    }

    //void Start()
    //{
    //
    //}
    //
    //void Update()
    //{
    //
    //}
}
