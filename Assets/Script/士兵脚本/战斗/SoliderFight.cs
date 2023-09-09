using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoliderFight : Fight
{
    private RaycastHit2D hit;
    private void OnMouseDown()
    {
        hit = new RaycastHit2D();
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
                for (int i = 0; i < attackObject.Count; i++)
                {
                    Debug.Log(attackObject[i].name);
                    if (hit.collider.gameObject == attackObject[i])
                    {
                        Debug.Log("攻击敌人");
                        Hurt(attackObject[i].GetComponent<Fight>());
                        attackNumber--;
                    }
                }
            }
        }
    }
}
