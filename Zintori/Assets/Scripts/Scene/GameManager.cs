using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // 制限時間表示
    [SerializeField]
    private int minute = 3;
    [SerializeField]
    private float seconds = 0;
    [SerializeField]
    private Text text;
    private CountDownTimer timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = new CountDownTimer(minute, seconds);
        TextDisplay(minute, seconds);
    }

    // Update is called once per frame
    void Update()
    {
        timer.Update();
        TextDisplay(timer.Minute, timer.Seconds);
    }

    void TextDisplay(int m,float s)
    {
        text.text = m.ToString("00") + ":" + s.ToString("00");
    }
}
