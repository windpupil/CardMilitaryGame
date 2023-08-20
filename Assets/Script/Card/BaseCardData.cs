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
    [SerializedDictionary("消耗类型", "消耗值")]
    public SerializedDictionary<string, int> cost = new SerializedDictionary<string, int>();  //定义一个字典变量存储消耗类型和消耗值
    public GameObject gameObject;                   // 对应生成游戏对象
    public int counts;                              //可放置区域的数量
    public int[] locationRow;                       //可放置区域的下标数组的行
    public int[] locationColumn;                    //可放置区域的下标数组的列
    public int perRoundCost;                        //每回合消耗
}