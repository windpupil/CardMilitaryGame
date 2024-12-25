using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPShowMainCity : MonoBehaviour
{
    [Tooltip("本变量用于存放胜利或失败界面")]
    [SerializeField]
    private GameObject obj;
    private int hp = CollectionOfConstants.MainCityHP;
    public int HP
    {
        get { return hp; }
        set
        {
            hp = value;
            if(hp<=0)
            {
                //游戏结束
                Debug.Log("游戏结束");
                //生成胜利或失败界面
                Instantiate(obj, GameObject.Find("Canvas").transform);
                return;
            }
            updateHPUI();
        }
    }
    public void updateHPUI()
    {
        this.GetComponent<Image>().fillAmount = hp/ CollectionOfConstants.MainCityHP;
    }
}
