using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowButton : MonoBehaviour
{
    private void Start() {
        
    }
    [SerializeField]
    private GameObject button;
    private void OnMouseEnter() {
        button.SetActive(true);
    }
    private void OnMouseExit() {
        button.SetActive(false);
    }
}
