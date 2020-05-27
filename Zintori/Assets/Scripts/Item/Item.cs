using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    [SerializeField]
    private int minute = 0;
    [SerializeField]
    private float seconds = 5f;
    private Slider slider;
    private CountDownTimer timer;
    private float limit;

    public int hp = 10;

    private int playerId = 0;
    public int PlayerId
    {
        get => playerId;
        set => playerId = value;
    }


    void Start()
    {
        slider = transform.Find("timerGauge/Slider").GetComponent<Slider>();
        timer = new CountDownTimer(minute, seconds);
        limit = minute * 60 + seconds;
    }

    void Update()
    {
        if(hp <= 0)
        {
            Destroy(gameObject, 0.5f);
        }
        if (timer.TotalTime <= 0)
        {
            Destroy(gameObject, 0.5f);
        }
        TimerUpdate();
    }

    void TimerUpdate()
    {
        timer.Update();
        var value = timer.TotalTime / limit;
        slider.value = value;
    }
}
