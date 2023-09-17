using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Enemy : Control
{
    private static Enemy instance;
    public static Enemy Instance
    {
        get { return instance; }
    }
    private void Start()
    {
        instance = this;
        realDistance = data.moveDistance;
    }

    /// <summary>
    /// 本函数用于士兵死亡后减少每回合的消耗
    /// </summary>
    private void OnDestroy()
    {
        Manage.Instance.SuppliesConsumedPerTurn -= data.perCost["补给"];
        Manage.Instance.IronConsumedPerTurn -= data.perCost["铁矿"];
    }
}
