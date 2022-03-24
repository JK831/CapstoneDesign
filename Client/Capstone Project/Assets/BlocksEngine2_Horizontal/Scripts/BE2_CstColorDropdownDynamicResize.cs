using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class BE2_CstColorDropdownDynamicResize : MonoBehaviour
{
    RectTransform _rectTransform;
    Dropdown _dropdown;
    float _minWidth = 70;
    float _offset = 35;

    Image _mainImage;

    void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _dropdown = GetComponent<Dropdown>();
        _mainImage = transform.GetChild(1).GetComponent<Image>();
    }

    void Start()
    {
        Resize();
        SetColor(_dropdown.options[_dropdown.value].text);
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

        _mainImage.color = newColor;
    }

    //#if UNITY_EDITOR
    //    void Update()
    //    {
    //        if (!EditorApplication.isPlaying)
    //        {
    //            Resize();
    //        }
    //    }
    //#endif

    void OnEnable()
    {
        _dropdown.onValueChanged.AddListener(delegate { Start(); });
    }

    void OnDisable()
    {
        _dropdown.onValueChanged.RemoveAllListeners();
    }

    public void Resize()
    {
        if (_dropdown)
        {
            float width = _offset + _dropdown.captionText.preferredWidth;
            if (width < _minWidth)
                width = _minWidth;

            _rectTransform.sizeDelta = new Vector2(60, _rectTransform.sizeDelta.y);
        }
    }

    bool lateStart = true;
    void LateUpdate()
    {
        if (lateStart)
        {
            Resize();
            lateStart = false;
        }
    }
}
