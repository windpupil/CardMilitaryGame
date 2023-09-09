using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardShowEx : MonoBehaviour
{
    public void ShowCard(SoldierCardData data)
    {
        this.transform.GetComponent<Image>().sprite = data.cardImage;
        this.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text =
            data.attack.ToString();
        this.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text =
            data.defense.ToString();
        this.transform.GetChild(0).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text =
            data.health.ToString();
        this.transform.GetChild(0).GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>().text =
            data.attackDistance.ToString();
        this.transform.GetChild(0).GetChild(4).GetChild(0).GetComponent<TextMeshProUGUI>().text =
            data.moveDistance.ToString();
    }
}
