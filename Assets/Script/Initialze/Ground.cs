using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    public bool isActive = false;  //是否激活
    public GameObject objectControl;    //格子上的物体
    public bool isHaveObject = false;  //是否有物体
    public int row;              //行
    public int column;           //列
    public int Steps;            //步数
    private void OnMouseDown()
    {
        if (isActive)
        {
            //当格子被点击后，物体移动到这个格子上
            //将格子变成原来的颜色
            for (int i = 0; i < CollectionOfConstants.MapRow; i++)
            {
                for (int j = 0; j < CollectionOfConstants.MapColumn; j++)
                {
                    if (!isHaveObject&&StaticGround.grounds[i, j].tag!="MainCity")
                    {
                        StaticGround.grounds[i, j].GetComponent<SpriteRenderer>().color = UnityEngine.Color.white;
                        StaticGround.grounds[i, j].GetComponent<Ground>().isActive = false;
                    }
                }
            }
            isHaveObject = true;
            //将物体原来所在的位置更新为没有物体
            StaticGround.grounds[objectControl.GetComponent<ObjectsControl>().row, objectControl.GetComponent<ObjectsControl>().column].GetComponent<Ground>().isHaveObject = false;
            //将物体移动到这个格子上
            objectControl.transform.position = this.transform.position;
            //将物体的行列数改变
            objectControl.GetComponent<ObjectsControl>().row = row;
            objectControl.GetComponent<ObjectsControl>().column = column;
            //行动点-1
            ActionNumberUI.actionNumber--;
            ActionNumberUI.updateActionNumberText();
            //distance减去相应的步数
            objectControl.GetComponent<ObjectsControl>().realDistance -= Steps;
        }
    }

    public void updateObjectsControlRealDistance()
    {
        if(isHaveObject)
        {
            objectControl.GetComponent<ObjectsControl>().realDistance = objectControl.GetComponent<ObjectsControl>().cardData.moveDistance;
        }
    }
}
