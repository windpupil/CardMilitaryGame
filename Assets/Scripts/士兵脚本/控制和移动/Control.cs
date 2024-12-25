using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 本脚本主要是物体的控制脚本基类，包括移动和基础属性
/// </summary>
[RequireComponent(typeof(BaseObject))]
public class Control : MonoBehaviour
{
    [HideInInspector]
    public int realDistance = 0; // 回合中实际剩余步数
    [HideInInspector]
    public HashSet<GameObject> moveObject { get; private set; } = new HashSet<GameObject>(); // 可移动的格子
    /// <summary>
    /// 获取可移动的格子
    /// </summary>
    public void GetMoveDistanceObject()
    {
        moveObject.Clear();
        for (int i = 0; i <= realDistance; i++)
        {
            for (int j = 0; j <= realDistance; j++)
            {
                if ((i != 0 || j != 0) && ((i + j) <= realDistance))
                {
                    if (this.GetComponent<BaseObject>().row + i < CollectionOfConstants.MAPROW && this.GetComponent<BaseObject>().column + j < CollectionOfConstants.MAPCOLUMN && !StaticGround.Instance.grounds[this.GetComponent<BaseObject>().row + i, this.GetComponent<BaseObject>().column + j].GetComponent<Ground>().isHaveObject)
                    {
                        moveObject.Add(StaticGround.Instance.grounds[this.GetComponent<BaseObject>().row + i, this.GetComponent<BaseObject>().column + j]);
                    }
                    if (this.GetComponent<BaseObject>().row - i >= 0 && this.GetComponent<BaseObject>().column + j < CollectionOfConstants.MAPCOLUMN && !StaticGround.Instance.grounds[this.GetComponent<BaseObject>().row - i, this.GetComponent<BaseObject>().column + j].GetComponent<Ground>().isHaveObject)
                    {
                        moveObject.Add(StaticGround.Instance.grounds[this.GetComponent<BaseObject>().row - i, this.GetComponent<BaseObject>().column + j]);
                    }
                    if (this.GetComponent<BaseObject>().row + i < CollectionOfConstants.MAPROW && this.GetComponent<BaseObject>().column - j >= 0 && !StaticGround.Instance.grounds[this.GetComponent<BaseObject>().row + i, this.GetComponent<BaseObject>().column - j].GetComponent<Ground>().isHaveObject)
                    {
                        moveObject.Add(StaticGround.Instance.grounds[this.GetComponent<BaseObject>().row + i, this.GetComponent<BaseObject>().column - j]);
                    }
                    if (this.GetComponent<BaseObject>().row - i >= 0 && this.GetComponent<BaseObject>().column - j >= 0 && !StaticGround.Instance.grounds[this.GetComponent<BaseObject>().row - i, this.GetComponent<BaseObject>().column - j].GetComponent<Ground>().isHaveObject)
                    {
                        moveObject.Add(StaticGround.Instance.grounds[this.GetComponent<BaseObject>().row - i, this.GetComponent<BaseObject>().column - j]);
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
        StaticGround.Instance.grounds[this.GetComponent<BaseObject>().row, this.GetComponent<BaseObject>().column].GetComponent<Ground>().isHaveObject = false;
        StaticGround.Instance.grounds[this.GetComponent<BaseObject>().row, this.GetComponent<BaseObject>().column].GetComponent<Ground>().objectControl = null;
        //将物体移动到这个格子上
        this.transform.position = ground.transform.position+new Vector3(0,0,-0.5f);
        //将物体的行列数改变
        int steps = Mathf.Abs(ground.row - this.GetComponent<BaseObject>().row) + Mathf.Abs(ground.column - this.GetComponent<BaseObject>().column);
        realDistance -= steps;
        this.GetComponent<BaseObject>().row = ground.row;
        this.GetComponent<BaseObject>().column = ground.column;
    }
    /// <summary>
    /// 更新物体的实际步数
    /// </summary>
    public void updateObjectsControlRealDistance()
    {
        realDistance = this.GetComponent<BaseObject>().data.moveDistance;
    }
}
