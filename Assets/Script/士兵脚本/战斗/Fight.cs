using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BaseObject))]
public class Fight: MonoBehaviour
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
    public List<GameObject> attackableObjects { get; private set; } = new List<GameObject>(); // 可攻击的对象
    /// <summary>
    /// 获取攻击距离内的敌人
    /// </summary>
    public void GetAttackDistanceObject(string tagInput)
    {
        Debug.Log(this.GetComponent<BaseObject>().row + "," + this.GetComponent<BaseObject>().column);
        attackableObjects.Clear();
        for (int i = 1; i <= this.GetComponent<BaseObject>().data.attackDistance; i++)
        {
            if (this.GetComponent<BaseObject>().row + i < CollectionOfConstants.MAPROW && StaticGround.Instance.grounds[this.GetComponent<BaseObject>().row + i, this.GetComponent<BaseObject>().column].GetComponent<Ground>().objectControl != null && StaticGround.Instance.grounds[this.GetComponent<BaseObject>().row + i, this.GetComponent<BaseObject>().column].GetComponent<Ground>().objectControl.tag == tagInput)
            {
                attackableObjects.Add(StaticGround.Instance.grounds[this.GetComponent<BaseObject>().row + i, this.GetComponent<BaseObject>().column].GetComponent<Ground>().objectControl);
            }
            else if (this.GetComponent<BaseObject>().row - i >= 0 && StaticGround.Instance.grounds[this.GetComponent<BaseObject>().row - i, this.GetComponent<BaseObject>().column].GetComponent<Ground>().objectControl != null && StaticGround.Instance.grounds[this.GetComponent<BaseObject>().row - i, this.GetComponent<BaseObject>().column].GetComponent<Ground>().objectControl.tag == tagInput)
            {
                attackableObjects.Add(StaticGround.Instance.grounds[this.GetComponent<BaseObject>().row - i, this.GetComponent<BaseObject>().column].GetComponent<Ground>().objectControl);
            }
            else if (this.GetComponent<BaseObject>().column + i < CollectionOfConstants.MAPCOLUMN && StaticGround.Instance.grounds[this.GetComponent<BaseObject>().row, this.GetComponent<BaseObject>().column + i].GetComponent<Ground>().objectControl != null && StaticGround.Instance.grounds[this.GetComponent<BaseObject>().row, this.GetComponent<BaseObject>().column + i].GetComponent<Ground>().objectControl.tag == tagInput)
            {
                attackableObjects.Add(StaticGround.Instance.grounds[this.GetComponent<BaseObject>().row, this.GetComponent<BaseObject>().column + i].GetComponent<Ground>().objectControl);
            }
            else if (this.GetComponent<BaseObject>().column - i >= 0 && StaticGround.Instance.grounds[this.GetComponent<BaseObject>().row, this.GetComponent<BaseObject>().column - i].GetComponent<Ground>().objectControl != null && StaticGround.Instance.grounds[this.GetComponent<BaseObject>().row, this.GetComponent<BaseObject>().column - i].GetComponent<Ground>().objectControl.tag == tagInput)
            {
                attackableObjects.Add(StaticGround.Instance.grounds[this.GetComponent<BaseObject>().row, this.GetComponent<BaseObject>().column - i].GetComponent<Ground>().objectControl);
            }
        }
    }

    /// <summary>
    /// 本函数用于受伤
    /// </summary>
    /// <param name="fight">传递攻击者的数值</param>
    public void Hurt(Fight fight)
    {
        if (this.isAttacking)
        {
            updateHP(ComputeDamageInAttack(this.GetComponent<BaseObject>().data.defense, fight.gameObject.GetComponent<BaseObject>().data.attack));
        }
        else
        {
            updateHP(ComputeDamageInDefense(this.GetComponent<BaseObject>().data.defense, fight.gameObject.GetComponent<BaseObject>().data.attack));
        }
    }
    /// <summary>
    ///更新血量
    /// </summary>
    public void updateHP(float damage)
    {
        hp -= damage;
        // Debug.Log("扣了"+damage+"点血");
        GameObject.Find("Canvas/HealthBar").GetComponent<HPShow>().updateHPUI();

    }
    ///<summary>
    ///计算对方处于防御状态时的伤害数值
    ///<summary>
    public float ComputeDamageInDefense(int defense, int attack)
    {
        return attack * attack / (attack + (float)4.5 * defense);
    }
    ///<summary>
    ///计算对方处于攻击状态时的伤害数值
    ///<summary>
    public float ComputeDamageInAttack(int defense, int attack)
    {
        return attack * attack / (attack + 2.0f * defense);
    }
    /// <summary>
    /// 更新攻击次数
    /// </summary>
    public void updateObjectsControlAttackNumber()
    {
        attackNumber = this.GetComponent<BaseObject>().data.maxAttackNumber;
    }
}
