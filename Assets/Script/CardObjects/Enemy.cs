using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Enemy : MonoBehaviour
{
    public SoldierCardData cardData;
    public int row;                        // 行
    public int column;                     // 列

    public int attackNumber ;               //本回合攻击次数
    private float HP;                                     // 生命值
    public float hp
    {
        get { return HP; }
        set
        {
            if (value < 0)
            {
                HP = 0;
            }
            else
            {
                HP = value;
            }
        }
    }
    public int attack ;                               // 攻击力
    public int defense ;                            // 防御力
    private void Awake()
    {
        //初始化攻击次数
        attackNumber = cardData.maxAttackNumber;
        //初始化生命值
        hp = cardData.health;
        //初始化攻击力
        attack = cardData.attack;
        //初始化防御力
        defense = cardData.defense;
    }

    public bool isAttacking = true;   //是否是攻击状态

    /// <summary>
    /// 受伤
    /// </summary>
    public void Hurt(Enemy enemy)
    {
        if (enemy.isAttacking)
        {
            updateHP(computeDamageInAttack(enemy.defense,this.attack));
        }
        else
        {
            updateHP(computeDamageInDefense(enemy.defense,this.attack));
        }
    }

    ///<summary>
    ///计算对方处于防御状态时的伤害数值
    ///<summary>
    public float computeDamageInDefense(int defense,int attack)
    {
        return attack*attack/(attack+(float)4.5*defense);
    }

    ///<summary>
    ///计算对方处于攻击状态时的伤害数值
    ///<summary>
    public float computeDamageInAttack(int defense,int attack)
    {
        return attack^attack/(attack+2*defense);
    }


    /// <summary>
    ///更新血量
    /// </summary>
    public void updateHP(float damage)
    {
        this.hp -= damage;
        Debug.Log(this.hp);
        //更新ui
    }
}
