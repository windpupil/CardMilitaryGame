using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ResourceNumberUI : MonoBehaviour
{
    private static int foodNumber = 5;
    public static int FoodNumber
    {
        get
        {
            return foodNumber;
        }
        set
        {
            if (value < 0)
            {
                value = 0;
            }
            foodNumber = value;
        }
    }

    private static int woodNumber = 5;
    public static int WoodNumber
    {
        get
        {
            return woodNumber;
        }
        set
        {
            if (value < 0)
            {
                value = 0;
            }
            woodNumber = value;
        }
    }

    private static int ironNumber = 0;
    public static int IronNumber
    {
        get
        {
            return ironNumber;
        }
        set
        {
            if (value < 0)
            {
                value = 0;
            }
            ironNumber = value;
        }
    }

    private static TextMeshProUGUI tip;
    void Awake()
    {
        tip = this.GetComponent<TextMeshProUGUI>();
    }
    public static void updateResourceNumberText()
    {
        tip.text = "粮食:" + foodNumber.ToString() + "\n木材:" + woodNumber.ToString() + "\n铁矿:" + ironNumber.ToString();
    }
}
