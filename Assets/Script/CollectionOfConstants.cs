using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CollectionOfConstants
{
    public const int MapRow = 9; // 地图行数
    public const int MapColumn = 11; // 地图列数
    public const int initialFoodNumber = 10; // 初始补给数
    public const int initialIronNumber = 5; // 初始铁矿数

    public const int HandCardLimit = 10; // 手牌上限
    public const int actionNumberLimit = 10;                //行动点最大上限

    private static int suppliesConsumedPerTurn = 0; // 每回合消耗补给数
    public static int SuppliesConsumedPerTurn
    {
        get { return suppliesConsumedPerTurn; }
        set
        {
            if (Manage.Instance.isResourceEnoughStage)
            {

                if (isEnough())
                {
                    Debug.Log(3);
                    Manage.Instance.isResourceEnoughStage = false;
                    Manage.Instance.resourceDecisionEnd();
                }
            }
            suppliesConsumedPerTurn = value;
        }
    }
    private static int ironConsumedPerTurn = 0; // 每回合消耗铁矿数
    public static int IronConsumedPerTurn
    {
        get { return ironConsumedPerTurn; }
        set
        {
            if (Manage.Instance.isResourceEnoughStage)
            {
                if (isEnough())
                {
                    Manage.Instance.isResourceEnoughStage = false;
                    Manage.Instance.resourceDecisionEnd();
                }
            }
            ironConsumedPerTurn = value;
        }
    }

    public static bool isEnough()
    {
        // Debug.Log("食物数量是：" + ResourceNumberUI.Instance.FoodNumber);
        // Debug.Log("铁矿数量是：" + ResourceNumberUI.Instance.IronNumber);
        // Debug.Log("每回合消耗的食物数量是：" + SuppliesConsumedPerTurn);
        // Debug.Log("每回合消耗的铁矿数量是：" + IronConsumedPerTurn);
        if (
            ResourceNumberUI.Instance.FoodNumber >= SuppliesConsumedPerTurn
            && ResourceNumberUI.Instance.IronNumber >= IronConsumedPerTurn
        )
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
