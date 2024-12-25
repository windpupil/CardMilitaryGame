using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AYellowpaper.SerializedCollections;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObject/资源卡数据", order = 1)]
public class ResourceCardData : ScriptableObject
{
    public string cardName;                         // 卡牌资源名称
    public string cardDescription;                  // 卡牌描述
    public Sprite cardImage;                        // 卡牌图片
    public string cardType;                         // 卡牌资源类型
    [SerializedDictionary("资源类型", "资源值")]
    public SerializedDictionary<string, int> resourceType = new SerializedDictionary<string, int>();  //定义一个字典变量存储资源类型和资源值
}
