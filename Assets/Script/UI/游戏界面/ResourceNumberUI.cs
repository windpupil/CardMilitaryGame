using System.Collections.ObjectModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ResourceNumberUI : MonoBehaviour
{
    private static ResourceNumberUI instance;
    public static ResourceNumberUI Instance
    {
        get { return instance; }
    }
    private int foodNumber = CollectionOfConstants.INITIALFOODNUMBER;
    public int FoodNumber
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

    private int woodNumber = 5;
    public int WoodNumber
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

    private int ironNumber = CollectionOfConstants.INITIALFOODNUMBER;
    public int IronNumber
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


    private TextMeshProUGUI tip;
    void Start()
    {
        tip = this.GetComponent<TextMeshProUGUI>();
        instance = this;
        updateResourceNumberText();
    }
    public void updateResourceNumberText()
    {
        tip.text = "补给:" + foodNumber.ToString() + "\n铁矿:" + ironNumber.ToString();
    }
}
