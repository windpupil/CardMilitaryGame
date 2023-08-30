using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 本脚本是资源数UI的控制脚本
/// </summary>
public class ResourceGround : Ground
{
    public int resourceNumber;  //资源数
    public string resourceType;    //资源类型

    void Start()
    {
        Manage.Instance.resourcePoints.Add(this.gameObject.GetComponent<ResourceGround>());
        AIBrain.Instance.resourcePoints.Add(this.gameObject.GetComponent<ResourceGround>());
    }
}
