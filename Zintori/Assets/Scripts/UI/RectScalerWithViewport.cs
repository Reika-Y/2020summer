using UnityEngine;

// キャンバスのアスペクト比を固定する
[ExecuteAlways]
[RequireComponent(typeof(RectTransform))]
public class RectScalerWithViewport : MonoBehaviour
{
    [SerializeField]
    private RectTransform refRect = null;

    [SerializeField]
    private Vector2 referenceResolution = new Vector2(1080, 1920);

    [Range(0, 1)]
    [SerializeField]
    private float matchWidthOrHeight = 0;

    private float m_width = -1;
    private float m_height = -1;

    private const float kLogBase = 2;

    private void Start()
    {
        if (refRect == null)
        {
            refRect = GetComponent<RectTransform>();
        }
        UpdateRect();
    }

    private void Update()
    {
        UpdateRectWithCheck();
    }

    private void UpdateRectWithCheck()
    {
        Camera cam = Camera.main;
        float width = cam.rect.width * Screen.width;
        float height = cam.rect.height * Screen.height;
        if(m_width == width && m_height == height)
        {
            return;
        }
        UpdateRect();
    }

    private void UpdateRect()
    {
        Camera cam = Camera.main;
        float width = cam.rect.width * Screen.width;
        float height = cam.rect.height * Screen.height;
        m_width = width;
        m_height = height;

        // canvas scalerから引用
        float logWidth = Mathf.Log(width / referenceResolution.x, kLogBase);
        float logHeight = Mathf.Log(height / referenceResolution.y, kLogBase);
        float logWeightedAverage = Mathf.Lerp(logWidth, logHeight, matchWidthOrHeight);
        float scale = Mathf.Pow(kLogBase, logWeightedAverage);

        // スケールで縮まるので領域だけ広げる
        float revScale = 1f / scale;
        refRect.sizeDelta = new Vector2(m_width * revScale, m_height * revScale);
    }
}
