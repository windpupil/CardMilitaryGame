using System.Net.Mime;
using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class CardShow
    : MonoBehaviour,
        IDragHandler,
        IBeginDragHandler,
        IEndDragHandler,
        IPointerExitHandler,
        IPointerEnterHandler
{
    public SoldierCardData cardData; // 卡牌数据
    private static CardShow instance;
    public static CardShow Instance
    {
        get { return instance; }
    }

    private bool isAllowDrag = true; // 是否允许拖拽
    private GameObject foldedGround; //”丢弃“的游戏对象

    [Tooltip("本变量用于存放左侧展示卡牌")]
    private GameObject leftShowCard;

    private void Start()
    {
        //为丢弃赋值
        foldedGround = GameObject.Find("丢弃");
        HandCard.Instance = GameObject.Find("手牌库").GetComponent<HandCard>();
        instance = this;
        leftShowCard = GameObject.Find("左侧展示卡牌");
        ShowCard();
    }

    [Tooltip("本变量用于存放提示部署资源不足的预制体")]
    [SerializeField]
    private GameObject tipResource;

    public void ShowCard()
    {
        this.transform.GetComponent<Image>().sprite = cardData.cardImage;
        this.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text =
            cardData.attack.ToString();
        this.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text =
            cardData.defense.ToString();
        this.transform.GetChild(0).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text =
            cardData.health.ToString();
        this.transform.GetChild(0).GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>().text =
            cardData.attackDistance.ToString();
        this.transform.GetChild(0).GetChild(4).GetChild(0).GetComponent<TextMeshProUGUI>().text =
            cardData.moveDistance.ToString();
    }

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
                //刷新地图
                StaticGround.Instance.updateGroundsColor();
                //改变颜色为绿色
                for (int i = 0; i < cardData.counts; i++)
                {
                    if (
                        !StaticGround.Instance.grounds[
                            cardData.locationRow[i],
                            cardData.locationColumn[i]
                        ]
                            .GetComponent<Ground>()
                            .isHaveObject
                    )
                        StaticGround.Instance.grounds[
                            cardData.locationRow[i],
                            cardData.locationColumn[i]
                        ]
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
        if (Manage.Instance.isAction)   //行动阶段
        {
            if (isAllowDrag)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
                if (hit.collider != null)
                {
                    bool isUse = false;
                    for (int i = 0; i < cardData.counts; i++)
                    {
                        if (
                            hit.collider.gameObject
                            == StaticGround.Instance.grounds[
                                cardData.locationRow[i],
                                cardData.locationColumn[i]
                            ]&& (hit.collider.gameObject.GetComponent<Ground>().isHaveObject
                                        == false)
                        )
                        {
                            isUse = true;
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
                            //更新每回合消耗的资源
                            CollectionOfConstants.SuppliesConsumedPerTurn += cardData.perCost["补给"];
                            CollectionOfConstants.IronConsumedPerTurn += cardData.perCost["铁矿"];
                            //将该格的isHaveObject变为true
                            StaticGround.Instance.grounds[
                                cardData.locationRow[i],
                                cardData.locationColumn[i]
                            ]
                                .GetComponent<Ground>()
                                .isHaveObject = true;
                            //将该格的objectControl变为该物体
                            StaticGround.Instance.grounds[
                                cardData.locationRow[i],
                                cardData.locationColumn[i]
                            ]
                                .GetComponent<Ground>()
                                .objectControl = newGameObject;
                            //删除该物体的父物体
                            Destroy(this.transform.parent.gameObject);
                        }
                        StaticGround.Instance.grounds[
                            cardData.locationRow[i],
                            cardData.locationColumn[i]
                        ]
                            .GetComponent<SpriteRenderer>()
                            .color = UnityEngine.Color.white;
                    }
                    if (!isUse)
                    {
                        //将格子变成原来的颜色
                        StaticGround.Instance.updateGroundsColor();
                    }
                }
                else
                {
                    //将格子变成原来的颜色
                    StaticGround.Instance.updateGroundsColor();
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

    public void OnPointerEnter(PointerEventData eventData)
    {
        leftShowCard.GetComponent<Image>().enabled = true;
        leftShowCard.SetActive(true);
        leftShowCard.GetComponent<CardShowEx>().ShowCard(cardData);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        leftShowCard.SetActive(false);
    }
}
