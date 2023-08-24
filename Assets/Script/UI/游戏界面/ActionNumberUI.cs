using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ActionNumberUI : MonoBehaviour
{
    public const int actionNumberLimit = 10;                //行动点最大上限
    public static int actionNumber = 1;              //行动点
    private static TextMeshProUGUI tip;
    public static int actionNumberCurrentLimit = 1;  //行动点当前上限
    void Awake()
    {
        tip = this.GetComponent<TextMeshProUGUI>();
    }
    public static void updateActionNumberText()
    {
        tip.text = "本回合行动点还剩:"+actionNumber.ToString();
    }
}
