using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using TMPro;
using UnityEngine.UI;

public class LoadingController : MonoBehaviour
{
	// 加载进度显示
	public TextMeshProUGUI progressText;
	// 进度条
    public Image progressBar;
	// 目标异步场景
    private AsyncOperation asyncOperation;

    void Start()
    {
        // 协程启动异步加载
        StartCoroutine(this.AsyncLoading());
    }

    IEnumerator AsyncLoading()
    {
        this.asyncOperation= SceneManager.LoadSceneAsync((string)SceneController.sceneNames[SceneController.sceneIndex]);
        //终止自动切换场景
        this.asyncOperation.allowSceneActivation = false;
        yield return asyncOperation;
    }
    void Update()
    {
        // 获取加载进度，让进度条动起来
        this.SettingLoadingUI(this.asyncOperation.progress);
        // 当进度大于0.9时就已经加载完毕
        if (this.asyncOperation.progress >= 0.9f)
        {
            // 允许切换。将在下一帧切换场景
            this.asyncOperation.allowSceneActivation = true;
        }
    }
    public void SettingLoadingUI(float progress )
    {
    	// 百分比的方式显示进度
        this.progressText.text = (progress*100f).ToString() + "%";
        // 通过改变image的宽度来实现进度条
        this.progressBar.fillAmount = progress;
    }
}
