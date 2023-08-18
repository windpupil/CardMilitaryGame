using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticGround : MonoBehaviour
{
    public static GameObject[,] grounds=new GameObject[9,11];    // 地图区域
    private void Start() {
        //遍历数组，将所有地图区域放入数组中
        for(int i=0;i<9;i++){
            for(int j=0;j<11;j++){
                grounds[i,j]=this.transform.GetChild(i*11+j).gameObject;
                grounds[i,j].GetComponent<Ground>().row=i;
                grounds[i,j].GetComponent<Ground>().column=j;
            }
        }
    }
}