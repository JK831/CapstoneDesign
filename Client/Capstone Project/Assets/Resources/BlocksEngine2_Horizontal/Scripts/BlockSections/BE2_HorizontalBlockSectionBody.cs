using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class BE2_HorizontalBlockSectionBody : MonoBehaviour, I_BE2_BlockSectionBody
{
    RectTransform _rectTransform;
    I_BE2_BlockSection _section;
    I_BE2_BlockLayout _blockLayout;
    Image _image;
    public RectTransform RectTransform => _rectTransform;
    public I_BE2_Block[] ChildBlocksArray { get; set; }
    public I_BE2_BlockSection BlockSection { get; set; }
    public Vector2 Size
    {
        get
        {
            return _rectTransform.sizeDelta;
        }
        set
        {
            _rectTransform.sizeDelta = value;
        }
    }
    public I_BE2_Spot Spot { get; set; }
    public int ChildBlocksCount { get; set; }
    Shadow _shadow;
    public Shadow Shadow
    {
        get
        {
            if (!_shadow)
            {
                if (GetComponent<Shadow>())
                    _shadow = GetComponent<Shadow>();
                else
                    _shadow = gameObject.AddComponent<Shadow>();

                _shadow.effectColor = Color.green;
                _shadow.effectDistance = new Vector2(-6, -6);
            }

            return _shadow;
        }
    }

    void OnValidate()
    {
        Awake();
    }

    void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();

        if (transform.parent)
        {
            _section = transform.parent.GetComponent<I_BE2_BlockSection>();
            _blockLayout = transform.parent.parent.GetComponent<I_BE2_BlockLayout>();
            BlockSection = transform.parent.GetComponent<I_BE2_BlockSection>();
        }

        _image = GetComponent<Image>();
        _image.type = Image.Type.Sliced;
        _image.pixelsPerUnitMultiplier = 2;

        ChildBlocksArray = new I_BE2_Block[0];
        Spot = GetComponent<I_BE2_Spot>();
    }

    //void Start()
    //{
    //
    //}

    //void Update()
    //{
    //    //UpdateLayout();
    //}

    public void UpdateChildBlocksList()
    {
        ChildBlocksArray = new I_BE2_Block[0];
        int childCount = transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            I_BE2_Block childBlock = transform.GetChild(i).GetComponent<I_BE2_Block>();
            if (childBlock != null)
            {
                ChildBlocksArray = BE2_ArrayUtils.AddReturn(ChildBlocksArray, childBlock);
            }
        }
        ChildBlocksCount = ChildBlocksArray.Length;
    }

    public void UpdateLayout()
    {
        if (_image.sprite != null && _blockLayout != null)
            _image.color = _blockLayout.Color;

        float minwidth = 10;
        if (_section.Block.Type == BlockTypeEnum.trigger)
            minwidth = 0;

        float width = 0;
        //float height = 100;
        float height = GetMaxHeight();

        //UpdateChildBlocksList();
        int childsLength = ChildBlocksArray.Length;
        for (int i = 0; i < childsLength; i++)
        {
            width += ChildBlocksArray[i].Layout.Size.x - 10;
            //if (ChildBlocksArray[i].Layout.Size.y > height)
            //    height = ChildBlocksArray[i].Layout.Size.y;
        }
        width -= 10;
        //height += 15;

        if (width < minwidth)
            width = minwidth;

        if (_section.RectTransform.transform.GetSiblingIndex() == _section.RectTransform.transform.parent.childCount - 2)
        {
            if (_section.Block.Type != BlockTypeEnum.trigger)
            {
                width += 50;
            }
        }

        _rectTransform.sizeDelta = new Vector2(width, height);
    }

    float GetMaxHeight()
    {
        I_BE2_BlockSection[] sectionArray = _blockLayout.SectionsArray;

        float height = 0;

        foreach (I_BE2_BlockSection section in sectionArray)
        {
            I_BE2_BlockSectionBody body = section.Body;
            float tempHeight = 100;
            
            body.UpdateChildBlocksList();
            int childsLength = body.ChildBlocksArray.Length;
            for (int i = 0; i < childsLength; i++)
            {
                if (body.ChildBlocksArray[i].Layout.Size.y > tempHeight)
                    tempHeight = body.ChildBlocksArray[i].Layout.Size.y;
            }

            if(tempHeight > height) height = tempHeight;
        }

        height += 15;

        return height;
    }
}
