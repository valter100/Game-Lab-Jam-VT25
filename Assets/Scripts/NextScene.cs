using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    [SerializeField] string sceneName;

    public void SwitchScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
