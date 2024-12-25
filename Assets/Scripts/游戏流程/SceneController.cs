using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public static class SceneController
{
	// 场景列表，这里填写的都是你已经存在的scene 的场景名
    public static ArrayList sceneNames = new ArrayList() {
        "加载界面",
        "第一关",
        "开始界面",
    };
    // 自定义loading名，在这里使用它是因为别的需求，如果你的需求比较简单可以直接写死
    public static string LoadingSceneName = "加载界面";
    // 定义一个回调事件  当完成任何场景切换时触发
    public static Action AfterGoToScene;

    // 即将切换场景的序号
    public static int sceneIndex = -1;
    // 根据场景顺序，切换到下一个场景的方法
    public static void GoToNextScene()
    {
        if (SceneController.sceneIndex < SceneController.sceneNames.Count - 1)
        {
            SceneController.sceneIndex++;
            GoToSceneByIndex(SceneController.sceneIndex);
        }
    }
    // 根据场景顺序，切换到上一个场景
    public static void GoToPrevScene()
    {
        if (SceneController.sceneIndex > 0)
        {
            SceneController.sceneIndex--;
            GoToSceneByIndex(SceneController.sceneIndex);
        }
    }

	// 根据场景名称（scene name） 切换到目标场景 （scene index会跟着变动）
    /// <summary>
    /// goto the scene if finded name in scenelist,atherwase reopen current scene
    /// </summary>
    /// <param name="sceneName"></param>
    public static void GoToSceneByName(string sceneName)
    {
        if (SceneController.sceneNames.Contains(sceneName))
        {
         	GoToSceneByIndex(SceneController.sceneNames.IndexOf(sceneName));
        }
    }
	//根据 场景序号切换到目标场景
    public static void GoToSceneByIndex(int sceneIndex)
    {
        SceneController.sceneIndex=sceneIndex;
        if (SceneController.sceneIndex >= 0 && SceneController.sceneIndex < SceneController.sceneNames.Count)
        {
            SceneController.sceneIndex = sceneIndex;
            SceneManager.LoadScene(LoadingSceneName);
            AfterGoToScene?.Invoke();
        }
    }
    // 根据目标场景名称 注销目标场景，这个只是为了统一管理，因为unity早就内置了方法
    public static void UnLoadSceneByName(string sceneName)
    {
        SceneManager.UnloadSceneAsync(sceneName);
    }
    // 根据目标场景序号 注销目标场景
    public static void UnLoadSceneById(int sceneID)
    {
        SceneManager.UnloadSceneAsync((string)SceneController.sceneNames[sceneID]);
    }
}

