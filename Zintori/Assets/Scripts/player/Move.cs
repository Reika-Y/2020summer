﻿using UnityEngine;

// キャラの移動処理
public class Move : MonoBehaviour
{
    [SerializeField]
    private Joystick joystick = null;

    // 移動速度
    [SerializeField]
    private float speed = 10.0f;

    void Update()
    {
        Vector3 pos = transform.position;

        pos.x -= joystick.Position.x * speed * Time.deltaTime;
        pos.z -= joystick.Position.y * speed * Time.deltaTime;

        transform.position = pos;
    }
}
