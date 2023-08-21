using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObject/兵种数据", order = 0)]
public class SoldierCardData : BaseCardData
{
    public int attack = 0;                // 攻击力
    public int defense = 0;               // 防御力
    private int Health = 0;                // 生命值,仅为了输入
    public int health                 // 生命值限制
    {
        get { return Health; }
        set
        {
            if (value < 0)
            {
                Health = 0;
            }
            else
            {
                Health = value;
            }
        }
    }
    public int attackDistance = 0;              // 攻击距离
    public int moveDistance = 0;                 // 移动距离
    public int maxAttackNumber;                     //最大攻击次数
}