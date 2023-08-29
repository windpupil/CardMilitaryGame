using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishButton : MonoBehaviour
{
    public void Finish()
    {
        Manage.Instance.End();
        StaticGround.Instance.updateGroundsColor();
    }
}
