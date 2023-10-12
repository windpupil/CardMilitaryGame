using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBrain : MonoBehaviour
{
    private static AIBrain instance;
    public static AIBrain Instance
    {
        get { return instance; }
    }
    [Tooltip("本变量用于存放AI占领的资源点")]
    public List<ResourceGround> resourcePoints = new List<ResourceGround>();
    /// <summary>
    /// 本方法用于将资源点加入到resourcePoints中
    /// </summary>
    /// <param name="resourceGround"></param>
    public void AddResourceGround(ResourceGround resourceGround)
    {
        resourcePoints.Add(resourceGround);
    }

    [Tooltip("本变量用于存放AI所有资源卡牌")]
    public List<GameObject> resourceCard = new List<GameObject>();

    [Tooltip("本变量用于存放AI所有非资源卡牌")]
    public List<GameObject> notresourceCard = new List<GameObject>();
    private int HandCardNum = 0; //手牌数
    private const int HandCardNumMax = 10; //手牌上限
    private bool isHavePitfallCard = false; //是否有陷阱卡或者策略卡
    private int foodNumberAI = 10; //AI的补给数
    public int FoodNumberAI
    {
        get { return foodNumberAI; }
        set
        {
            foodNumberAI = value;
        }
    }

    private int ironNumberAI = 10; //AI的铁矿数
    public int IronNumberAI
    {
        get { return ironNumberAI; }
        set
        {
            ironNumberAI = value;
        }
    }
    [HideInInspector]
    public int perConsumedFood = 0; //每回合消耗的补给数
    [HideInInspector]
    public int perConsumedIron = 0; //每回合消耗的铁矿数
    private List<GameObject> Enemys = new List<GameObject>(); //AI的士兵
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        //初始化牌组
        //打乱resourceCard的顺序
        for (int i = 0; i < resourceCard.Count; i++)
        {
            GameObject temp = resourceCard[i];
            int randomIndex = Random.Range(i, resourceCard.Count);
            resourceCard[i] = resourceCard[randomIndex];
            resourceCard[randomIndex] = temp;
        }
        //打乱notresourceCard的顺序
        for (int i = 0; i < notresourceCard.Count; i++)
        {
            GameObject temp = notresourceCard[i];
            int randomIndex = Random.Range(i, notresourceCard.Count);
            notresourceCard[i] = notresourceCard[randomIndex];
            notresourceCard[randomIndex] = temp;
        }
    }
    /// <summary>
    /// AI的准备阶段
    /// </summary>
    public void Begin()
    {
        Manage.Instance.isEnd = false;
        Debug.Log("AI的准备阶段");
        //遍历占据的资源点，添加资源储备
        foreach (ResourceGround resourcePoint in resourcePoints)
        {
            if (resourcePoint.GetComponent<Ground>().objectControl != null && resourcePoint.GetComponent<Ground>().objectControl.tag == "BaseObject")
            {
                if (resourcePoint.resourceType == "补给")
                {
                    FoodNumberAI += resourcePoint.resourceNumber;
                }
                else if (resourcePoint.resourceType == "铁矿")
                {
                    IronNumberAI += resourcePoint.resourceNumber;
                }
            }
        }
        //计算场上已有士兵所需的本回合消耗，若大于已有资源，撤掉消耗补给最少的，直到资源足够
        while (perConsumedFood > FoodNumberAI)
        {
            int minConsumedNumber = 0;
            //记得在士兵销毁时，加一个 语句撤销每回合消耗
            //遍历所有enemys，找到消耗补给最少的
            foreach (GameObject enemy in Enemys)
            {
                if (enemy.GetComponent<BaseObject>().data.perCost["补给"] < Enemys[minConsumedNumber].GetComponent<BaseObject>().data.perCost["补给"])
                {
                    minConsumedNumber = Enemys.IndexOf(enemy);
                }
            }
            //销毁该士兵
            Destroy(Enemys[minConsumedNumber]);
        }
        while (perConsumedIron > IronNumberAI)
        {
            int minConsumedNumber = 0;
            //记得在士兵销毁时，加一个 语句撤销每回合消耗
            //遍历所有enemys，找到消耗补给最少的
            foreach (GameObject enemy in Enemys)
            {
                if (enemy.GetComponent<BaseObject>().data.perCost["铁矿"] < Enemys[minConsumedNumber].GetComponent<BaseObject>().data.perCost["铁矿"])
                {
                    minConsumedNumber = Enemys.IndexOf(enemy);
                }
            }
            //销毁该士兵
            Destroy(Enemys[minConsumedNumber]);
        }
        Draw();
    }
    private List<GameObject> cards = new List<GameObject>(); //AI的手牌
    /// <summary>
    /// AI的抽牌阶段
    /// </summary>
    public void Draw()
    {
        Debug.Log("AI的抽牌阶段");
        //抽牌，5张资源，3张主卡
        for (int i = 0; i < 3; i++)
        {
            // Debug.Log("第"+i+"张士兵卡");
            // Debug.Log("还剩"+notresourceCard.Count);
            if (notresourceCard.Count > 0)
            {
                // Debug.Log("抽到了卡牌");
                cards.Add(notresourceCard[0]);
                notresourceCard.RemoveAt(0);
                HandCardNum++;
            }
            else
            {
                break;
            }
        }
        for (int i = 0; i < 5; i++)
        {
            // Debug.Log("第" + i + "张资源卡");
            // Debug.Log("还剩"+resourceCard.Count+"张资源卡");
            if (resourceCard.Count > 0)
            {
                foodNumberAI += resourceCard[0].GetComponentInChildren<ResourceCardShow>().resourceCardData.resourceType["补给"];
                ironNumberAI += resourceCard[0].GetComponentInChildren<ResourceCardShow>().resourceCardData.resourceType["铁矿"];
                resourceCard.RemoveAt(0);
            }
            else
            {
                break;
            }
        }
        PlayCards();
    }
    [Tooltip("本变量用于存放AI的士兵对应的预制体")]
    [SerializeField]
    private GameObject enemyObject; //AI士兵对应的预制体
    /// <summary>
    /// AI的出牌阶段
    /// </summary>
    public void PlayCards()
    {
        Debug.Log("AI的出牌阶段");
        if (HandCardNum != 0)
        {
            foreach (GameObject card in cards)
            {
                //判断资源是否足够
                if (foodNumberAI >= card.GetComponent<BaseObject>().data.cost["补给"] && ironNumberAI >= card.GetComponent<BaseObject>().data.cost["铁矿"])
                {
                    if (!StaticGround.Instance.grounds[1, 3].GetComponent<Ground>().isHaveObject)
                    {
                        Set(enemyObject, 1, 3, card.GetComponent<BaseObject>().data);
                    }
                    else if (!StaticGround.Instance.grounds[0, 2].GetComponent<Ground>().isHaveObject)
                    {
                        Set(enemyObject, 0, 2, card.GetComponent<BaseObject>().data);
                    }
                    else if (!StaticGround.Instance.grounds[0, 4].GetComponent<Ground>().isHaveObject)
                    {
                        Set(enemyObject, 0, 4, card.GetComponent<BaseObject>().data);
                    }
                }
            }
            Action();
        }
    }
    /// <summary>
    /// AI的行动阶段
    /// </summary>
    public void Action()
    {
        Debug.Log("AI的行动阶段");
        End();
    }

    /// <summary>
    /// AI的结束阶段
    /// </summary>
    public void End()
    {
        Debug.Log("AI的结束阶段");
        //当手牌大于上限时，弃掉手牌
        while (HandCardNum > HandCardNumMax)
        {
            if (isHavePitfallCard)
            {
                //弃掉陷阱卡或者策略卡
                //检测陷阱卡或者策略卡是否还有
                //遍历cards，找到陷阱卡或者策略卡
                //销毁该卡
            }
            else
            {
                int minConsumedNumber = 0;
                //比较部署所需补给数，丢掉补给数最小的兵种卡
                foreach (GameObject card in cards)
                {
                    if (card.GetComponent<BaseObject>().data.cost["补给"] < cards[minConsumedNumber].GetComponent<BaseObject>().data.perCost["补给"])
                    {
                        minConsumedNumber = cards.IndexOf(card);
                    }
                }
                cards.RemoveAt(minConsumedNumber);
                HandCardNum--;
            }
        }
        Manage.Instance.Begin();
    }
    /// <summary>
    /// 将卡牌设置到场上
    /// </summary>
    public void Set(GameObject enemyObject, int rowInput, int columnInput, SoldierCardData soldierCardData)
    {
        GameObject enemy = Instantiate(enemyObject);
        enemy.transform.position = StaticGround.Instance.grounds[rowInput, columnInput].transform.position+new Vector3(0,0,-1);
        enemy.GetComponent<BaseObject>().row = rowInput;
        enemy.GetComponent<BaseObject>().column = columnInput;
        Enemys.Add(enemy);
        foodNumberAI -= enemy.GetComponent<BaseObject>().data.cost["补给"];
        ironNumberAI -= enemy.GetComponent<BaseObject>().data.cost["铁矿"];
        StaticGround.Instance.grounds[rowInput, columnInput].GetComponent<Ground>().isHaveObject = true;
        StaticGround.Instance.grounds[rowInput, columnInput].GetComponent<Ground>().objectControl = enemy;
    }
}