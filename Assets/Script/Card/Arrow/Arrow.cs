using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Arrow : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public GameObject ArrowParent; //这是父节点
    public GameObject ArrowNode; //这是尾巴小箭头的prefab
    public GameObject ArrowHead; //这是箭头的prefab
    private const int NODE_COUNT = 20; //这是整个曲线的截数
    private List<GameObject> ArrowNodes; //这是用于保存整个曲线的prefab的list

    private void Start()
    {
        Init();
    }
    public void Init()
    {
        ArrowNodes = new List<GameObject>();
        //生成尾巴小箭头
        for (int i = 0; i < NODE_COUNT - 1; i++)
        {
            //生成一个游戏对象
            GameObject node = GameObject.Instantiate(ArrowNode, ArrowParent.transform);
            //添加到数组里
            ArrowNodes.Add(node);
            //改变缩放，根据杀戮尖塔，箭头是一节节越来越大的
            Vector2 scale = new Vector2(0.2f + i / (float)(NODE_COUNT - 1) * 0.8f, 0.2f + i / (float)(NODE_COUNT - 1) * 0.8f);
            node.GetComponent<RectTransform>().localScale = scale;
        }
        //生成头部小箭头
        GameObject head = GameObject.Instantiate(ArrowHead, ArrowParent.transform);
        //添加到数组里
        ArrowNodes.Add(head);

        //将整个曲线隐藏起来
        ArrowParent.SetActive(false);
    }
    public void UpdateArrow(Vector2 startPos, Vector2 endPos)
    {
        //根据传入的起点和终点来计算两个控制点
        Vector2 ctrlAPos = new Vector2();
        Vector2 ctrlBPos = new Vector2();
        //这里原文作者对参数做了微调，更加符合杀戮尖塔的效果，我照搬了过来
        ctrlAPos.x = startPos.x + (startPos.x - endPos.x) * 0.1f;
        ctrlAPos.y = endPos.y - (endPos.y - startPos.y) * 0.2f;
        ctrlBPos.y = endPos.y + (endPos.y - startPos.y) * 0.3f;
        ctrlBPos.x = startPos.x - (startPos.x - endPos.x) * 0.3f;

        //将曲线显示出来
        ArrowParent.SetActive(true);

        //根据贝塞尔曲线重新设置所有小箭头的位置
        for (int i = 0; i < ArrowNodes.Count; i++)
        {
            float t = (i / (float)(ArrowNodes.Count - 1));
            Vector2 pos = startPos * (1 - t) * (1 - t) * (1 - t) + 3 * ctrlAPos * t * (1 - t) * (1 - t) + 3 * ctrlBPos * t * t * (1 - t) + endPos * t * t * t;
            ArrowNodes[i].transform.localPosition = pos;
        }

        //虽然更改了箭头的位置，不过还需要重新计算箭头的方向
        UpdateAngle();
    }
    private void UpdateAngle()
    {
        for (int i = 0; i < ArrowNodes.Count; i++)
        {
            //第一个箭头默认朝上
            //注意：需要你的箭头素材也得要箭头朝上才行
            if (i == 0)
            {
                ArrowNodes[i].transform.rotation = Quaternion.Euler(Vector3.forward);
            }
            else
            {
                //算出当前箭头的pivot与前一个箭头的pivot的向量
                Vector3 direction = ArrowNodes[i].transform.position - ArrowNodes[i - 1].transform.position;
                //然后求出该向量的夹角
                float angle = Vector3.Angle(direction, Vector3.up);
                //因为这个夹角始终是一个正数，所以需要再进行一步判定
                if (ArrowNodes[i].transform.position.x - ArrowNodes[i - 1].transform.position.x > 0)
                {
                    angle = -angle;
                }
                //修改角度
                ArrowNodes[i].transform.rotation = Quaternion.Euler(Vector3.forward * angle);
            }
        }
    }

    private Vector3 mousePos;
    private Vector3 myPos;
    public void OnBeginDrag(PointerEventData eventData)
    {
        ArrowParent.SetActive(true);
    }
    public void OnDrag(PointerEventData eventData)
    {
        myPos = this.transform.position;
        mousePos = Input.mousePosition;
        UpdateArrow(myPos, mousePos);
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        ArrowParent.SetActive(false);
    }
}