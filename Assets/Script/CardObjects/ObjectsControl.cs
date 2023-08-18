using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsControl : MonoBehaviour
{
    public const int MainCityRow = 8;
    public const int MainCityColumn = 5;
    public SoldierCardData cardData;
    public int row;                        // 行
    public int column;                     // 列
    public int realDistance;               // 回合中实际剩余步数
    private void Start()
    {
        realDistance = cardData.distance;
    }

    private void OnMouseDown()
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
                        if (row + i < 9 && column + j < 11 && !(row + i == MainCityRow && column + j == MainCityColumn))
                        {
                            StaticGround.grounds[row + i, column + j].GetComponent<SpriteRenderer>().color = UnityEngine.Color.green;
                            StaticGround.grounds[row + i, column + j].GetComponent<Ground>().isActive = true;
                            StaticGround.grounds[row + i, column + j].GetComponent<Ground>().Steps = i + j;
                            StaticGround.grounds[row + i, column + j].GetComponent<Ground>().objectControl = this.gameObject;
                        }
                        if (row - i >= 0 && column + j < 11 && !(row - i == MainCityRow && column + j == MainCityColumn))
                        {
                            StaticGround.grounds[row - i, column + j].GetComponent<SpriteRenderer>().color = UnityEngine.Color.green;
                            StaticGround.grounds[row - i, column + j].GetComponent<Ground>().isActive = true;
                            StaticGround.grounds[row - i, column + j].GetComponent<Ground>().Steps = i + j;
                            StaticGround.grounds[row - i, column + j].GetComponent<Ground>().objectControl = this.gameObject;
                        }
                        if (row + i < 9 && column - j >= 0 && !(row + i == MainCityRow && column - j == MainCityColumn))
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
}
