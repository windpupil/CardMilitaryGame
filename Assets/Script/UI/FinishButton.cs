using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishButton : MonoBehaviour
{
    [SerializeField] private Manage manage;
    public void Finish()
    {
        manage.End();
    }
}
