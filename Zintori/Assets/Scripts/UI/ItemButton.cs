using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour
{
    [SerializeField]
    private MapControl map = null;

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private GameObject item;

    [SerializeField]
    private Color fadeColor;

    [SerializeField]
    private float coolTime = 3f;

    private Image fadeImage;
    private RectTransform rect;
    private float textureHeight;
    private CountDownTimer timer;
    private Button button;
    void Start()
    {
        var obj = transform.Find("FadeImage");
        fadeImage = obj.GetComponent<Image>();
        fadeImage.sprite = gameObject.GetComponent<Image>().sprite;
        fadeImage.color = fadeColor;
        fadeImage.enabled = false;
        rect = obj.GetComponent<RectTransform>();
        textureHeight = rect.sizeDelta.y;
        timer = new CountDownTimer(0, coolTime);
        button = gameObject.GetComponent<Button>();
    }

    void Update()
    {
        if(!fadeImage.enabled)
        {
            return;
        }
        timer.Update();
        rect.sizeDelta = new Vector2(rect.sizeDelta.x,textureHeight * (timer.TotalTime / coolTime));
        if(timer.TotalTime <= 0)
        {
            fadeImage.enabled = false;
            button.enabled = true;
        }
    }

    public void OnClick()
    {
        if (fadeImage.enabled)
        {
            return;
        }
        if (map.CheckPosition(player.transform.position))
        {
            Instantiate(item, player.transform.position, Quaternion.identity);
        }
        button.enabled = false;
        fadeImage.enabled = true;
        timer.Reset(0,coolTime);
    }
}
