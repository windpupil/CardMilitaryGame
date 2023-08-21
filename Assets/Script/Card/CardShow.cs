using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class CardShow : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public SoldierCardData cardData;   // 卡牌数据
    public static bool isAllowDrag = true;  // 是否允许拖拽
    private void Start()
    {
        int[] locationRow = new int[cardData.counts];
        int[] locationColumn = new int[cardData.counts];
        ShowCard();
    }
    public void ShowCard()
    {
        this.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = cardData.attack.ToString();
        this.transform.GetChild(1).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = cardData.defense.ToString();
        this.transform.GetChild(1).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = cardData.health.ToString();
        this.transform.GetChild(1).GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>().text = cardData.moveDistance.ToString();

        this.transform.GetChild(1).GetChild(4).GetChild(0).GetComponent<TextMeshProUGUI>().text = cardData.cost["补给"].ToString();//这里需要修改
        
        this.transform.GetChild(1).GetChild(5).GetChild(0).GetComponent<TextMeshProUGUI>().text = cardData.cardType;
        this.transform.GetChild(1).GetChild(6).GetComponent<TextMeshProUGUI>().text = cardData.cardName;
        this.transform.GetChild(1).GetChild(7).GetComponent<TextMeshProUGUI>().text = cardData.cardDescription;
    }

    [SerializeField] private GameObject tipResource;
    public void OnBeginDrag(PointerEventData eventData)
    {
        if(cardData.cost["补给"] >ResourceNumberUI.FoodNumber||cardData.cost["铁矿"] >ResourceNumberUI.IronNumber)
        {
            isAllowDrag=false;
            //跳出提示，材料不足
            GameObject tip = GameObject.Instantiate(tipResource, GameObject.Find("Canvas").transform.position, Quaternion.identity);
            tip.transform.SetParent(GameObject.Find("Canvas").transform);
            //2秒后销毁
            Destroy(tip, 2f);
        }
        else
        {
            isAllowDrag=true;
        }
        if (isAllowDrag)
        {
            //改变颜色为绿色
            for (int i = 0; i < cardData.counts; i++)
            {
                StaticGround.grounds[cardData.locationRow[i], cardData.locationColumn[i]].GetComponent<SpriteRenderer>().color = UnityEngine.Color.green;
            }
        }
    }
    public void OnDrag(PointerEventData eventData)
    {

    }
    public void OnEndDrag(PointerEventData eventData)
    {
        if (isAllowDrag)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
            if (hit.collider != null)
            {
                for (int i = 0; i < cardData.counts; i++)
                {
                    if (hit.collider.gameObject == StaticGround.grounds[cardData.locationRow[i], cardData.locationColumn[i]])
                    {
                        GameObject newGameObject = GameObject.Instantiate(cardData.gameObject, hit.collider.gameObject.transform.position, Quaternion.identity);
                        newGameObject.GetComponent<ObjectsControl>().cardData = cardData;
                        newGameObject.GetComponent<ObjectsControl>().row = cardData.locationRow[i];
                        newGameObject.GetComponent<ObjectsControl>().column = cardData.locationColumn[i];
                        //更新资源数
                        if (cardData.cost["补给"] != 0)
                        {
                            ResourceNumberUI.FoodNumber -= cardData.cost["补给"];
                        }
                        // if (cardData.cost["木材"] != 0)
                        // {
                        //     ResourceNumberUI.WoodNumber -= cardData.cost["木材"];
                        // }
                        if (cardData.cost["铁矿"] != 0)
                        {
                            ResourceNumberUI.IronNumber -= cardData.cost["铁矿"];
                        }
                        ResourceNumberUI.updateResourceNumberText();
                        Destroy(this.gameObject);
                    }
                    StaticGround.grounds[cardData.locationRow[i], cardData.locationColumn[i]].GetComponent<SpriteRenderer>().color = UnityEngine.Color.white;
                }
            }
        }
    }
}
