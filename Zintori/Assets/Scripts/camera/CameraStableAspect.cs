using UnityEngine;

// カメラのアスペクト比を固定する
// Edit時も実行される
[ExecuteAlways]
[RequireComponent(typeof(Camera))]
public class CameraStableAspect : MonoBehaviour
{
    // アスペクト比を固定するカメラ
    [SerializeField]
    private Camera refCamera;

    // 固定する横幅
    [SerializeField]
    private int width = 1080;

    // 固定する縦幅
    [SerializeField]
    private int height = 1920;

    // ピクセル数
    [SerializeField]
    private float pixcelPerUnit = 100f;

    // 横幅、縦幅（変数）
    private int m_width = -1;
    private int m_height = -1;

    private void Awake()
    {
        if (refCamera == null)
        {
            refCamera = GetComponent<Camera>();
        }
        UpdateCamera();
    }

    private void Update()
    {
        UpdateCameraWithCheck();
    }

    // 画面サイズの確認
    private void UpdateCameraWithCheck()
    {
        if(m_width == Screen.width && m_height == Screen.height)
        {
            return;
        }
        UpdateCamera();
    }

    // カメラのアスペクト比を固定する
    private void UpdateCamera()
    {
        float screen_w = (float)Screen.width;
        float screen_h = (float)Screen.height;
        float target_w = (float)width;
        float target_h = (float)height;

        // アスペクト比
        float aspect = screen_w / screen_h;
        float targetAspect = target_w / target_h;
        float orthographicSize = (target_h / 2f / pixcelPerUnit);

        // 縦に長い
        if(aspect < targetAspect)
        {
            float bgScale_w = target_w / screen_w;
            float camHeight = target_h / (screen_h * bgScale_w);
            refCamera.rect = new Rect(0f, (1f - camHeight) * 0.5f, 1f, camHeight);
        }
        // 横に長い
        else
        {
            // カメラのorthographicSizeを横の長さに合わせて設定しなおす
            float bgScale = aspect / targetAspect;
            orthographicSize *= bgScale;

            float bgScale_h = target_h / screen_h;
            float camWidth = target_w / (screen_w * bgScale_h);
            refCamera.rect = new Rect((1f - camWidth) * 0.5f, 0f, camWidth, 1f);
        }

        refCamera.orthographicSize = orthographicSize;

        m_width = Screen.width;
        m_height = Screen.height;
    }
}
