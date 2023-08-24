using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginGameUI : MonoBehaviour
{
    [Tooltip("本变量用于存放开始游戏界面")]
    [SerializeField]
    private GameObject beginGameUI;
    public void Begin () {
        beginGameUI.SetActive(false);
    }
}
