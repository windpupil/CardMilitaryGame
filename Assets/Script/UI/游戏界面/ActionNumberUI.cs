using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ActionNumberUI : MonoBehaviour
{
    private int _actionNumber=1;              //行动点
    public int actionNumber
    {
        get
        {
            return _actionNumber;
        }
        set
        {
            if (value > actionNumberCurrentLimit)
            {
                value = actionNumberCurrentLimit;
            }
            else if (value < 0)
            {
                value = 0;
            }
            _actionNumber = value;
            updateActionNumberText();
        }
    }

    private TextMeshProUGUI tip;
    public int actionNumberCurrentLimit = 1;  //行动点当前上限
    private static ActionNumberUI instance;
    public static ActionNumberUI Instance
    {
        get{
            return instance;
        }
    }
    void Start()
    {
        instance=this;
        tip = this.GetComponent<TextMeshProUGUI>();
    }
    public void updateActionNumberText()
    {
        tip.text = "本回合行动点还剩:"+_actionNumber.ToString();
    }
}
