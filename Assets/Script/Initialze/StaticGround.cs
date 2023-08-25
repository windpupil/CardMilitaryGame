using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticGround : MonoBehaviour
{
    [Tooltip("本变量用于存放地图区域")]
    [HideInInspector]
    public GameObject[,] grounds = new GameObject[
        CollectionOfConstants.MapRow,
        CollectionOfConstants.MapColumn
    ]; // 地图区域
    private static StaticGround instance;
    public static StaticGround Instance
    {
        get { return instance; }
    }

    private void Start()
    {
        instance = this;
        //遍历数组，将所有地图区域放入数组中
        for (int i = 0; i < CollectionOfConstants.MapRow; i++)
        {
            for (int j = 0; j < CollectionOfConstants.MapColumn; j++)
            {
                grounds[i, j] = this.transform
                    .GetChild(i * CollectionOfConstants.MapColumn + j)
                    .gameObject;
                grounds[i, j].GetComponent<Ground>().row = i;
                grounds[i, j].GetComponent<Ground>().column = j;
            }
        }
    }

    /// <summary>
    /// 更新所有物体的realDistance
    /// </summary>
    public void updateObjectsControlRealDistance()
    {
        for (int i = 0; i < CollectionOfConstants.MapRow; i++)
        {
            for (int j = 0; j < CollectionOfConstants.MapColumn; j++)
            {
                grounds[i, j].GetComponent<Ground>().updateObjectsControlRealDistance();
            }
        }
    }

    /// <summary>
    /// 将所有grounds标签的格子变成原来的颜色
    /// </summary>
    public void updateGroundsColor()
    {
        for (int i = 0; i < CollectionOfConstants.MapRow; i++)
        {
            for (int j = 0; j < CollectionOfConstants.MapColumn; j++)
            {
                if (grounds[i, j] != null)
                {
                    if (grounds[i, j].tag == "Grounds")
                    {
                        grounds[i, j].GetComponent<SpriteRenderer>().color = UnityEngine
                            .Color
                            .white;
                        grounds[i, j].GetComponent<Ground>().isActive = false;
                    }
                }
                else
                {
                    return;
                }
            }
        }
    }

    /// <summary>
    ///更新所有格子上士兵的攻击次数
    /// <summary>
    public void updateSoldierAttackNumber()
    {
        for (int i = 0; i < CollectionOfConstants.MapRow; i++)
        {
            for (int j = 0; j < CollectionOfConstants.MapColumn; j++)
            {
                if (
                    grounds[i, j].GetComponent<Ground>().isHaveObject
                    && grounds[i, j].GetComponent<Ground>().objectControl.tag == "Soldier"
                )
                {
                    grounds[i, j]
                        .GetComponent<Ground>()
                        .objectControl.GetComponent<Fight>()
                        .attackNumber = grounds[i, j]
                        .GetComponent<Ground>()
                        .objectControl.GetComponent<Fight>()
                        .data.cardData.maxAttackNumber;
                }
            }
        }
    }
}
