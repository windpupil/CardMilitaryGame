using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseObject : MonoBehaviour
{
    public SoldierCardData data;//数据
    [HideInInspector]
    public int row; //行
    [HideInInspector]
    public int column; //列
}
