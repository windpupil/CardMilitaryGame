using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvacuateButton : MonoBehaviour
{
    public void OnMouseDown()
    {
        StaticGround.grounds[GetComponentInParent<ObjectsControl>().row, GetComponentInParent<ObjectsControl>().column].GetComponent<Ground>().isHaveObject = false;
        Destroy(this.transform.parent.gameObject);
    }
}
