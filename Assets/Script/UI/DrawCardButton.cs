using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCardButton : MonoBehaviour
{
    [SerializeField] private GameObject handCard;
    [SerializeField] private Manage manage;
    [SerializeField] private GameObject UIDraw;
    private Vector3 cardPosition = new Vector3(0, 0, 0);
    public void resourceCardClick()
    {
        //生成卡牌
        GameObject card = Instantiate(manage.resourceCard[manage.resourceCard.Count-1], cardPosition, Quaternion.identity);
        cardPosition.y -= 5f;
        card.transform.SetParent(handCard.transform);
        //移除卡牌
        manage.resourceCard.RemoveAt(manage.resourceCard.Count-1);
        DrawCardCountsUI.resourceCardCountsCurrent--;
        DrawCardCountsUI.updateResourceCardCountsText();
        if (DrawCardCountsUI.resourceCardCountsCurrent == 0)
        {
            Finish();
        }
    }
    public void notresourceCardClick()
    {

        //生成卡牌
        GameObject card = Instantiate(manage.notresourceCard[manage.notresourceCard.Count-1], cardPosition, Quaternion.identity);
        cardPosition.y -= 5f;
        card.transform.SetParent(handCard.transform);
        //移除卡牌
        manage.notresourceCard.RemoveAt(manage.notresourceCard.Count-1);
        DrawCardCountsUI.resourceCardCountsCurrent--;
        DrawCardCountsUI.updateResourceCardCountsText();
        if (DrawCardCountsUI.resourceCardCountsCurrent == 0||(manage.notresourceCard.Count==0&&manage.resourceCard.Count==0))
        {
            Finish();
        }
    }

    public GameObject tipCardIsNull;
    private void Finish()
    {
        cardPosition = new Vector3(0, 0, 0);
        UIDraw.SetActive(false);
        if (manage.resourceCard.Count == 0 && manage.notresourceCard.Count == 0)
        {
            //生成提示，并在5秒后销毁
            GameObject tip = Instantiate(tipCardIsNull, new Vector3(0, 0, 0), Quaternion.identity);
            tip.transform.SetParent(GameObject.Find("Canvas").transform);
            Destroy(tip, 5);
        }
        //进入行动阶段
    }
}
