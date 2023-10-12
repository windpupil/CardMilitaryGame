using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 本脚本是物体的控制脚本
/// </summary>
public class Solider : Control
{
    private void Start()
    {
        realDistance = this.GetComponent<BaseObject>().data.moveDistance;
    }
    /// <summary>
    /// 当物体被销毁时，将其从每回合消耗的资源中减去
    /// 因为是玩家士兵，因此需要更新地图颜色
    /// </summary>
    private void OnDestroy()
    {
        Manage.Instance.SuppliesConsumedPerTurn -= this.GetComponent<BaseObject>().data.perCost["补给"];
        Manage.Instance.IronConsumedPerTurn -= this.GetComponent<BaseObject>().data.perCost["铁矿"];
        StaticGround.Instance.updateGroundsColor();
    }

    private void OnMouseDown()
    {
        // Debug.Log("点击了士兵");
        // Debug.Log("士兵的可移动步数为：" + realDistance);
        //更新整张地图恢复原来的颜色
        StaticGround.Instance.updateGroundsColor();
        StaticGround.Instance.ClearAllGroundListner();
        SearchAndShowGrounds();
    }

    /// <summary>
    /// 寻找并展示可以去的地方
    /// </summary>
    public void SearchAndShowGrounds()
    {
        if (ActionNumberUI.Instance.actionNumber != 0 && Manage.Instance.isAction)
        {
            GetMoveDistanceObject();
            SetGround();
            AddGroundListener();
        }
    }
    /// <summary>
    /// 添加监听，当可移动物体被点击时，移动到该物体上
    /// </summary>
    public void AddGroundListener()
    {
        foreach (GameObject go in moveObject)
        {
            go.GetComponent<Ground>().ClickEvent.AddListener(() => { Move(go);SoldierUpdate(); });
        }
    }
    /// <summary>
    /// 移除监听
    /// </summary>
    public void RemoveGroundListener()
    {
        foreach (GameObject go in moveObject)
        {
            go.GetComponent<Ground>().ClickEvent.RemoveAllListeners();
        }
    }
    /// <summary>
    /// 本脚本用于设置可移动范围的地面的属性
    /// </summary>
    public void SetGround()
    {
        foreach (GameObject go in moveObject)
        {
            go.GetComponent<SpriteRenderer>().color = UnityEngine.Color.green;
        }
    }
    /// <summary>
    /// 士兵特有的移动处理
    /// </summary>
    public void SoldierUpdate()
    {
        //更新地图颜色
        StaticGround.Instance.updateGroundsColor();
        //行动点-1
        ActionNumberUI.Instance.actionNumber--;
        ActionNumberUI.Instance.updateActionNumberText();
        RemoveGroundListener();
    }
}
