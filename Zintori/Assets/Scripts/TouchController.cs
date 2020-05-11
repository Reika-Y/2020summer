using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchController : MonoBehaviour
{
    // 移動速度
    [SerializeField] private float speed = 5;

    // タッチ位置保存用
    private Vector3 mousePos;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        // タッチされたとき
        if(Input.GetMouseButtonDown(0))
        {
            mousePos = Input.mousePosition;
        }

        if(Input.GetMouseButton(0))
        {
            // 現在と1フレーム前の位置の差分
            var mouseDiff = Input.mousePosition - mousePos;
            // 位置の更新
            //mousePos = Input.mousePosition;

            Vector3 newPos = this.gameObject.transform.position + new Vector3(-mouseDiff.x / Screen.width, 0, -mouseDiff.y / Screen.height) * speed * Time.deltaTime;

            transform.position = newPos;
        }
    }
}
