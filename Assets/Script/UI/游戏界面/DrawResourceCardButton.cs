using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// 本脚本用于抽卡
/// </summary>
public class DrawResourceCardButton : MonoBehaviour
{
    [Tooltip("本变量用于存放”抽卡界面“的UI")]
    [SerializeField]
    private GameObject UIDraw;

    private void Start()
    {
        if (CheckIsNull(Manage.Instance.resourceCard.Count))
        {
            //将按钮设置为不可用
            this.GetComponent<Button>().interactable = false;
        }
    }
    public void resourceCardClick()
    {
        //生成卡牌
        GameObject card = Instantiate(Manage.Instance.resourceCard[Manage.Instance.resourceCard.Count - 1], this.transform.position, Quaternion.identity);
        // card.transform.SetParent(GameObject.Find("Canvas").transform);
        //移除卡牌
        Manage.Instance.resourceCard.RemoveAt(Manage.Instance.resourceCard.Count - 1);
        DrawCardCountsUI.Instance.resourceCardCountsCurrent--;
        DrawCardCountsUI.Instance.updateResourceCardCountsText();
        if (CheckIsNull(Manage.Instance.resourceCard.Count))                                                             //如果卡牌数为0，将按钮设置为不可用
        {
            this.GetComponent<Button>().interactable = false;
        }
        if (DrawCardCountsUI.Instance.resourceCardCountsCurrent == 0)                                                   //如果抽卡数为0，进入行动阶段
        {
            Finish();
        }
        else if (CheckIsNull(Manage.Instance.resourceCard.Count) && CheckIsNull(Manage.Instance.notresourceCard.Count))                //如果两个卡牌堆都为空，进入行动阶段
        {
            Finish();
            if (Manage.Instance.resourceCard.Count == 0 && Manage.Instance.notresourceCard.Count == 0)
            {
                //生成提示，并在5秒后销毁
                GameObject tip = Instantiate(tipCardIsNull, Manage.Instance.Canvas.position, Quaternion.identity);
                tip.transform.SetParent(GameObject.Find("Canvas").transform);
                Destroy(tip, 5);
            }
        }
    }
    public void notresourceCardClick()
    {

        //生成卡牌
        GameObject card = Instantiate(Manage.Instance.notresourceCard[Manage.Instance.notresourceCard.Count - 1], this.transform.position, Quaternion.identity);
        card.transform.SetParent(HandCard.Instance.transform);
        HandCard.Instance.HandCardCounts++;
        //移除卡牌
        Manage.Instance.notresourceCard.RemoveAt(Manage.Instance.notresourceCard.Count - 1);
        DrawCardCountsUI.Instance.resourceCardCountsCurrent--;
        DrawCardCountsUI.Instance.updateResourceCardCountsText();
        if (CheckIsNull(Manage.Instance.notresourceCard.Count))
        {
            this.GetComponent<Button>().interactable = false;
        }
        if (DrawCardCountsUI.Instance.resourceCardCountsCurrent == 0)
        {
            Finish();
        }
        else if (CheckIsNull(Manage.Instance.resourceCard.Count) && CheckIsNull(Manage.Instance.notresourceCard.Count))
        {
            Finish();
            if (Manage.Instance.resourceCard.Count == 0 && Manage.Instance.notresourceCard.Count == 0)
            {
                //生成提示，并在5秒后销毁
                GameObject tip = Instantiate(tipCardIsNull, Manage.Instance.Canvas.position, Quaternion.identity);
                tip.transform.SetParent(GameObject.Find("Canvas").transform);
                Destroy(tip, 5);
            }
        }
    }

    public GameObject tipCardIsNull;
    private void Finish()
    {
        UIDraw.SetActive(false);
        Manage.Instance.Action();
        //进入行动阶段
    }

    public bool CheckIsNull(int length)
    {
        if (length == 0)
        {
            return true;
        }
        return false;
    }
}