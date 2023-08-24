using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawnotResourceCardButton : DrawResourceCardButton
{
    private void Start() {
        if (CheckIsNull(Manage.Instance.notresourceCard.Count))
        {
            //将按钮设置为不可用
            this.GetComponent<Button>().interactable = false;
        }
    }
}
