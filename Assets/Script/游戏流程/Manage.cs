using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manage : MonoBehaviour
{
    public static Dictionary<string, int> resourceNumber = new Dictionary<string, int>();      //存储资源点增加的资源数量
    public List<GameObject> resourceCard;
    public List<GameObject> notresourceCard;
    public static int rounds=1;                     //回合数

    private void Start() {
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
        //根据是否占领资源点来决定资源增加
        foreach (var item in resourceNumber)
        {
            if(item.Key == "Food")
            {
                ResourceNumberUI.FoodNumber += item.Value;
            }
            else if(item.Key == "Wood")
            {
                ResourceNumberUI.WoodNumber += item.Value;
            }
            else if(item.Key == "Iron")
            {
                ResourceNumberUI.IronNumber += item.Value;
            }
            ResourceNumberUI.updateResourceNumberText();
        }
        //行动点数恢复
        if (ActionNumberUI.actionNumberLimit >= ActionNumberUI.actionNumberCurrentLimit&&rounds!=1)
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
            GameObject tip = Instantiate(tipCardIsNull, new Vector3(0, 0, 0), Quaternion.identity);
            tip.transform.SetParent(GameObject.Find("Canvas").transform);
            Destroy(tip, 5);
            Action();
            return;
        }
        //打开抽卡界面
        UIDraw.SetActive(true);
        DrawCardCountsUI.resourceCardCountsCurrent=DrawCardCountsUI.resourceCardCountsLimit;
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
        Begin();
    }
}
