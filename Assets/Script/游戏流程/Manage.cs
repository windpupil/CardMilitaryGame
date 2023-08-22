using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 本脚本是游戏流程的控制脚本
/// </summary>
public class Manage : MonoBehaviour
{
    public static List<GameObject> resourcePoints=new List<GameObject>();      //资源点
    public List<GameObject> resourceCard=new List<GameObject>();
    public List<GameObject> notresourceCard=new List<GameObject>();
    public static int rounds = 1;                     //回合数
    public Transform Canvas;                         //Canvas

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


    public static bool isBegin = false;             //是否在准备阶段
    /// <summary>
    /// 准备阶段
    /// </summary>
    public void Begin()
    {
        Debug.Log("游戏准备阶段开始");
        isBegin = true;
        //遍历所有资源点，将资源点的资源数加到资源UI上
        for (int i = 0; i < resourcePoints.Count; i++)
        {
            resourcePoints[i].GetComponent<ResourceGround>().AddResource();
        }
        //行动点数恢复
        if (ActionNumberUI.actionNumberLimit >= ActionNumberUI.actionNumberCurrentLimit && rounds != 1)
        {
            ActionNumberUI.actionNumberCurrentLimit++;
        }
        ActionNumberUI.actionNumber = ActionNumberUI.actionNumberCurrentLimit;
        ActionNumberUI.updateActionNumberText();
        isBegin = false;
        //抽卡
        Draw();
    }

    /// <summary>
    /// 抽卡阶段
    /// </summary>
    [SerializeField]
    public GameObject UIDraw;         //抽卡界面
    [SerializeField] public GameObject tipCardIsNull;       //"你的牌库已空对话框"
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
        DrawCardCountsUI.resourceCardCountsCurrent = DrawCardCountsUI.resourceCardCountsLimit;
        DrawCardCountsUI.updateResourceCardCountsText();
    }


    /// <summary>
    /// 行动阶段
    /// </summary>
    public static bool isActionFinish = false;      //是否行动阶段结束
    public void Action()
    {
        Debug.Log("游戏行动阶段开始");
    }


    public static bool isEnd = false;               //是否在结算阶段
    /// <summary>
    /// 结算阶段
    /// </summary>
    public void End()
    {
        Debug.Log("游戏结算阶段开始");
        isActionFinish = false;
        isEnd = true;
        rounds++;
        StaticGround.updateObjectsControlRealDistance();     //更新物体的realDistance
        StaticGround.updateSoldierAttackNumber();      //更新物体的attackNumber
        Begin();
    }


}
