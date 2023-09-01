using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dialogue2;
namespace Dialogue1Demo
{
    public class GameManager : MonoBehaviour
    {
        public GameObject dialogGo;
        public void OnClick_Dialogue()
        {
            dialogGo.SetActive(true);
            DialogueManager.Instance.SetDialogID(1);
        }
    }
}
