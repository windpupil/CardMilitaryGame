using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// 本脚本用于抽卡
/// </summary>
public class DrawResourceCardButton : MonoBehaviour
{
    [SerializeField] private GameObject handCard;
    public Manage manage;
    public GameObject UIDraw;

    private void Start()
    {
        if (CheckIsNull(manage.resourceCard.Count))
        {
            //将按钮设置为不可用
            this.GetComponent<Button>().interactable = false;
        }
    }
    public void resourceCardClick()
    {
        //生成卡牌
        GameObject card = Instantiate(manage.resourceCard[manage.resourceCard.Count - 1], this.transform.position, Quaternion.identity);
        card.transform.SetParent(handCard.transform);
        //移除卡牌
        manage.resourceCard.RemoveAt(manage.resourceCard.Count - 1);
        DrawCardCountsUI.resourceCardCountsCurrent--;
        DrawCardCountsUI.updateResourceCardCountsText();
        if (CheckIsNull(manage.resourceCard.Count))                                                             //如果卡牌数为0，将按钮设置为不可用
        {
            this.GetComponent<Button>().interactable = false;
        }
        if (DrawCardCountsUI.resourceCardCountsCurrent == 0)                                                   //如果抽卡数为0，进入行动阶段
        {
            Finish();
        }
        else if (CheckIsNull(manage.resourceCard.Count) && CheckIsNull(manage.notresourceCard.Count))                //如果两个卡牌堆都为空，进入行动阶段
        {
            Finish();
            if (manage.resourceCard.Count == 0 && manage.notresourceCard.Count == 0)
            {
                //生成提示，并在5秒后销毁
                GameObject tip = Instantiate(tipCardIsNull, manage.Canvas.position, Quaternion.identity);
                tip.transform.SetParent(GameObject.Find("Canvas").transform);
                Destroy(tip, 5);
            }
        }
    }
    public void notresourceCardClick()
    {

        //生成卡牌
        GameObject card = Instantiate(manage.notresourceCard[manage.notresourceCard.Count - 1], this.transform.position, Quaternion.identity);
        card.transform.SetParent(handCard.transform);
        //移除卡牌
        manage.notresourceCard.RemoveAt(manage.notresourceCard.Count - 1);
        DrawCardCountsUI.resourceCardCountsCurrent--;
        DrawCardCountsUI.updateResourceCardCountsText();
        if (CheckIsNull(manage.notresourceCard.Count))
        {
            this.GetComponent<Button>().interactable = false;
        }
        if (DrawCardCountsUI.resourceCardCountsCurrent == 0)
        {
            Finish();
        }
        else if (CheckIsNull(manage.resourceCard.Count) && CheckIsNull(manage.notresourceCard.Count))
        {
            Finish();
            if (manage.resourceCard.Count == 0 && manage.notresourceCard.Count == 0)
            {
                //生成提示，并在5秒后销毁
                GameObject tip = Instantiate(tipCardIsNull, manage.Canvas.position, Quaternion.identity);
                tip.transform.SetParent(GameObject.Find("Canvas").transform);
                Destroy(tip, 5);
            }
        }
    }

    public GameObject tipCardIsNull;
    private void Finish()
    {
        UIDraw.SetActive(false);
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