using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateTransitionButton : MonoBehaviour
{

    [HideInInspector]
    public bool isOn = false;
    public void OnMouseDown()
    {
        if(!isOn)
        {
            //状态转换
            this.transform.parent.GetComponent<Fight>().ChangeState();
            Debug.Log("状态转换");
            isOn = true;
        }
    }
}
