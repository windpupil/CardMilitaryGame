using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fight : MonoBehaviour
{
    public ObjectsControl data;
    private static Fight instance;
    public static Fight Instance
    {
        get { return instance; }
    }

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
    private List<GameObject> enemy=new List<GameObject>();    //敌人的列表
    private void Start()
    {
        instance = this;
        //初始化攻击次数
        attackNumber = data.cardData.maxAttackNumber;
        //初始化生命值
        hp = data.cardData.health;
        //初始化攻击力
        attack = data.cardData.attack;
        //初始化防御力
        defense = data.cardData.defense;
    }

    private bool isAttacking = true;   //是否是攻击状态

    /// <summary>
    /// 检测攻击范围内是否有敌人,只有上下左右四个方向
    /// </summary>
    public void IsHaveEnemy()
    {
        enemy.Clear();
        for (int i = 1; i <= data.cardData.attackDistance; i++)
        {
            // if(StaticGround.Instance.grounds[data.row - i, data.column].GetComponent<Ground>().objectControl!=null)
            // {
            //     Debug.Log(StaticGround.Instance.grounds[data.row - i, data.column].GetComponent<Ground>().objectControl.tag);
            // }
            // else
            // {
            //     Debug.Log("null");
            // }
            // Debug.Log(data.row - i);
            // Debug.Log(data.column);
            if(data.row+i<CollectionOfConstants.MapRow&&StaticGround.Instance.grounds[data.row+i,data.column].GetComponent<Ground>().objectControl!=null&&StaticGround.Instance.grounds[data.row+i,data.column].GetComponent<Ground>().objectControl.tag=="Enemy")
            {
                enemy.Add(StaticGround.Instance.grounds[data.row + i, data.column].GetComponent<Ground>().objectControl);
            }
            if (data.row - i >= 0 && StaticGround.Instance.grounds[data.row - i, data.column].GetComponent<Ground>().objectControl != null && StaticGround.Instance.grounds[data.row - i, data.column].GetComponent<Ground>().objectControl.tag == "Enemy")
            {
                enemy.Add(StaticGround.Instance.grounds[data.row - i, data.column].GetComponent<Ground>().objectControl);
            }
            if (data.column + i < CollectionOfConstants.MapColumn && StaticGround.Instance.grounds[data.row, data.column + i].GetComponent<Ground>().objectControl != null && StaticGround.Instance.grounds[data.row, data.column + i].GetComponent<Ground>().objectControl.tag == "Enemy")
            {
                enemy.Add(StaticGround.Instance.grounds[data.row, data.column + i].GetComponent<Ground>().objectControl);
            }
            if (data.column - i >= 0 && StaticGround.Instance.grounds[data.row, data.column - i].GetComponent<Ground>().objectControl != null && StaticGround.Instance.grounds[data.row, data.column - i].GetComponent<Ground>().objectControl.tag == "Enemy")
            {
                enemy.Add(StaticGround.Instance.grounds[data.row, data.column - i].GetComponent<Ground>().objectControl);
            }
        }
    }

    /// <summary>
    /// 受伤
    /// </summary>
    public void Hurt(Enemy enemy)
    {
        if (enemy.isAttacking)
        {
            enemy.updateHP(computeDamageInAttack(enemy.defense,this.attack));
        }
        else
        {
            enemy.updateHP(computeDamageInDefense(enemy.defense,this.attack));
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
        return attack*attack/(attack+2.0f*defense);
    }

    /// <summary>
    ///更新血量
    /// </summary>
    public void updateHP(float damage)
    {
        this.hp -= damage;
        // Debug.Log(this.hp);
        //更新ui
    }

    private RaycastHit2D hit;
    private void OnMouseDown() {
        hit= new RaycastHit2D();
        if (isAttacking&&attackNumber>0)
        {
            IsHaveEnemy();
            //如果有敌人，显示箭头
        }
    }
    private void OnMouseDrag() {
        //射线检测，输出碰撞点的位置
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
    }
    private void OnMouseUp() {
        if (isAttacking&&(attackNumber>0))
        {
            // Debug.Log(hit.collider.gameObject.name);
            // Debug.Log(enemy.Count);
            if (hit.collider != null)
            {
                for (int i = 0; i < enemy.Count; i++)
                {
                    Debug.Log(enemy[i].name);
                    if (hit.collider.gameObject == enemy[i])
                    {
                        Debug.Log("攻击敌人");
                        Hurt(enemy[i].GetComponent<Enemy>());
                        attackNumber--;
                    }
                }
            }
        }
    }

    public bool IsAllowAttack()
    {
        if (isAttacking&&(attackNumber>0))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
