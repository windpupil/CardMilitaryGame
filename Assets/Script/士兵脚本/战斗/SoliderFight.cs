using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoliderFight : Fight
{
    private RaycastHit2D hit;
    private void Start()
    {
        attackNumber = this.GetComponent<BaseObject>().data.maxAttackNumber;
        hp = this.GetComponent<BaseObject>().data.health;
        attack = this.GetComponent<BaseObject>().data.attack;
        defense = this.GetComponent<BaseObject>().data.defense;
        attackDistance = this.GetComponent<BaseObject>().data.attackDistance;
    }
    private void OnMouseDown()
    {
        if (IsAllowAttack())
        {
            GetAttackDistanceObject("Enemy");
            //如果有敌人，显示箭头
        }
    }
    private void OnMouseDrag()
    {
        //射线检测，输出碰撞点的位置
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
    }
    private void OnMouseUp()
    {
        if (IsAllowAttack())
        {
            if (hit.collider != null)
            {
                Debug.Log(attackableObjects.Count);
                for (int i = 0; i < attackableObjects.Count; i++)
                {
                    Debug.Log("可攻击的敌人是"+attackableObjects[i].name);
                    Debug.Log("可攻击的敌人的坐标是"+attackableObjects[i].GetComponent<BaseObject>().row+","+attackableObjects[i].GetComponent<BaseObject>().column);
                    Debug.Log("鼠标检测的敌人是"+hit.collider.gameObject.name);
                    if (hit.collider.gameObject == attackableObjects[i])
                    {
                        Debug.Log("攻击敌人");
                        hit.collider.gameObject.GetComponent<Fight>().Hurt(this.GetComponent<Fight>());
                        attackNumber--;
                    }
                }
            }
        }
    }
}
