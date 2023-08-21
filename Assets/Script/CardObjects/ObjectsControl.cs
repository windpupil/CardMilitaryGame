using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 本脚本是物体的控制脚本
/// </summary>
public class ObjectsControl : MonoBehaviour
{
    public const int MainCityRow = 8;
    public const int MainCityColumn = 5;
    public SoldierCardData cardData;
    public int row;                        // 行
    public int column;                     // 列
    public int realDistance;               // 回合中实际剩余步数
    public GameObject evacuateButton;       // 撤退按钮


    private int attackNumber ;               //本回合攻击次数
    private int HP;                                     // 生命值
    public int hp
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
    private int attack ;                               // 攻击力
    private int defense ;                            // 防御力
    private void Awake()
    {
        realDistance = cardData.moveDistance;
        //初始化攻击次数
        attackNumber = cardData.maxAttackNumber;
        //初始化生命值
        hp = cardData.health;
        //初始化攻击力
        attack = cardData.attack;
        //初始化防御力
        defense = cardData.defense;
    }

    private void OnMouseDown()
    {
        //更新整张地图恢复原来的颜色
        StaticGround.updateGroundsColor();
        SearchAndShowGrounds();
        evacuateButton.SetActive(true);

    }
    /// <summary>
    /// 寻找并展示可以去的地方
    /// </summary>
    public void SearchAndShowGrounds()
    {
        //当物体被点击后，物体通过cardData中的distance来判断哪些格子可以到达
        //将所有可以到达的格子变成绿色
        if (ActionNumberUI.actionNumber != 0)
        {
            for (int i = 0; i <= realDistance; i++)
            {
                for (int j = 0; j <= realDistance; j++)
                {
                    if ((i != 0 || j != 0) && ((i + j) <= realDistance))
                    {
                        if (row + i < CollectionOfConstants.MapRow && column + j < CollectionOfConstants.MapColumn && !(row + i == MainCityRow && column + j == MainCityColumn))
                        {
                            StaticGround.grounds[row + i, column + j].GetComponent<SpriteRenderer>().color = UnityEngine.Color.green;
                            StaticGround.grounds[row + i, column + j].GetComponent<Ground>().isActive = true;
                            StaticGround.grounds[row + i, column + j].GetComponent<Ground>().Steps = i + j;
                            StaticGround.grounds[row + i, column + j].GetComponent<Ground>().objectControl = this.gameObject;
                        }
                        if (row - i >= 0 && column + j < CollectionOfConstants.MapColumn && !(row - i == MainCityRow && column + j == MainCityColumn))
                        {
                            StaticGround.grounds[row - i, column + j].GetComponent<SpriteRenderer>().color = UnityEngine.Color.green;
                            StaticGround.grounds[row - i, column + j].GetComponent<Ground>().isActive = true;
                            StaticGround.grounds[row - i, column + j].GetComponent<Ground>().Steps = i + j;
                            StaticGround.grounds[row - i, column + j].GetComponent<Ground>().objectControl = this.gameObject;
                        }
                        if (row + i < CollectionOfConstants.MapRow && column - j >= 0 && !(row + i == MainCityRow && column - j == MainCityColumn))
                        {
                            StaticGround.grounds[row + i, column - j].GetComponent<SpriteRenderer>().color = UnityEngine.Color.green;
                            StaticGround.grounds[row + i, column - j].GetComponent<Ground>().isActive = true;
                            StaticGround.grounds[row + i, column - j].GetComponent<Ground>().Steps = i + j;
                            StaticGround.grounds[row + i, column - j].GetComponent<Ground>().objectControl = this.gameObject;
                        }
                        if (row - i >= 0 && column - j >= 0 && !(row - i == MainCityRow && column - j == MainCityColumn))
                        {
                            StaticGround.grounds[row - i, column - j].GetComponent<SpriteRenderer>().color = UnityEngine.Color.green;
                            StaticGround.grounds[row - i, column - j].GetComponent<Ground>().isActive = true;
                            StaticGround.grounds[row - i, column - j].GetComponent<Ground>().Steps = i + j;
                            StaticGround.grounds[row - i, column - j].GetComponent<Ground>().objectControl = this.gameObject;
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// 攻击
    /// </summary>
    public void Attack()
    {

    }
}
