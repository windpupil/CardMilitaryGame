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
    private void Start()
    {
        int[] locationRow=new int[cardData.counts];
        int[] locationColumn=new int[cardData.counts];
        ShowCard();
    }
    public void ShowCard()
    {
        this.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = cardData.attack.ToString();
        this.transform.GetChild(1).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = cardData.defense.ToString();
        this.transform.GetChild(1).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = cardData.health.ToString();
        this.transform.GetChild(1).GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>().text = cardData.distance.ToString();
        this.transform.GetChild(1).GetChild(4).GetChild(0).GetComponent<TextMeshProUGUI>().text = cardData.costValue[0].ToString();
        this.transform.GetChild(1).GetChild(5).GetChild(0).GetComponent<TextMeshProUGUI>().text = cardData.cardType;
        this.transform.GetChild(1).GetChild(6).GetComponent<TextMeshProUGUI>().text = cardData.cardName;
        this.transform.GetChild(1).GetChild(7).GetComponent<TextMeshProUGUI>().text = cardData.cardDescription;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //改变颜色为绿色
        for (int i = 0; i < cardData.counts; i++)
        {
            StaticGround.grounds[cardData.locationRow[i], cardData.locationColumn[i]].GetComponent<SpriteRenderer>().color = UnityEngine.Color.green;
        }
    }
    public void OnDrag(PointerEventData eventData)
    {

    }
    public void OnEndDrag(PointerEventData eventData)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
        if (hit.collider != null)
        {
            for (int i = 0; i < cardData.counts; i++)
            {
                if(hit.collider.gameObject==StaticGround.grounds[cardData.locationRow[i], cardData.locationColumn[i]])
                {
                    GameObject newGameObject = GameObject.Instantiate(cardData.gameObject, hit.collider.gameObject.transform.position, Quaternion.identity);
                    newGameObject.GetComponent<ObjectsControl>().cardData = cardData;
                    newGameObject.GetComponent<ObjectsControl>().row = cardData.locationRow[i];
                    newGameObject.GetComponent<ObjectsControl>().column = cardData.locationColumn[i];
                    if(cardData.costType[0]=="粮食")
                    {
                        ResourceNumberUI.FoodNumber -= cardData.costValue[0];
                    }
                    else if(cardData.costType[0]=="木材")
                    {
                        ResourceNumberUI.WoodNumber -= cardData.costValue[0];
                    }
                    ResourceNumberUI.updateResourceNumberText();
                    Destroy(this.gameObject);
                }
                StaticGround.grounds[cardData.locationRow[i], cardData.locationColumn[i]].GetComponent<SpriteRenderer>().color = UnityEngine.Color.white;
            }
        }
    }
}
