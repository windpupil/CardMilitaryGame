using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandCard : MonoBehaviour
{
    public bool isUpperLimit = false; //是否超过手牌上限
    private static HandCard instance;
    public static HandCard Instance
    {
        get { return instance; }
        set { instance = value; }
    }
    private void Start()
    {
        instance = this;
    }

    private int handCardCounts = 0; //手牌数量
    public int HandCardCounts
    {
        get { return handCardCounts; }
        set
        {
            if (value != handCardCounts)
            {
                //整理手牌
            }
            handCardCounts = value;
            if (handCardCounts > CollectionOfConstants.HANDCARDLIMIT)
            {
                isUpperLimit = true;
            }
            else
            {
                isUpperLimit = false;
            }
            if (Manage.Instance.isEnd && handCardCounts <= CollectionOfConstants.HANDCARDLIMIT)
            {
                Manage.Instance.End();
            }
        }
    }
}
