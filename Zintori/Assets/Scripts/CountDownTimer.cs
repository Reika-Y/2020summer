using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountDownTimer 
{
    // トータル時間
    private float totalTime;
    public float TotalTime { get { return totalTime; } }

    // 制限時間
    private int minute = 0;
    public int Minute
    {
        get
        {
            if (Math.Round(seconds, 0, MidpointRounding.AwayFromZero) >= 60)
            {
                return minute + 1;
            }
            return minute;
        }
    }

    private float seconds = 0f;
    public float Seconds
    {
        get
        {
            if (Math.Round(seconds, 0, MidpointRounding.AwayFromZero) >= 60)
            {
                return 0;
            }
            return seconds;
        }
    }

    public CountDownTimer(int min,float sec)
    {
        minute = min;
        seconds = sec;
        totalTime = minute * 60 + seconds;
    }

    public void Update()
    {
        if(totalTime <= 0f)
        {
            return;
        }

        // 現在の時間を計算
        totalTime -= Time.deltaTime;

        // 設定
        minute = (int)totalTime / 60;
        seconds = totalTime - minute * 60;
    }
}
