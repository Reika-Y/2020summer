﻿//###############################################################
// ゲームシーンネットワークスクリプト
// 作成：2020/8/5
// 更新：2020/9/11
// 担当：山下圭介
//###############################################################

using Photon.Pun;
using UnityEngine;

public class GameNetwork : MonoBehaviour
{
    // 同期するプレイヤーオブジェクトの名前
    [SerializeField]
    private string InstansObjectName = "Player";
    private GameObject MyPlayerObject = null;

    void Start()
    {
        //　自身のプレイヤーを生成
        MyPlayerObject = PhotonNetwork.Instantiate(InstansObjectName, new Vector3(Random.Range(-100, 100), 0, Random.Range(-100, 100)), Quaternion.identity);
    }
}
