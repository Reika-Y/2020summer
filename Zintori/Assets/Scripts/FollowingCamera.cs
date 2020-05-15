using UnityEngine;

public class FollowingCamera : MonoBehaviour
{

    // 追尾するターゲット
    [SerializeField] private GameObject target = null;
    [SerializeField] private Vector3 offset = Vector3.zero;

    [SerializeField] private float distance = 4.0f;
    [SerializeField] private float polarAngle = 45.0f;
    [SerializeField] private float azimuthalAngle = 45.0f;

    [SerializeField] private float minDistance = 1.0f;
    [SerializeField] private float maxDistance = 7.0f;
    [SerializeField] private float minPolarAngle = 5.0f;
    [SerializeField] private float maxPolarAngle = 75.0f;
    [SerializeField] private float mouseXSensitivity = 5.0f;
    [SerializeField] private float mouseYSensitivity = 5.0f;
    [SerializeField] private float scrollSensitivity = 5.0f;

    private void LateUpdate()
    {
        UpdateDistance(Input.GetAxis("Mouse ScrollWheel"));
        UpdatePosition(target.transform.position + offset);
        transform.LookAt(target.transform.position);
    }

    // 角度の更新
    void UpdateAngle(float x,float y)
    {
        x = azimuthalAngle - x * mouseXSensitivity;
        azimuthalAngle = Mathf.Repeat(x, 360);

        y = polarAngle + y * mouseYSensitivity;
        polarAngle = Mathf.Clamp(y, minPolarAngle, maxPolarAngle);
    }

    // ズーム処理
    void UpdateDistance(float scroll)
    {
        scroll = distance - scroll * scrollSensitivity;
        distance = Mathf.Clamp(scroll, minDistance, maxDistance);
    }

    void UpdatePosition(Vector3 lookAtPos)
    {
        var da = azimuthalAngle * Mathf.Deg2Rad;
        var dp = polarAngle * Mathf.Deg2Rad;
        transform.position = new Vector3(
            lookAtPos.x + distance * Mathf.Sin(dp) * Mathf.Cos(da),
            lookAtPos.y + distance * Mathf.Cos(dp),
            lookAtPos.z + distance * Mathf.Sin(dp) * Mathf.Sin(da));
    }
}
