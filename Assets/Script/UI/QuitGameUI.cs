using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitGameUI : MonoBehaviour
{
    public void Quit () {
        //退出游戏
        Debug.Log("点击了退出游戏按钮");
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
