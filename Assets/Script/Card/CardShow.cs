using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class CardShow : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerExitHandler,IPointerEnterHandler
{
    public SoldierCardData cardData; // 卡牌数据
    private bool isAllowDrag = true; // 是否允许拖拽
    private GameObject foldedGround; //”丢弃“的游戏对象
    private static CardShow instance;
    public static CardShow Instance
    {
        get { return instance; }
    }

    private void Start()
    {
        instance = this;
        int[] locationRow = new int[cardData.counts];
        int[] locationColumn = new int[cardData.counts];
        //为丢弃赋值
        foldedGround = GameObject.Find("丢弃");
        HandCard.Instance = GameObject.Find("手牌库").GetComponent<HandCard>();
        ShowCard();
    }

    public void ShowCard()
    {
        this.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text =
            cardData.attack.ToString();
        this.transform.GetChild(1).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text =
            cardData.defense.ToString();
        this.transform.GetChild(1).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text =
            cardData.health.ToString();
        this.transform.GetChild(1).GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>().text =
            cardData.moveDistance.ToString();

        this.transform.GetChild(1).GetChild(4).GetChild(0).GetComponent<TextMeshProUGUI>().text =
            cardData.cost["补给"].ToString(); //这里需要修改

        this.transform.GetChild(1).GetChild(5).GetChild(0).GetComponent<TextMeshProUGUI>().text =
            cardData.cardType;
        this.transform.GetChild(1).GetChild(6).GetComponent<TextMeshProUGUI>().text =
            cardData.cardName;
        this.transform.GetChild(1).GetChild(7).GetComponent<TextMeshProUGUI>().text =
            cardData.cardDescription;
    }

    [Tooltip("本变量用于存放提示部署资源不足的预制体")]
    [SerializeField]
    private GameObject tipResource;

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (Manage.Instance.isAction)
        {
            if (
                cardData.cost["补给"] > ResourceNumberUI.Instance.FoodNumber
                || cardData.cost["铁矿"] > ResourceNumberUI.Instance.IronNumber
            )
            {
                isAllowDrag = false;
                //跳出提示，材料不足
                GameObject tip = GameObject.Instantiate(
                    tipResource,
                    GameObject.Find("Canvas").transform.position,
                    Quaternion.identity
                );
                tip.transform.SetParent(GameObject.Find("Canvas").transform);
                //2秒后销毁
                Destroy(tip, 2f);
            }
            else
            {
                isAllowDrag = true;
            }
            if (isAllowDrag)
            {
                //改变颜色为绿色
                for (int i = 0; i < cardData.counts; i++)
                {
                    StaticGround.Instance.grounds[cardData.locationRow[i], cardData.locationColumn[i]]
                        .GetComponent<SpriteRenderer>()
                        .color = UnityEngine.Color.green;
                }
            }
        }
        else if (HandCard.Instance.isUpperLimit && Manage.Instance.isEnd) { }
    }

    public void OnDrag(PointerEventData eventData) { }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (Manage.Instance.isAction)
        {
            if (isAllowDrag)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
                if (hit.collider != null)
                {
                    for (int i = 0; i < cardData.counts; i++)
                    {
                        if (
                            hit.collider.gameObject
                            == StaticGround.Instance.grounds[
                                cardData.locationRow[i],
                                cardData.locationColumn[i]
                            ]
                        )
                        {
                            GameObject newGameObject = GameObject.Instantiate(
                                cardData.gameObject,
                                hit.collider.gameObject.transform.position,
                                Quaternion.identity
                            );
                            newGameObject.GetComponent<ObjectsControl>().cardData = cardData;
                            newGameObject.GetComponent<ObjectsControl>().row = cardData.locationRow[
                                i
                            ];
                            newGameObject.GetComponent<ObjectsControl>().column =
                                cardData.locationColumn[i];
                            //更新资源数
                            if (cardData.cost["补给"] != 0)
                            {
                                ResourceNumberUI.Instance.FoodNumber -= cardData.cost["补给"];
                            }
                            if (cardData.cost["铁矿"] != 0)
                            {
                                ResourceNumberUI.Instance.IronNumber -= cardData.cost["铁矿"];
                            }
                            ResourceNumberUI.Instance.updateResourceNumberText();
                            HandCard.Instance.HandCardCounts--;

                            CollectionOfConstants.SuppliesConsumedPerTurn += cardData.perCost["补给"];
                            CollectionOfConstants.IronConsumedPerTurn += cardData.perCost["铁矿"];

                            Destroy(this.gameObject);
                        }
                        StaticGround.Instance.grounds[cardData.locationRow[i], cardData.locationColumn[i]]
                            .GetComponent<SpriteRenderer>()
                            .color = UnityEngine.Color.white;
                    }
                }
            }
        }
        else if (HandCard.Instance.isUpperLimit && Manage.Instance.isEnd)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
            if (hit.collider != null && hit.collider.gameObject == foldedGround)
            {
                HandCard.Instance.HandCardCounts--;
                Destroy(this.gameObject);
            }
        }
    }
    [Tooltip("本变量用于存放左侧展示卡牌")]
    [SerializeField]
    private GameObject leftShowCard;
    public void OnPointerEnter(PointerEventData eventData){
        leftShowCard.SetActive(true);
    }
    public void OnPointerExit(PointerEventData eventData){
        leftShowCard.SetActive(false);
    }
}