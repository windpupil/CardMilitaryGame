using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardShowEx: MonoBehaviour
{
    public void ShowCard(SoldierCardData cardData)
    {
        this.transform.GetComponent<Image>().sprite = cardData.cardImage;
        this.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text =
            cardData.attack.ToString();
        this.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text =
            cardData.defense.ToString();
        this.transform.GetChild(0).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text =
            cardData.health.ToString();
        this.transform.GetChild(0).GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>().text =
            cardData.attackDistance.ToString();
        this.transform.GetChild(0).GetChild(4).GetChild(0).GetComponent<TextMeshProUGUI>().text =
            cardData.moveDistance.ToString();
    }
}
