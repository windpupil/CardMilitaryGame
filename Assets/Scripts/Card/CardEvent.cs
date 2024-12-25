using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardEvent
    : MonoBehaviour,
        IDragHandler,
        IBeginDragHandler,
        IEndDragHandler,
        IPointerExitHandler,
        IPointerEnterHandler
{
    public SoldierCardData data; // 卡牌数据
    private bool isAllowDrag = true; // 是否允许拖拽
    private GameObject foldedGround; //”丢弃“的游戏对象
    private CardEvent instance;
    public CardEvent Instance
    {
        get { return instance; }
    }

    private void Start()
    {
        //为丢弃赋值
        foldedGround = GameObject.Find("丢弃");
        HandCard.Instance = GameObject.Find("手牌库").GetComponent<HandCard>();
        instance = this;
        leftShowCard = GameObject.Find("左侧展示卡牌");
    }

    [Tooltip("本变量用于存放提示部署资源不足的预制体")]
    [SerializeField]
    private GameObject tipResource;

    [Tooltip("本变量用于存放左侧展示卡牌")]
    private GameObject leftShowCard;

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (Manage.Instance.isAction)
        {
            if (
                data.cost["补给"] > ResourceNumberUI.Instance.FoodNumber
                || data.cost["铁矿"] > ResourceNumberUI.Instance.IronNumber
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
                for (int i = 0; i < data.counts; i++)
                {
                    StaticGround.Instance.grounds[
                        data.locationRow[i],
                        data.locationColumn[i]
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
        if (Manage.Instance.isAction)
        {
            if (isAllowDrag)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
                if (hit.collider != null)
                {
                    for (int i = 0; i < data.counts; i++)
                    {
                        if (
                            hit.collider.gameObject
                            == StaticGround.Instance.grounds[
                                data.locationRow[i],
                                data.locationColumn[i]
                            ]
                        )
                        {
                            GameObject newGameObject = GameObject.Instantiate(
                                data.gameObject,
                                hit.collider.gameObject.transform.position,
                                Quaternion.identity
                            );
                            newGameObject.GetComponent<BaseObject>().data = data;
                            newGameObject.GetComponent<BaseObject>().row = data.locationRow[
                                i
                            ];
                            newGameObject.GetComponent<BaseObject>().column =
                                data.locationColumn[i];
                            //更新资源数
                            if (data.cost["补给"] != 0)
                            {
                                ResourceNumberUI.Instance.FoodNumber -= data.cost["补给"];
                            }
                            if (data.cost["铁矿"] != 0)
                            {
                                ResourceNumberUI.Instance.IronNumber -= data.cost["铁矿"];
                            }
                            ResourceNumberUI.Instance.updateResourceNumberText();
                            HandCard.Instance.HandCardCounts--;

                            Manage.Instance.SuppliesConsumedPerTurn += data.perCost["补给"];
                            Manage.Instance.IronConsumedPerTurn += data.perCost["铁矿"];

                            Destroy(this.gameObject);
                        }
                        StaticGround.Instance.grounds[
                            data.locationRow[i],
                            data.locationColumn[i]
                        ]
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

    public void OnPointerEnter(PointerEventData eventData)
    {
        leftShowCard.SetActive(true);
        leftShowCard.GetComponent<CardShowEx>().ShowCard(data);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        leftShowCard.SetActive(false);
    }
}
