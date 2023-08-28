using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPShowEnemy : MonoBehaviour
{
    [SerializeField]
    private Enemy enemy;
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
        this.GetComponent<Image>().fillAmount = enemy.hp / enemy.cardData.health;
    }

    private void Update()
    {
        this.transform.position = Camera.main.WorldToScreenPoint(obj.transform.position);
    }
}
