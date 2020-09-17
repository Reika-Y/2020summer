using UnityEngine;
using UnityEngine.SceneManagement;

// シーン遷移を行うボタン用
public class SceneChangeButton : MonoBehaviour
{
    [SerializeField]
    private EnumScene scene;

    public void OnClick()
    {
        Transition.Instance.LoadScene(scene.ToString(), 1f);
    }
}
