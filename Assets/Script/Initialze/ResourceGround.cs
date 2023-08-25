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
        Manage.resourcePoints.Add(this.gameObject);
    }
    public void AddResource()        //将资源点的资源数加到资源UI上
    {
        if (isHaveObject)
        {
            if (resourceType == "补给")
            {
                ResourceNumberUI.Instance.FoodNumber += resourceNumber;
            }
            else if (resourceType == "铁矿")
            {
                ResourceNumberUI.Instance.IronNumber += resourceNumber;
            }
            ResourceNumberUI.Instance.updateResourceNumberText();
        }
    }
}
