using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Ground : MonoBehaviour
{
    public GameObject objectControl;    //格子上的物体
    public bool isHaveObject = false;  //是否有物体
    public int row;              //行
    public int column;           //列
    public UnityEvent ClickEvent;
    private void OnMouseDown()
    {
        ClickEvent.Invoke();
    }
}
