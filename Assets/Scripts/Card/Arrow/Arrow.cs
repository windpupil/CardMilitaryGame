using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed;

    [SerializeField]
    public GameObject obj;
    private Vector2 direction;

    private Vector2 direction2;

    void OnMouseDown()
    {
        this.transform.GetChild(0).gameObject.SetActive(true);
    }

    void OnMouseUp()
    {
        this.transform.GetChild(0).gameObject.SetActive(false);
    }

    private void OnMouseDrag()
    {
        FollowMouse();
    }

    void FollowMouse()
    {
        //得到物体指向鼠标位置的向量
        direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - obj.transform.position;
        //通过反正切函数得到弧度并转化为角度
        float rotateAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //指定向屏幕外的z轴为旋转轴并且旋转
        Quaternion rotation = Quaternion.AngleAxis(rotateAngle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, speed * Time.deltaTime);
    }

    void FollowMouse2()
    {
        direction2 = (
            (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition)
            - new Vector2(obj.transform.position.x, obj.transform.position.y)
        ).normalized;
        obj.transform.right = direction2;
    }
}
