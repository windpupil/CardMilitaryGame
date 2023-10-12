using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Enemy : Control
{
    private void Start()
    {
        realDistance = this.GetComponent<BaseObject>().data.moveDistance;
    }

    /// <summary>
    /// 本函数用于士兵死亡后减少每回合的消耗
    /// </summary>
    private void OnDestroy()
    {
        Manage.Instance.SuppliesConsumedPerTurn -= this.GetComponent<BaseObject>().data.perCost["补给"];
        Manage.Instance.IronConsumedPerTurn -= this.GetComponent<BaseObject>().data.perCost["铁矿"];
    }
}
