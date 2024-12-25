using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPShow : MonoBehaviour
{
    [Tooltip("本变量用于存放对象的Fight组件")]
    [SerializeField]
    private Fight fight;
    [Tooltip("本变量用于存放父对象")]
    [SerializeField]
    private GameObject obj;

    // private void Start()
    // {
    //     updateHPUI();
    // }

    public void updateHPUI()
    {
        // Debug.Log(fight.hp);
        // Debug.Log(fight.gameObject.GetComponent<BaseObject>().data.health);
        this.GetComponent<Image>().fillAmount = fight.hp / fight.gameObject.GetComponent<BaseObject>().data.health;
    }

    private void Update()
    {
        this.transform.position = Camera.main.WorldToScreenPoint(obj.transform.position);
    }
}
