using Photon.Pun;
using UnityEngine;

// キャラの移動処理
public class Move : MonoBehaviour
{
    [SerializeField]
    private Joystick joystick = null;

    // 移動速度
    [SerializeField]
    private float speed = 10.0f;

    private PlayerAnim playerAnim = null;

    private void Start()
    {
        playerAnim = gameObject.GetComponent<PlayerAnim>();
    }

    void Update()
    {
        Vector3 pos = transform.position;

        if (joystick != null && this.GetComponent<PhotonView>().IsMine) { 
            pos.x -= joystick.Position.x * speed * Time.deltaTime;
            pos.z -= joystick.Position.y * speed * Time.deltaTime;
        }
        else
        {
            GameObject obj = GameObject.Find("Joystick");
            if (obj == null) return;
            joystick = GameObject.Find("Joystick").GetComponent<Joystick>();
        }
        
        // ベクトル
        var vec = pos - transform.position;
        if (vec.magnitude > 0.001f)
        {
            transform.rotation = Quaternion.LookRotation(-vec);
            // 何かしら入力がある場合
            if (playerAnim != null)
            {
                playerAnim.ChangeAnim(ANIM_ID.RUN);
            }
            else
            {
                playerAnim = gameObject.GetComponent<PlayerAnim>();
            }
        }
        else
        {
            // 入力なしの場合
            if (playerAnim != null)
            {
                playerAnim.ChangeAnim(ANIM_ID.IDLE);
            }
            else
            {
                playerAnim = gameObject.GetComponent<PlayerAnim>();
            }
        }

        if (MapControl.Instance.CheckPosition(pos))
        {
            transform.position = pos;
        }

    }
}
