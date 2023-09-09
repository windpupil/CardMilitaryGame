using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 本脚本主要是物体的控制脚本基类，包括移动和基础属性
/// </summary>
public class Control : BaseObject
{
    private int realDistance = 0; // 回合中实际剩余步数
    public List<GameObject> moveObject { get; private set; } = new List<GameObject>(); // 可移动的格子
    private void Start() {
        realDistance = data.moveDistance;
    }

    /// <summary>
    /// 获取可移动的格子
    /// </summary>
    /// <returns></returns>
    public void GetMoveDistanceObject()
    {
        moveObject.Clear();
        for (int i = 0; i <= realDistance; i++)
        {
            for (int j = 0; j <= realDistance; j++)
            {
                if ((i != 0 || j != 0) && ((i + j) <= realDistance))
                {
                    if (row + i < CollectionOfConstants.MAPROW && column + j < CollectionOfConstants.MAPCOLUMN && !StaticGround.Instance.grounds[row + i, column + j].GetComponent<Ground>().isHaveObject)
                    {
                        moveObject.Add(StaticGround.Instance.grounds[row + i, column + j]);
                    }
                    if (row - i >= 0 && column + j < CollectionOfConstants.MAPCOLUMN && !StaticGround.Instance.grounds[row - i, column + j].GetComponent<Ground>().isHaveObject)
                    {
                        moveObject.Add(StaticGround.Instance.grounds[row - i, column + j]);
                    }
                    if (row + i < CollectionOfConstants.MAPROW && column - j >= 0 && !StaticGround.Instance.grounds[row + i, column - j].GetComponent<Ground>().isHaveObject)
                    {
                        moveObject.Add(StaticGround.Instance.grounds[row + i, column - j]);
                    }
                    if (row - i >= 0 && column - j >= 0 && !StaticGround.Instance.grounds[row - i, column - j].GetComponent<Ground>().isHaveObject)
                    {
                        moveObject.Add(StaticGround.Instance.grounds[row - i, column - j]);
                    }
                }
            }
        }
    }
    /// <summary>
    /// 移动
    /// </summary>
    public void Move(GameObject taragt)
    {
        Ground ground = taragt.GetComponent<Ground>();
        //将物体的对象赋值到这个格子上
        ground.objectControl = this.gameObject;
        ground.isHaveObject = true;
        //将物体原来所在的位置更新为没有物体
        StaticGround.Instance.grounds[row, column].GetComponent<Ground>().isHaveObject = false;
        StaticGround.Instance.grounds[row, column].GetComponent<Ground>().objectControl = null;
        //将物体移动到这个格子上
        this.transform.position = ground.transform.position;
        //将物体的行列数改变
        int steps = Mathf.Abs(ground.row - row) + Mathf.Abs(ground.column - column);
        realDistance -= steps;
        row = ground.row;
        column = ground.column;
        if (this.gameObject.tag == "Soldier")
        {
            //更新地图颜色
            StaticGround.Instance.updateGroundsColor();
            //行动点-1
            ActionNumberUI.Instance.actionNumber--;
            ActionNumberUI.Instance.updateActionNumberText();
            foreach (GameObject go in moveObject)
            {
                RemoveGroundListener(go);
            }
        }
    }
    /// <summary>
    /// 更新物体的实际步数
    /// </summary>
    public void updateObjectsControlRealDistance()
    {
        realDistance = data.moveDistance;
    }

    public void RemoveGroundListener(GameObject go)
    {
        go.GetComponent<Ground>().ClickEvent.RemoveListener(() => { Move(go); });
    }
}
