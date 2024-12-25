using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButton : MonoBehaviour
{
    public void Click()
    {
        SceneController.GoToSceneByName("开始界面");
    }
}
