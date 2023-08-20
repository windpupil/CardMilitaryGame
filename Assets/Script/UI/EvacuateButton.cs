using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvacuateButton : MonoBehaviour
{
    //单击销毁父物体
    public void OnMouseDown()
    {
        Debug.Log("点击了撤退按钮");
        Destroy(this.transform.parent.gameObject);
    }
}
