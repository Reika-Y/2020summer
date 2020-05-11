using UnityEngine;
using UnityEngine.UI;

// ジョイスティック
public class Joystick :MonoBehaviour
{
    #region <変数>

    // スティック部分
    [SerializeField]
    private GameObject stick = null;
    private const string stickName = "stick";

    // 動く範囲
    [SerializeField]
    private float radius = 100;

    // 現在地
    [SerializeField]
    private Vector2 position = Vector2.zero;
    public Vector2 Position { get { return position; } }

    // スティックの位置
    private Vector3 stickPosition
    {
        set
        {
            stick.transform.localPosition = value;
            position = new Vector2(
                stick.transform.localPosition.x / radius,
                stick.transform.localPosition.y / radius
                );
        }
    }

    // 初期化されているか
    public bool IsInitialized
    {
        get
        {
            // スティックがあれば初期化済
            if (stick)
            {
                return true;
            }

            if(transform.Find(stickName))
            {
                stick = transform.Find(stickName).gameObject;
                return true;
            }

            return false;
        }

    }

    #endregion

    #region <初期化>

    private void Awake()
    {
        Init();

        // 画像は判定しない
        stick.GetComponent<Image>().raycastTarget = false;

        // スケール0
        transform.localScale = Vector3.zero;

        // イベント登録
        TouchEventHandler.Instance.onBeginPress += OnBeginPress;
        TouchEventHandler.Instance.onEndPress += OnEndPress;
        TouchEventHandler.Instance.onEndDrag += OnEndDrag;
        TouchEventHandler.Instance.onDrag += OnDrag;
    }

    // 必要時のみ
    public void Init()
    {
        if(IsInitialized)
        {
            return;
        }

        // スティック生成
        stick = new GameObject(stickName);
        stick.transform.SetParent(gameObject.transform);
        stick.transform.localRotation = Quaternion.identity;
        stickPosition = Vector3.zero;

        // 画像セット
        stick.AddComponent<Image>();
    }

    #endregion

    #region <計算>

    // タッチされている座標をワールド座標で取得
    private Vector3 GetTouchPointInWorld()
    {
        // タップされている位置を画面内の座標に変換
        Vector2 screenPos = Vector2.zero;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(GetComponent<RectTransform>(),
          new Vector2(Input.mousePosition.x, Input.mousePosition.y),
          null,
          out screenPos
        );

        return screenPos;
    }

    #endregion

    #region <タップ>

    // タップ開始時
    public void OnBeginPress()
    {
        // スケールを1に
        transform.localScale = Vector3.one;

        // 全体をタッチされている場所に移動
        transform.localPosition = Vector3.zero;
        transform.localPosition = GetTouchPointInWorld();

        // スティックの位置を中心に
        stickPosition = Vector3.zero;
    }

    // タップ終了時(ドラッグ終了時には呼ばれない)
    public void OnEndPress()
    {
        // タップした終了した時にドラッグを終了した時と同じ処理をする
        OnEndDrag();
    }

    #endregion

    #region <ドラッグ>

    // ドラッグ終了時
    public void OnEndDrag()
    {
        //スティックの位置を中心
        stickPosition = Vector3.zero;

        //スケールを0
        transform.localScale = Vector3.zero;
    }

    // ドラッグ中
    public void OnDrag(Vector2 delta)
    {
        //スティックをタップされている場所に移動
        stickPosition = GetTouchPointInWorld();

        //移動場所が設定した半径を超えてる場合は制限内に抑える
        float currentRadius = Vector3.Distance(Vector3.zero, stick.transform.localPosition);
        if (currentRadius > radius)
        {
            //角度計算
            float radian = Mathf.Atan2(stick.transform.localPosition.y, stick.transform.localPosition.x);

            //円上にXとYを設定
            Vector3 limitedPosition = Vector3.zero;
            limitedPosition.x = radius * Mathf.Cos(radian);
            limitedPosition.y = radius * Mathf.Sin(radian);

            stickPosition = limitedPosition;
        }
    }

    #endregion

    #region <更新>

#if UNITY_EDITOR
    // Gizmoを表示する
    private void OnDrawGizmos()
    {
        // スティックが移動できる範囲をScene上に表示
        UnityEditor.Handles.color = Color.green;
        UnityEditor.Handles.DrawWireDisc(transform.position, transform.forward, radius * 0.5f);
    }
#endif

    #endregion
}
