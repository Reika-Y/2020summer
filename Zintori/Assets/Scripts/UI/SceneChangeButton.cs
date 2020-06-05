using UnityEngine;
using UnityEngine.SceneManagement;

// シーン遷移を行うボタン用
public class SceneChangeButton : MonoBehaviour
{
    [SerializeField]
    private EnumScene scene;

    public void OnClick()
    {
        SceneManager.LoadScene(scene.ToString());
    }
}
