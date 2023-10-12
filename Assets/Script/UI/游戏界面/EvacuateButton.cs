using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvacuateButton : MonoBehaviour
{
    public void OnMouseDown()
    {
        StaticGround.Instance.grounds[GetComponentInParent<BaseObject>().row, GetComponentInParent<BaseObject>().column].GetComponent<Ground>().isHaveObject = false;
        Destroy(this.transform.parent.gameObject);
    }
}
