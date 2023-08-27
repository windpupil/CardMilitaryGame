using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 本脚本是游戏流程的控制脚本
/// </summary>
public class Manage : MonoBehaviour
{
    [Tooltip("本变量用于存放所有资源点")]
    public static List<GameObject> resourcePoints = new List<GameObject>(); //资源点

    [Tooltip("本变量用于存放所有资源卡牌")]
    public List<GameObject> resourceCard = new List<GameObject>();

    [Tooltip("本变量用于存放所有非资源卡牌")]
    public List<GameObject> notresourceCard = new List<GameObject>();
    public static int rounds = 1; //回合数

    [Tooltip("本变量用于存放Canvas")]
    public Transform Canvas; //Canvas

    private static Manage instance;
    public static Manage Instance
    {
        get { return instance; }
    }

    private void Start()
    {
        instance = this;
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
        //遍历所有资源点，将资源点的资源数加到资源UI上
        for (int i = 0; i < resourcePoints.Count; i++)
        {
            if (resourcePoints[i] != null)
            {
                resourcePoints[i].GetComponent<ResourceGround>().AddResource();
            }
        }

        if (CollectionOfConstants.isEnough())
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
        if (HandCard.Instance.HandCardCounts > CollectionOfConstants.HandCardLimit)
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
            rounds++;
            StaticGround.Instance.updateObjectsControlRealDistance(); //更新物体的realDistance
            StaticGround.Instance.updateSoldierAttackNumber(); //更新物体的attackNumber
            Begin();
        }
    }

    public void resourceDecisionEnd()
    {
        Debug.Log("资源决策阶段结束");
        ResourceNumberUI.Instance.FoodNumber -= CollectionOfConstants.SuppliesConsumedPerTurn;
        ResourceNumberUI.Instance.IronNumber -= CollectionOfConstants.IronConsumedPerTurn;
        ResourceNumberUI.Instance.updateResourceNumberText();
        if (
            CollectionOfConstants.actionNumberLimit
                >= ActionNumberUI.Instance.actionNumberCurrentLimit
            && rounds != 1
        )
        {
            ActionNumberUI.Instance.actionNumberCurrentLimit++;
        }
        ActionNumberUI.Instance.actionNumber = ActionNumberUI.Instance.actionNumberCurrentLimit;
        ActionNumberUI.Instance.updateActionNumberText();
        isBegin = false;
        //抽卡
        Draw();
    }
}
