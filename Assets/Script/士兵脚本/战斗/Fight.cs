using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fight :BaseObject
{
    [HideInInspector]
    public int attackNumber;             //本回合攻击次数
    private float HP;                      // 生命值
    public float hp
    {
        get { return HP; }
        set
        {
            if (value < 0)
            {
                Destroy(this.gameObject);
            }
            else
            {
                HP = value;
            }
        }
    }
    [HideInInspector]
    public int attack;                    //攻击力
    [HideInInspector]
    public int defense;                   //防御力
    [HideInInspector]
    public int attackDistance;           //攻击距离
    bool isAttacking = true; //是否是攻击状态
    /// <summary>
    /// 本函数用于改变攻击状态
    /// </summary>
    public void ChangeState()
    {
        isAttacking = !isAttacking;
    }
    [SerializeField] Image HealthBar; // 血条
    private void Start()
    {
        if (HealthBar != null)
            HealthBar.transform.position = Camera.main.WorldToScreenPoint(this.transform.position);
        attackNumber = data.maxAttackNumber;
        hp = data.health;
        attack = data.attack;
        defense = data.defense;
        attackDistance = data.attackDistance;
    }
    public List<GameObject> attackObject { get ;private set; }= new List<GameObject>();//攻击对象

    /// <summary>
    ///更新血量
    /// </summary>
    public void updateHP(float damage)
    {
        this.hp -= damage;
        // Debug.Log(this.hp);
        //更新ui
    }

    public bool IsAllowAttack()
    {
        if (isAttacking && (attackNumber > 0))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    /// <summary>
    /// 获取攻击距离内的敌人
    /// </summary>
    /// <returns></returns>
    public List<GameObject> GetAttackDistanceObject(string tagInput)
    {
        List<GameObject> enemy = new List<GameObject>();
        for (int i = 1; i <= data.attackDistance; i++)
        {
            if (row + i < CollectionOfConstants.MAPROW && StaticGround.Instance.grounds[row + i, column].GetComponent<Ground>().objectControl != null && StaticGround.Instance.grounds[row + i, column].GetComponent<Ground>().objectControl.tag == tagInput)
            {
                enemy.Add(StaticGround.Instance.grounds[row + i, column].GetComponent<Ground>().objectControl);
            }
            else if (row - i >= 0 && StaticGround.Instance.grounds[row - i, column].GetComponent<Ground>().objectControl != null && StaticGround.Instance.grounds[row - i, column].GetComponent<Ground>().objectControl.tag == tagInput)
            {
                enemy.Add(StaticGround.Instance.grounds[row - i, column].GetComponent<Ground>().objectControl);
            }
            else if (column + i < CollectionOfConstants.MAPCOLUMN && StaticGround.Instance.grounds[row, column + i].GetComponent<Ground>().objectControl != null && StaticGround.Instance.grounds[row, column + i].GetComponent<Ground>().objectControl.tag == tagInput)
            {
                enemy.Add(StaticGround.Instance.grounds[row, column + i].GetComponent<Ground>().objectControl);
            }
            else if (column - i >= 0 && StaticGround.Instance.grounds[row, column - i].GetComponent<Ground>().objectControl != null && StaticGround.Instance.grounds[row, column - i].GetComponent<Ground>().objectControl.tag == tagInput)
            {
                enemy.Add(StaticGround.Instance.grounds[row, column - i].GetComponent<Ground>().objectControl);
            }
        }
        return enemy;
    }














    /// <summary>
    /// 受伤
    /// </summary>
    public void Hurt(Fight fight)
    {
        if (fight.isAttacking)
        {
            updateHP(computeDamageInAttack(fight.data.defense, this.data.attack));
        }
        else
        {
            updateHP(computeDamageInDefense(fight.data.defense, this.data.attack));
        }
    }

    ///<summary>
    ///计算对方处于防御状态时的伤害数值
    ///<summary>
    public float computeDamageInDefense(int defense, int attack)
    {
        return attack * attack / (attack + (float)4.5 * defense);
    }

    ///<summary>
    ///计算对方处于攻击状态时的伤害数值
    ///<summary>
    public float computeDamageInAttack(int defense, int attack)
    {
        return attack * attack / (attack + 2.0f * defense);
    }
}
