﻿//###############################################################
// ゲームシーンネットワークスクリプト
// 作成：2020/8/5
// 更新：2020/9/11
// 担当：山下圭介
//###############################################################

using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameNetwork : MonoBehaviour
{
    // 同期するプレイヤーオブジェクトの名前
    [SerializeField]
    private string InstansObjectName = "Player";
    // インスタンスしたオブジェクト保持用
    private static GameObject MyPlayerObject = null;

    void Start()
    {
        //　自身のプレイヤーを生成
        PhotonNetwork.IsMessageQueueRunning = true;
        MyPlayerObject = PhotonNetwork.Instantiate(InstansObjectName, new Vector3(Random.Range(-20, 20), 0, Random.Range(-20, 20)), Quaternion.identity);
    }

    public static GameObject GetMyPlayerObject {
        get { return MyPlayerObject; }
    }
}
