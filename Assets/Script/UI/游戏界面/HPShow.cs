using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPShow : MonoBehaviour
{
    [SerializeField]
    private Fight fight;
    [SerializeField]
    private GameObject obj;
    [SerializeField]
    private Canvas canvas;


    private Vector3 ptWorld;

    private void Start()
    {
        ptWorld = Camera.main.WorldToScreenPoint(obj.transform.position);
        updateHPUI();
    }

    public void updateHPUI()
    {
        this.GetComponent<Image>().fillAmount = fight.hp / fight.data.cardData.health;
    }

    private void Update()
    {
        this.transform.position = Camera.main.WorldToScreenPoint(obj.transform.position);
    }
}
