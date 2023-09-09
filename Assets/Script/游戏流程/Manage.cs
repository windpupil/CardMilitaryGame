using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 本脚本是游戏流程的控制脚本
/// </summary>
public class Manage : MonoBehaviour
{
    [Tooltip("本变量用于存放所有资源点")]
    public List<ResourceGround> resourcePoints = new List<ResourceGround>();
    /// <summary>
    /// 本方法用于将资源点加入到resourcePoints中
    /// </summary>
    /// <param name="resourceGround"></param>
    public void AddResourceGround(ResourceGround resourceGround)
    {
        resourcePoints.Add(resourceGround);
    }
    [Tooltip("本变量用于存放所有资源卡牌")]
    public List<GameObject> resourceCard = new List<GameObject>();

    [Tooltip("本变量用于存放所有非资源卡牌")]
    public List<GameObject> notresourceCard = new List<GameObject>();
    private int rounds = 1; //回合数
    public int Rounds
    {
        get { return rounds; }
        set
        {
            if (value != rounds)
            {
                StaticGround.Instance.updateSoldierStateButton();
                rounds = value;
            }
        }
    }
    [Tooltip("本变量用于存放Canvas")]
    public Transform Canvas; //Canvas

    private static Manage instance;
    public static Manage Instance
    {
        get { return instance; }
    }

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
        Begin();
    }

    [HideInInspector]
    public bool isBegin = false; //是否在准备阶段

    [Tooltip("本变量用于存放每回合消耗的资源不足提示")]
    [SerializeField]
    private GameObject resourcesPerTurnNotEnough; //资源不足提示

    [HideInInspector]
    public bool isResourceEnoughStage = false; //是否资源足够的阶段

    /// <summary>
    /// 准备阶段
    /// </summary>
    public void Begin()
    {
        Debug.Log("游戏准备阶段开始");
        isEnd = false;
        isBegin = true;
        //更新士兵的realDistance
        

        
        //遍历所有资源点，将资源点的资源数加到资源UI上
        foreach (ResourceGround resourcePoint in resourcePoints)
        {
            if (resourcePoint.GetComponent<Ground>().objectControl != null && resourcePoint.GetComponent<Ground>().objectControl.tag == "Soldier")
            {
                if (resourcePoint.resourceType == "补给")
                {
                    ResourceNumberUI.Instance.FoodNumber += resourcePoint.resourceNumber;
                }
                else if (resourcePoint.resourceType == "铁矿")
                {
                    ResourceNumberUI.Instance.IronNumber += resourcePoint.resourceNumber;
                }
            }
        }
        ResourceNumberUI.Instance.updateResourceNumberText();
        if (isEnough())
        {
            resourceDecisionEnd();
        }
        else
        {
            isResourceEnoughStage = true;
            //生成提示，材料不足
            GameObject tip = Instantiate(
                resourcesPerTurnNotEnough,
                Canvas.position,
                Quaternion.identity
            );
            tip.transform.SetParent(Canvas);
            Destroy(tip, 2.5f);
        }
    }

    /// <summary>
    /// 抽卡阶段
    /// </summary>
    [Tooltip("本变量用于存放抽卡界面")]
    [SerializeField]
    private GameObject UIDraw; //抽卡界面

    [Tooltip("本变量用于存放牌库已空提示")]
    [SerializeField]
    private GameObject tipCardIsNull; //"你的牌库已空对话框"

    public void Draw()
    {
        Debug.Log("游戏抽卡阶段开始");
        if (resourceCard.Count == 0 && notresourceCard.Count == 0)
        {
            //生成提示，并在5秒后销毁
            GameObject tip = Instantiate(tipCardIsNull, Canvas.position, Quaternion.identity);
            tip.transform.SetParent(GameObject.Find("Canvas").transform);
            Destroy(tip, 5);
            Action();
            return;
        }
        //打开抽卡界面
        UIDraw.SetActive(true);
        if (DrawCardCountsUI.Instance != null)
        {
            DrawCardCountsUI.Instance.UpdateCounts();
            DrawCardCountsUI.Instance.updateResourceCardCountsText();
        }
    }

    /// <summary>
    /// 行动阶段
    /// </summary>
    [HideInInspector]
    public bool isAction = false; //是否在行动阶段

    public void Action()
    {
        isAction = true;
        Debug.Log("游戏行动阶段开始");
    }

    [Tooltip("本变量用于存放手牌数量超过上限的提示")]
    [SerializeField]
    private GameObject handCardtip;

    [HideInInspector]
    public bool isEnd = false; //是否在结算阶段

    /// <summary>
    /// 结算阶段
    /// </summary>
    public void End()
    {
        //检测handCard的子物体数量是否大于10
        if (HandCard.Instance.HandCardCounts > CollectionOfConstants.HANDCARDLIMIT)
        {
            //生成提示
            GameObject tip = Instantiate(handCardtip, Canvas.position, Quaternion.identity);
            tip.transform.SetParent(Canvas);
            Destroy(tip, 2.5f);
        }
        else
        {
            Debug.Log("游戏结算阶段开始");
            isAction = false;
            isEnd = true;
            Rounds++;
            // StaticGround.Instance.updateObjectsControlRealDistance(); //更新物体的realDistance
            StaticGround.Instance.updateSoldierAttackNumber(); //更新物体的attackNumber
            AIBrain.Instance.Begin();
        }
    }

    public void resourceDecisionEnd()
    {
        Debug.Log("资源决策阶段结束");
        ResourceNumberUI.Instance.FoodNumber -= SuppliesConsumedPerTurn;
        ResourceNumberUI.Instance.IronNumber -= IronConsumedPerTurn;
        ResourceNumberUI.Instance.updateResourceNumberText();
        if (CollectionOfConstants.ACTIONNUMBERLIMIT >= ActionNumberUI.Instance.actionNumberCurrentLimit && Rounds != 1)
        {
            ActionNumberUI.Instance.actionNumberCurrentLimit++;
        }
        ActionNumberUI.Instance.actionNumber = ActionNumberUI.Instance.actionNumberCurrentLimit;
        ActionNumberUI.Instance.updateActionNumberText();
        isBegin = false;
        //抽卡
        Draw();
    }

    private int suppliesConsumedPerTurn = 0; // 每回合消耗补给数
    public int SuppliesConsumedPerTurn
    {
        get { return suppliesConsumedPerTurn; }
        set
        {
            if (Manage.Instance.isResourceEnoughStage)
            {

                if (isEnough())
                {
                    Debug.Log(3);
                    Manage.Instance.isResourceEnoughStage = false;
                    Manage.Instance.resourceDecisionEnd();
                }
            }
            suppliesConsumedPerTurn = value;
        }
    }
    private int ironConsumedPerTurn = 0; // 每回合消耗铁矿数
    public int IronConsumedPerTurn
    {
        get { return ironConsumedPerTurn; }
        set
        {
            if (Manage.Instance.isResourceEnoughStage)
            {
                if (isEnough())
                {
                    Manage.Instance.isResourceEnoughStage = false;
                    Manage.Instance.resourceDecisionEnd();
                }
            }
            ironConsumedPerTurn = value;
        }
    }
    public bool isEnough()
    {
        // Debug.Log("食物数量是：" + ResourceNumberUI.Instance.FoodNumber);
        // Debug.Log("铁矿数量是：" + ResourceNumberUI.Instance.IronNumber);
        // Debug.Log("每回合消耗的食物数量是：" + SuppliesConsumedPerTurn);
        // Debug.Log("每回合消耗的铁矿数量是：" + IronConsumedPerTurn);
        if (ResourceNumberUI.Instance.FoodNumber >= SuppliesConsumedPerTurn && ResourceNumberUI.Instance.IronNumber >= IronConsumedPerTurn)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    //观察者模式,但是暂时用不到
    // private List<IObserver> observers = new List<IObserver>();
    // //添加观察者
    // public void AddObserver(IObserver observer)
    // {
    //     observers.Add(observer);
    // }
    // //移除观察者
    // public void RemoveObserver(IObserver observer)
    // {
    //     observers.Remove(observer);
    // }
    // //发送通知给观察者
    // public void Notify()
    // {
    //     for (int i = 0; i < observers.Count; i++)
    //     {
    //         observers[i]?.ResponseToNotify();
    //     }
    // }
}
