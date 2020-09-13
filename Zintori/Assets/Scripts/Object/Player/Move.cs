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

        pos.x -= joystick.Position.x * speed * Time.deltaTime;
        pos.z -= joystick.Position.y * speed * Time.deltaTime;

        // ベクトル
        var vec = pos - transform.position;
        if (vec.magnitude > 0.01f)
        {
            transform.rotation = Quaternion.LookRotation(-vec);
            // 何かしら入力がある場合
            playerAnim.ChangeAnim(ANIM_ID.RUN);
        }
        else
        {
            // 入力なしの場合
            playerAnim.ChangeAnim(ANIM_ID.IDLE);
        }

        if (MapControl.Instance.CheckPosition(pos))
        {
            transform.position = pos;
        }

    }
}
