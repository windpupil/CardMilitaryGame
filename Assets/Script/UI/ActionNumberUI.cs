using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ActionNumberUI : MonoBehaviour
{
    public static int actionNumber = 5;
    private static TextMeshProUGUI tip;
    private void Start()
    {
        tip = this.GetComponent<TextMeshProUGUI>();
    }
    public static void updateActionNumberText()
    {
        tip.text = "本回合行动点还剩:"+actionNumber.ToString();
    }
}
