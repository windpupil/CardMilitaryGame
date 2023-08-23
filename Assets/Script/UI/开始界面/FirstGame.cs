using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstGame : MonoBehaviour
{
    public void OnClick()
    {
        SceneController.GoToSceneByName("第一关");
    }
}
