//################################
// ネットワーク接続スクリプト
// 作成：2020/8/5
// 更新：2020/9/11
// 担当：山下圭介
//################################

using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Network : MonoBehaviourPunCallbacks
{
    // 最大プレイヤー数
    [SerializeField]
    private int MaxPlayer = 2;
    // フェード処理をするかどうか
    [SerializeField]
    private bool isFading = true;
    // 暗転するまでの時間
    [SerializeField]
    private float interval = 2f;
    // 遷移先のシーン
    [SerializeField]
    private EnumScene scene;

    private Button button;

    // ボタンを押されたときの処理
    // ネットワーク機能を有効にするなどの処理
    public void OnClickStartButton()
    {
        PhotonNetwork.ConnectUsingSettings();
        //############################################
        // 以下にボタンを押した際に行う処理を追加
        // 対戦相手待機中てきな処理をあとから追加予定

        //############################################
    }

    // マスターサーバーに接続されたときに呼ばれるコールバック関数
    // ここで部屋を作成、もしくは部屋に入る
    public override void OnConnectedToMaster()
    {
        RoomOptions roomOps = new RoomOptions();
        roomOps.MaxPlayers = (byte)MaxPlayer;
        PhotonNetwork.JoinOrCreateRoom("MultiPlayRoom", roomOps, null);
    }

    // プレイヤーが入ってきたとき（自分以外）のコールバック関数
    // 自分含め二人目が入ってきた時点でPunRPCに登録されているこの関数を呼び出す。
    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        var view = this.GetComponent<PhotonView>();
        view.RPC("LoadScene", RpcTarget.All);
    }

    //##########################################################################
    // 以下RPC用関数

    // シーン遷移用関数
    [PunRPC]
    public void LoadScene()
    {
        if (isFading)
        {
            // フェードして遷移
            Transition.Instance.LoadScene(scene.ToString(), interval);
        }
        else
        {
            // フェードなし遷移
            PhotonNetwork.IsMessageQueueRunning = false;
            SceneManager.LoadScene(scene.ToString());
        }
    }

    //##########################################################################
}
