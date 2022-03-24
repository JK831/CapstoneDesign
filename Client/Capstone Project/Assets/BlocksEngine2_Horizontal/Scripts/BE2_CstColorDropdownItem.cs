using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BE2_CstColorDropdownItem : MonoBehaviour
{
    Toggle _toggle;
    Text _label;
    Image _image;
    //public Image mainImage;

    //void OnEnable()
    //{
    //    if (!_toggle) Awake();
    //    _toggle.onValueChanged.AddListener(delegate
    //    {
    //        SetColor(_label.text);
    //    });
    //}
//
    //void OnDisable()
    //{
    //    _toggle.onValueChanged.RemoveAllListeners();
    //}

    void Awake()
    {
        _toggle = GetComponent<Toggle>();
        _label = transform.GetChild(3).GetComponent<Text>();
        _image = transform.GetChild(1).GetComponent<Image>();
    }

    void Start()
    {
        SetColor(_label.text);
    }

    void SetColor(string value)
    {
        Color newColor = Color.white;

        switch (value)
        {
            case "Random":
                ColorUtility.TryParseHtmlString("#FFFFFF", out newColor);
                break;
            case "Red":
                ColorUtility.TryParseHtmlString("#FF0000", out newColor);
                break;
            case "Orange":
                ColorUtility.TryParseHtmlString("#FF7F00", out newColor);
                break;
            case "Yellow":
                ColorUtility.TryParseHtmlString("#FFFF00", out newColor);
                break;
            case "Green":
                ColorUtility.TryParseHtmlString("#00FF00", out newColor);
                break;
            case "Blue":
                ColorUtility.TryParseHtmlString("#0000FF", out newColor);
                break;
            case "Indigo":
                ColorUtility.TryParseHtmlString("#2E2B5F", out newColor);
                break;
            case "Violet":
                ColorUtility.TryParseHtmlString("#8B00FF", out newColor);
                break;
            default:
                break;
        }

        _image.color = newColor;

        //if (_toggle.isOn)
        //{
        //    mainImage.color = newColor;
        //}
    }
}
