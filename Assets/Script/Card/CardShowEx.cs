using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardShowEx: MonoBehaviour
{
    public SoldierCardData cardData; // 卡牌数据
    private void Start() {
        this.transform.localPosition=new Vector3(-822.5f,-11,0);
        ShowCard();
    }
    public void ShowCard()
    {
        this.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text =
            cardData.attack.ToString();
        this.transform.GetChild(1).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text =
            cardData.defense.ToString();
        this.transform.GetChild(1).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text =
            cardData.health.ToString();
        this.transform.GetChild(1).GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>().text =
            cardData.moveDistance.ToString();

        this.transform.GetChild(1).GetChild(4).GetChild(0).GetComponent<TextMeshProUGUI>().text =
            cardData.cost["补给"].ToString(); //这里需要修改

        this.transform.GetChild(1).GetChild(5).GetChild(0).GetComponent<TextMeshProUGUI>().text =
            cardData.cardType;
        this.transform.GetChild(1).GetChild(6).GetComponent<TextMeshProUGUI>().text =
            cardData.cardName;
        this.transform.GetChild(1).GetChild(7).GetComponent<TextMeshProUGUI>().text =
            cardData.cardDescription;
    }
}
