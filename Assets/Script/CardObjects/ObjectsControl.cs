using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 本脚本是物体的控制脚本
/// </summary>
public class ObjectsControl : MonoBehaviour
{
    private static ObjectsControl instance;
    public static ObjectsControl Instance
    {
        get { return instance; }
    }
    public SoldierCardData cardData;
    public int row; // 行
    public int column; // 列
    public int realDistance; // 回合中实际剩余步数

    private void Start()
    {
        realDistance = cardData.moveDistance;
        instance = this;
    }

    private void OnDestroy()
    {
        Manage.Instance.SuppliesConsumedPerTurn -= cardData.perCost["补给"];
        Manage.Instance.IronConsumedPerTurn -= cardData.perCost["铁矿"];
        StaticGround.Instance.updateGroundsColor();
    }

    private void OnMouseDown()
    {
        //更新整张地图恢复原来的颜色
        StaticGround.Instance.updateGroundsColor();
        SearchAndShowGrounds();
    }

    /// <summary>
    /// 寻找并展示可以去的地方
    /// </summary>
    public void SearchAndShowGrounds()
    {
        //当物体被点击后，物体通过cardData中的distance来判断哪些格子可以到达
        //将所有可以到达的格子变成绿色
        if (ActionNumberUI.Instance.actionNumber != 0 && Manage.Instance.isAction)
        {
            for (int i = 0; i <= realDistance; i++)
            {
                for (int j = 0; j <= realDistance; j++)
                {
                    if ((i != 0 || j != 0) && ((i + j) <= realDistance))
                    {
                        if (row + i < CollectionOfConstants.MapRow && column + j < CollectionOfConstants.MapColumn && !StaticGround.Instance.grounds[row + i, column + j].GetComponent<Ground>().isHaveObject)
                        {
                            StaticGround.Instance.grounds[row + i, column + j].GetComponent<SpriteRenderer>().color = UnityEngine.Color.green;
                            StaticGround.Instance.grounds[row + i, column + j].GetComponent<Ground>().isActive = true;
                            StaticGround.Instance.grounds[row + i, column + j].GetComponent<Ground>().Steps = i + j;
                            StaticGround.Instance.grounds[row + i, column + j].GetComponent<Ground>().possibleFootholds = this.gameObject;
                        }
                        if (row - i >= 0 && column + j < CollectionOfConstants.MapColumn && !StaticGround.Instance.grounds[row - i, column + j].GetComponent<Ground>().isHaveObject)
                        {
                            StaticGround.Instance.grounds[row - i, column + j].GetComponent<SpriteRenderer>().color = UnityEngine.Color.green;
                            StaticGround.Instance.grounds[row - i, column + j].GetComponent<Ground>().isActive = true;
                            StaticGround.Instance.grounds[row - i, column + j].GetComponent<Ground>().Steps = i + j;
                            StaticGround.Instance.grounds[row - i, column + j].GetComponent<Ground>().possibleFootholds = this.gameObject;
                        }
                        if (row + i < CollectionOfConstants.MapRow && column - j >= 0 && !StaticGround.Instance.grounds[row + i, column - j].GetComponent<Ground>().isHaveObject)
                        {
                            StaticGround.Instance.grounds[row + i, column - j].GetComponent<SpriteRenderer>().color = UnityEngine.Color.green;
                            StaticGround.Instance.grounds[row + i, column - j].GetComponent<Ground>().isActive = true;
                            StaticGround.Instance.grounds[row + i, column - j].GetComponent<Ground>().Steps = i + j;
                            StaticGround.Instance.grounds[row + i, column - j].GetComponent<Ground>().possibleFootholds = this.gameObject;
                        }
                        if (row - i >= 0 && column - j >= 0 && !StaticGround.Instance.grounds[row - i, column - j].GetComponent<Ground>().isHaveObject)
                        {
                            StaticGround.Instance.grounds[row - i, column - j].GetComponent<SpriteRenderer>().color = UnityEngine.Color.green;
                            StaticGround.Instance.grounds[row - i, column - j].GetComponent<Ground>().isActive = true;
                            StaticGround.Instance.grounds[row - i, column - j].GetComponent<Ground>().Steps = i + j;
                            StaticGround.Instance.grounds[row - i, column - j].GetComponent<Ground>().possibleFootholds = this.gameObject;
                        }
                    }
                }
            }
        }
    }
}
