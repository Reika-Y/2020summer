using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeBarControl : MonoBehaviour
{
    private CountDownTimer timer;

    [SerializeField]
    private GameObject target = null;

    [SerializeField]
    private int minute = 0;

    [SerializeField]
    private float seconds = 5f;

    private float limit;
    private Slider slider;
    void Start()
    {
        timer = new CountDownTimer(minute, seconds);
        limit = minute * 60 + seconds;
        slider = GetComponent<Slider>();
    }

    void Update()
    {
        timer.Update();
        var value = timer.TotalTime / limit;
        slider.value = value;
        //slider.transform.parent.LookAt(target.transform);
    }
}
