using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    public bool isActive=false;  //是否激活
    public GameObject objectControl;    //格子上的物体
    public int row;              //行
    public int column;           //列
    public int Steps;            //步数
    private void OnMouseDown() {
        if(isActive){
            //当格子被点击后，物体移动到这个格子上
            //将所有格子变成原来的颜色
            for(int i=0;i<9;i++){
                for(int j=0;j<11;j++){
                    StaticGround.grounds[i,j].GetComponent<SpriteRenderer>().color=UnityEngine.Color.white;
                    StaticGround.grounds[i,j].GetComponent<Ground>().isActive=false;
                }
            }
            //将物体移动到这个格子上
            objectControl.transform.position=this.transform.position;
            //将物体的行列数改变
            objectControl.GetComponent<ObjectsControl>().row=row;
            objectControl.GetComponent<ObjectsControl>().column=column;
            //行动点-1
            ActionNumberUI.actionNumber--;
            //distance减去相应的步数
            objectControl.GetComponent<ObjectsControl>().realDistance-=Steps;
        }
    }
}
