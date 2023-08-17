using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObject/兵种数据", order = 0)]
public class SoldierCardData : BaseCardData
{
    public int attack=0;                // 攻击力
    public int health                 // 生命值
    {
        get { return health; }
        set
        {
            if (value < 0)
            {
                health = 0;
            }
            else
            {
                health = value;
            }
        }
    }
    public int distance=0;              // 攻击距离
    public int defense=0;               // 防御力
}