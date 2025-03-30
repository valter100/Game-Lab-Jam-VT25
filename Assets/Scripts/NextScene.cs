using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    [SerializeField] string sceneName;

    public void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }
    public void SwitchScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
