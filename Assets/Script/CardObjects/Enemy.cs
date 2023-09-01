using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public SoldierCardData cardData;
    private static Enemy instance;
    public static Enemy Instance
    {
        get { return instance; }
    }
    public int row; // 行
    public int column; // 列
    private float HP; // 生命值
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

    [SerializeField]
    private Image HealthBar; // 血条
    public int realDistance; // 回合中实际剩余步数


    private void Start()
    {
        instance = this;
        if(HealthBar != null)
        HealthBar.transform.position = Camera.main.WorldToScreenPoint(this.transform.position);
        realDistance = cardData.moveDistance;
    }

    public bool isAttacking = true; //是否是攻击状态

    /// <summary>
    /// 受伤
    /// </summary>
    public void Hurt(Enemy enemy)
    {
        if (enemy.isAttacking)
        {
            updateHP(computeDamageInAttack(enemy.cardData.defense, this.cardData.attack));
        }
        else
        {
            updateHP(computeDamageInDefense(enemy.cardData.defense, this.cardData.attack));
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

    /// <summary>
    ///更新血量
    /// </summary>
    public void updateHP(float damage)
    {
        this.hp -= damage;
        Debug.Log(damage);
        HealthBar.fillAmount = hp / cardData.health;
        //判断是否死亡
        if (hp <= 0)
        {
            //将格子状态改为无人
            StaticGround.Instance.grounds[row, column].GetComponent<Ground>().isHaveObject = false;
            StaticGround.Instance.grounds[row, column].GetComponent<Ground>().objectControl = null;
            //死亡
            Destroy(this.gameObject);
        }
    }
    /// <summary>
    /// 本函数用于改变攻击状态
    /// </summary>
    public void ChangeState()
    {
        isAttacking = !isAttacking;
    }
    /// <summary>
    /// 本函数用于初始化cardData
    /// </summary>
    public void Initializ(SoldierCardData cardDataPassed)
    {
        this.cardData = cardDataPassed;
        //初始化生命值
        hp = cardDataPassed.health;
    }
    /// <summary>
    /// 本函数用于士兵死亡后减少每回合的消耗
    /// </summary>
    private void OnDestroy()
    {
        Manage.Instance.SuppliesConsumedPerTurn -= cardData.perCost["补给"];
        Manage.Instance.IronConsumedPerTurn -= cardData.perCost["铁矿"];
    }
    /// <summary>
    /// 获取攻击距离内的敌人
    /// </summary>
    /// <returns></returns>
    public List<GameObject> GetAttackDistanceObject()
    {
        List<GameObject> enemy = new List<GameObject>();
        for (int i = 1; i <= cardData.attackDistance; i++)
        {
            if (row + i < CollectionOfConstants.MapRow && StaticGround.Instance.grounds[row + i, column].GetComponent<Ground>().objectControl != null && (StaticGround.Instance.grounds[row + i, column].GetComponent<Ground>().objectControl.tag == "Soldier" || StaticGround.Instance.grounds[row + i, column].GetComponent<Ground>().objectControl.tag == "MainCity"))
            {
                enemy.Add(StaticGround.Instance.grounds[row + i, column].GetComponent<Ground>().objectControl);
            }
            else if (row - i >= 0 && StaticGround.Instance.grounds[row - i, column].GetComponent<Ground>().objectControl != null && (StaticGround.Instance.grounds[row - i, column].GetComponent<Ground>().objectControl.tag == "Soldier" || StaticGround.Instance.grounds[row - i, column].GetComponent<Ground>().objectControl.tag == "MainCity"))
            {
                enemy.Add(StaticGround.Instance.grounds[row - i, column].GetComponent<Ground>().objectControl);
            }
            else if (column + i < CollectionOfConstants.MapColumn && StaticGround.Instance.grounds[row, column + i].GetComponent<Ground>().objectControl != null && (StaticGround.Instance.grounds[row, column + i].GetComponent<Ground>().objectControl.tag == "Soldier" || StaticGround.Instance.grounds[row, column + i].GetComponent<Ground>().objectControl.tag == "MainCity"))
            {
                enemy.Add(StaticGround.Instance.grounds[row, column + i].GetComponent<Ground>().objectControl);
            }
            else if (column - i >= 0 && StaticGround.Instance.grounds[row, column - i].GetComponent<Ground>().objectControl != null && (StaticGround.Instance.grounds[row, column - i].GetComponent<Ground>().objectControl.tag == "Soldier" || StaticGround.Instance.grounds[row, column - i].GetComponent<Ground>().objectControl.tag == "MainCity"))
            {
                enemy.Add(StaticGround.Instance.grounds[row, column - i].GetComponent<Ground>().objectControl);
            }
        }
        return enemy;
    }
    /// <summary>
    /// 获取可移动的格子
    /// </summary>
    /// <returns></returns>
    public List<GameObject> GetMoveDistanceObject()
    {
        List<GameObject> moveObject = new List<GameObject>();
        for (int i = 0; i <= realDistance; i++)
        {
            for (int j = 0; j <= realDistance; j++)
            {
                if ((i != 0 || j != 0) && ((i + j) <= realDistance))
                {
                    if (row + i < CollectionOfConstants.MapRow && column + j < CollectionOfConstants.MapColumn && !StaticGround.Instance.grounds[row + i, column + j].GetComponent<Ground>().isHaveObject)
                    {
                        moveObject.Add(StaticGround.Instance.grounds[row + i, column + j]);
                    }
                    if (row - i >= 0 && column + j < CollectionOfConstants.MapColumn && !StaticGround.Instance.grounds[row - i, column + j].GetComponent<Ground>().isHaveObject)
                    {
                        moveObject.Add(StaticGround.Instance.grounds[row - i, column + j]);
                    }
                    if (row + i < CollectionOfConstants.MapRow && column - j >= 0 && !StaticGround.Instance.grounds[row + i, column - j].GetComponent<Ground>().isHaveObject)
                    {
                        moveObject.Add(StaticGround.Instance.grounds[row + i, column - j]);
                    }
                    if (row - i >= 0 && column - j >= 0 && !StaticGround.Instance.grounds[row - i, column - j].GetComponent<Ground>().isHaveObject)
                    {
                        moveObject.Add(StaticGround.Instance.grounds[row - i, column - j]);
                    }
                }
            }
        }
        return moveObject;
    }
    //每回合更新移动步数的函数没写
}
