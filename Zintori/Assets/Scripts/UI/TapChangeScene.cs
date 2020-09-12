using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TapChangeScene : MonoBehaviour
{
    // シーン遷移するシーン
    [SerializeField]
    private EnumScene scene;

    // フェードするか
    [SerializeField]
    private bool isFading = true;

    // 暗転にかかる時間(s)
    [SerializeField]
    private float interval = 2f;

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if (isFading)
            {
                Transition.Instance.LoadScene(scene.ToString(), interval);
            }
            else
            {
               SceneManager.LoadScene(scene.ToString());
            }
        }
    }
}
