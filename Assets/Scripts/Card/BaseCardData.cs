using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AYellowpaper.SerializedCollections;

public class BaseCardData : ScriptableObject
{
    public string cardName;                         // 卡牌名称
    public string cardDescription;                  // 卡牌描述
    public Sprite cardImage;                        // 卡牌图片
    public string cardType;                         // 卡牌类型
    [SerializedDictionary("部署消耗类型", "部署消耗值")]
    public SerializedDictionary<string, int> cost = new SerializedDictionary<string, int>();  //定义一个字典变量存储消耗类型和消耗值
    [SerializedDictionary("每回合消耗类型", "每回合消耗值")]
    public SerializedDictionary<string, int> perCost = new SerializedDictionary<string, int>();  //定义一个字典变量存储每回合消耗类型和每回合消耗值
    public GameObject gameObject;                   // 对应生成游戏对象
    public int counts;                              //可放置区域的数量
    public int[] locationRow;                       //可放置区域的下标数组的行
    public int[] locationColumn;                    //可放置区域的下标数组的列
}