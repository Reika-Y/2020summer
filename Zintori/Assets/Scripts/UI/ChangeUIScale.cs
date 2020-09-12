using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 表示・非表示の切り替え
public class ChangeUIScale : MonoBehaviour
{
    // 切り替えたいオブジェクト
    [SerializeField]
    private GameObject target = null;

    void Start()
    {
        target.SetActive(false);
    }

    public void OnClick()
    {
        target.SetActive(true);
    }
}
