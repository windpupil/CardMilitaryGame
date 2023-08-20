using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticGround : MonoBehaviour
{
    public static GameObject[,] grounds = new GameObject[CollectionOfConstants.MapRow, CollectionOfConstants.MapColumn];    // 地图区域
    private void Awake()
    {
        //遍历数组，将所有地图区域放入数组中
        for (int i = 0; i < CollectionOfConstants.MapRow; i++)
        {
            for (int j = 0; j < CollectionOfConstants.MapColumn; j++)
            {
                grounds[i, j] = this.transform.GetChild(i * CollectionOfConstants.MapColumn + j).gameObject;
                grounds[i, j].GetComponent<Ground>().row = i;
                grounds[i, j].GetComponent<Ground>().column = j;
            }
        }
    }

    public static void updateObjectsControlRealDistance()
    {
        for (int i = 0; i < CollectionOfConstants.MapRow; i++)
        {
            for (int j = 0; j < CollectionOfConstants.MapColumn; j++)
            {
                grounds[i , j].GetComponent<Ground>().updateObjectsControlRealDistance();
            }
        }
    }

    /// <summary>
    /// 将所有格子变成原来的颜色
    /// </summary>
    public static void updateGroundsColor()
    {
        for (int i = 0; i < CollectionOfConstants.MapRow; i++)
        {
            for (int j = 0; j < CollectionOfConstants.MapColumn; j++)
            {
                if (!grounds[i, j].GetComponent<Ground>().isHaveObject && grounds[i, j].tag != "MainCity")
                {
                    grounds[i, j].GetComponent<SpriteRenderer>().color = UnityEngine.Color.white;
                    grounds[i, j].GetComponent<Ground>().isActive = false;
                }
            }
        }
    }
}