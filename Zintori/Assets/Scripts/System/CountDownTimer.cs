using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountDownTimer : MonoBehaviour
{
    // トータル時間
    private float totalTime;
    public float TotalTime { get { return totalTime; } }

    // 制限時間
    [SerializeField]
    private int minute = 0;
    public int Minute { get { return minute; } }
    [SerializeField]
    private float seconds = 0f;
    public float Seconds { get { return seconds; } }

    private void Start()
    {
        totalTime = minute * 60 + seconds;
    }

    // Update is called once per frame
    void Update()
    {
        if(totalTime <= 0f)
        {
            return;
        }

        // 現在の時間を計算
        totalTime = minute * 60 + seconds;
        totalTime -= Time.deltaTime;

        // 設定
        minute = (int)totalTime / 60;
        seconds = totalTime - minute * 60;

        // コンソール表示
        if(totalTime <= 0f)
        {
            Debug.Log("制限時間終了");
        }
    }
}
