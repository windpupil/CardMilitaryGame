using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AutoText : MonoBehaviour
{
    string str = ""; //要显示的所有字符串
    string s = ""; //每次显示的字符串
    float time = 0.05f; //每个文字显示时间间隔
    TextMeshProUGUI textMeshProUGUI;
    Coroutine cor;

    [SerializeField] private GameObject continueText;
    [SerializeField] private Button button;
    [SerializeField] private GameObject canvasDestroy;

    [SerializeField] private string _content;


    private void Start()
    {
        PlayAutoText(gameObject, _content, 0.05f);
        continueText.SetActive(false);
        //设置button为不可交互
        button.interactable = false;
    }

    void Update()
    {
        //开启播放后，点击鼠标左键或任意按键则显示全部文本
        if (cor != null && (Input.GetMouseButtonDown(0) || Input.anyKeyDown))
        {
            Finish();
        }
    }

    /// <summary>
    /// 调用协成实现一个字一个字冒出的效果
    /// </summary>
    public void PlayText(string content = null, float time = 0.05f)
    {
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        str = textMeshProUGUI.text;
        //重置
        if (!string.IsNullOrEmpty(content))
        {
            str = content;
        }
        this.time = time;
        textMeshProUGUI.text = "";
        s = "";

        //当前有协成，则先关闭
        if (cor != null)
        {
            StopCoroutine(cor);
        }
        //开启协成
        cor = StartCoroutine(ShowText(str.Length));
    }

    IEnumerator ShowText(int strLength)
    {
        int i = 0;
        while (i < strLength)
        {
            yield return new WaitForSeconds(time);
            s += str[i].ToString();
            textMeshProUGUI.text = s;
            i += 1;
        }
        //显示完成，停止协成
        Finish();
    }

    //立刻显示所有文字
    public void Finish()
    {
        s = str;
        textMeshProUGUI.text = s;
        if (cor != null)
        {
            StopCoroutine(cor);
            cor = null;
        }
        continueText.SetActive(true);
        //0.5s后设置button为可交互
        Invoke("ActiveButton", 0.5f);
    }

    /// <summary>
    /// 逐字播放
    /// </summary>
    public static void PlayAutoText(GameObject go, string content = null, float time = 0.05f)
    {
        if (go != null)
        {
            TextMeshProUGUI textMeshProUGUI = go.GetComponent<TextMeshProUGUI>();
            if (textMeshProUGUI != null)
            {
                AutoText autoText = go.GetComponent<AutoText>();
                if (autoText == null)
                {
                    autoText = go.AddComponent<AutoText>();
                }
                autoText.PlayText(content, time);
                return;
            }
        }
    }
    /// <summary>
    /// 按钮事件
    /// </summary>
    public void Click()
    {
        Destroy(canvasDestroy);
    }

    /// <summary>
    /// 激活按钮
    /// </summary>
    public void ActiveButton()
    {
        button.interactable = true;
    }
}