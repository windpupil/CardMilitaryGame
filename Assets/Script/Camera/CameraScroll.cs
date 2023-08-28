using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScroll : MonoBehaviour
{
    //摄像机随鼠标滚动而上下移动
    public float scrollSpeed = 15f;
    public float minCameraY = 0f;
    public float maxCameraY = 20f;
    private void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        Vector3 pos = transform.position;
        pos.y -= scroll * scrollSpeed * 100f * Time.deltaTime;
        pos.y = Mathf.Clamp(pos.y, minCameraY, maxCameraY);
        transform.position = pos;
    }
}
