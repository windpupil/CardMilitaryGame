using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initialize : MonoBehaviour
{
    [SerializeField]
    public GameObject squarePrefab;
    [SerializeField]
    public GameObject blueGroundPrefab;
    [SerializeField]
    public GameObject redGroundPrefab;
    [SerializeField]
    public int rows = 10;
    [SerializeField]
    public int columns = 10;

    void Start()
    {
        GenerateSquareMatrix();
    }
    public void GenerateSquareMatrix()
    {
        // 矩阵父物体
        Transform parent = new GameObject("GroundClone").transform;
        GameObject square=null;
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                // 实例化正方形
                if(i==0&&j==5)
                {
                    square = Instantiate(blueGroundPrefab);
                }
                else if(i==8&&j==5)
                {
                    square = Instantiate(redGroundPrefab);
                }
                else
                {
                    square = Instantiate(squarePrefab);
                }
                // 设置位置
                square.transform.SetParent(parent);
                square.transform.localPosition = new Vector3(j,-i, 0)+this.transform.position;
            }
        }
    }
}
