using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCardData : ScriptableObject
{
    public string cardName;                 // 卡牌名称
    public string cardDescription;          // 卡牌描述
    public Sprite cardImage;                // 卡牌图片
    public string cardType;                 // 卡牌类型
    public Transform[] arrangedAreas;       // 该卡牌可以被安排到的区域
}