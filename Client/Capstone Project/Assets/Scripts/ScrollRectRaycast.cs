using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollRectRaycast : MonoBehaviour
{
    BE2_DragDropManager _dragDropManager;
    ScrollRect _scrollRect;

    // Start is called before the first frame update
    void Start()
    {
        _dragDropManager = BE2_DragDropManager.instance;
        _scrollRect = gameObject.GetComponent<ScrollRect>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(_dragDropManager.isDragging);
        _scrollRect.horizontal = !_dragDropManager.isDragging;
        
    }
}
