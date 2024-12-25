using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
/// <summary>
/// 本脚本用于挂载在资源卡上以显示资源卡的信息并执行相关操作
/// </summary>
public class ResourceCardShow : MonoBehaviour
{
    //本脚本用于挂载在资源卡上以显示资源卡的信息并执行相关操作
    [Tooltip("本变量用于存储资源卡数据")]
    public ResourceCardData resourceCardData;        //定义一个资源卡数据的变量
    private void Start()
    {
        ShowCard();
        this.transform.parent.SetParent(GameObject.Find("Canvas").transform);
        this.transform.parent.localPosition = new Vector3(0, 0, 0);
        Destroy(this.transform.parent.gameObject, 1f);
    }

    public void ShowCard()
    {
        this.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text =
            resourceCardData.cardType;
        this.transform.GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>().text =
            resourceCardData.cardName;
        this.transform.GetChild(1).GetChild(2).GetComponent<TextMeshProUGUI>().text =
            resourceCardData.cardDescription;
    }
    private void OnDestroy()
    {
        ResourceNumberUI.Instance.FoodNumber += resourceCardData.resourceType["补给"];
        ResourceNumberUI.Instance.IronNumber += resourceCardData.resourceType["铁矿"];
        ResourceNumberUI.Instance.updateResourceNumberText();
    }
}