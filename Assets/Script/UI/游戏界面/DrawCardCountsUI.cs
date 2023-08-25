using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DrawCardCountsUI : MonoBehaviour
{
    private static DrawCardCountsUI instance;
    public static DrawCardCountsUI Instance
    {
        get { return instance; }
    }

    [Tooltip("本变量用于存放抽卡次数上限")]
    [HideInInspector]
    public int resourceCardCountsLimit = 8;

    [Tooltip("本变量用于存放抽卡次数")]
    [HideInInspector]
    public int resourceCardCountsCurrent = 8;
    private TextMeshProUGUI tip;

    void Start()
    {
        instance = this;
        tip = this.GetComponent<TextMeshProUGUI>();
    }

    public void updateResourceCardCountsText()
    {
        tip.text = "你还剩" + resourceCardCountsCurrent.ToString() + "次抽卡机会！";
    }

    public void UpdateCounts()
    {
        //更新抽卡次数为上限
        resourceCardCountsCurrent=resourceCardCountsLimit;
    }
}
