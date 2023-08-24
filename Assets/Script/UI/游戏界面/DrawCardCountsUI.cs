using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DrawCardCountsUI : MonoBehaviour
{
    public static int resourceCardCountsLimit = 8;
    public static int resourceCardCountsCurrent = 8;
    private static TextMeshProUGUI tip;
    void Awake()
    {
        tip = this.GetComponent<TextMeshProUGUI>();
    }
    public static void updateResourceCardCountsText()
    {
        tip.text = "你还剩"+resourceCardCountsCurrent.ToString()+"次抽卡机会！";
    }
}
