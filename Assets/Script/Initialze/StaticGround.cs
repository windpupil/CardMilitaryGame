using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticGround : MonoBehaviour
{
    [Tooltip("本变量用于存放地图区域")]
    [HideInInspector]
    public GameObject[,] grounds = new GameObject[CollectionOfConstants.MAPROW, CollectionOfConstants.MAPCOLUMN]; // 地图区域
    private static StaticGround instance;
    public static StaticGround Instance
    {
        get { return instance; }
    }

    private void Start()
    {
        instance = this;
        //遍历数组，将所有地图区域放入数组中
        for (int i = 0; i < CollectionOfConstants.MAPROW; i++)
        {
            for (int j = 0; j < CollectionOfConstants.MAPCOLUMN; j++)
            {
                grounds[i, j] = this.transform.GetChild(i * CollectionOfConstants.MAPCOLUMN + j).gameObject;
                grounds[i, j].GetComponent<Ground>().row = i;
                grounds[i, j].GetComponent<Ground>().column = j;
            }
        }
    }


    /// <summary>
    /// 将所有grounds标签的格子变成原来的颜色
    /// </summary>
    public void updateGroundsColor()
    {
        for (int i = 0; i < CollectionOfConstants.MAPROW; i++)
        {
            for (int j = 0; j < CollectionOfConstants.MAPCOLUMN; j++)
            {
                if (grounds[i, j] != null)
                {
                    if (grounds[i, j].tag == "Grounds")
                    {
                        grounds[i, j].GetComponent<SpriteRenderer>().color = UnityEngine.Color.white;
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
    ///更新所有格子上士兵的攻击次数和每回合最大移动距离
    /// <summary>
    public void updateNumber()
    {
        for (int i = 0; i < CollectionOfConstants.MAPROW; i++)
        {
            for (int j = 0; j < CollectionOfConstants.MAPCOLUMN; j++)
            {
                if (grounds[i, j].GetComponent<Ground>().isHaveObject && grounds[i, j].GetComponent<Ground>().objectControl != null && grounds[i, j].GetComponent<Ground>().objectControl.tag == "Soldier")
                {
                    grounds[i, j].GetComponent<Ground>().objectControl.GetComponent<Solider>().updateObjectsControlRealDistance();
                    grounds[i, j].GetComponent<Ground>().objectControl.GetComponent<Fight>().updateObjectsControlAttackNumber();
                }
            }
        }
    }
    /// <summary>
    /// 更新所有格子上士兵的状态按钮
    /// </summary>
    public void updateSoldierStateButton()
    {
        for (int i = 0; i < CollectionOfConstants.MAPROW; i++)
        {
            for (int j = 0; j < CollectionOfConstants.MAPCOLUMN; j++)
            {
                if (grounds[i, j].GetComponent<Ground>().isHaveObject && grounds[i, j].GetComponent<Ground>().objectControl != null && grounds[i, j].GetComponent<Ground>().objectControl.tag == "Soldier")
                {
                    grounds[i, j].GetComponent<Ground>().objectControl.transform.Find("状态转换按钮").GetComponent<StateTransitionButton>().isOn = false;
                }
            }
        }
    }
    /// <summary>
    /// 清楚所有格子的监听
    /// </summary>
    public void ClearAllGroundListner()
    {
        for (int i = 0; i < CollectionOfConstants.MAPROW; i++)
        {
            for (int j = 0; j < CollectionOfConstants.MAPCOLUMN; j++)
            {
                grounds[i, j].GetComponent<Ground>().ClearGroundListner();
            }
        }
    }
}
