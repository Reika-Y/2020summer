using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transition : SingletonMonoBehaviour<Transition>
{
    // 透明度
    private float fadeAlpha = 0;

    // 色
    [SerializeField]
    private Color fadeColor = Color.black;

    // フェード中か
    private bool isFading = false;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }

    // シーン遷移用
    // scene = 遷移するシーン名
    // interval = 暗転にかかる時間(s)
    public void LoadScene(string scene, float interval)
    {
        StartCoroutine(TransitionScene(scene, interval));
    }

    // シーン遷移用コルーチン
    // scene = 遷移するシーン名
    // interval = 暗転にかかる時間(s)
    private IEnumerator TransitionScene(string scene, float interval)
    {
        // フェードイン
        isFading = true;
        float time = 0;
        while(time <= interval)
        {
            fadeAlpha = Mathf.Lerp(0f, 1f, time / interval);
            time += Time.deltaTime;
            yield return 0;
        }

        // ロード
        SceneManager.LoadScene(scene);

        // フェードアウト
        time = 0f;
        while (time <= interval)
        {
            fadeAlpha = Mathf.Lerp(1f, 0f, time / interval);
            time += Time.deltaTime;
            yield return 0;
        }

        isFading = false;
    }

    private void OnGUI()
    {
        if (isFading)
        {
            // テクスチャの描画
            fadeColor.a = fadeAlpha;
            GUI.color = fadeColor;
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), Texture2D.whiteTexture);
        }
    }
}
