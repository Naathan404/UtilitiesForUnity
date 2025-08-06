using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] private GameObject sceneCanvas;
    [SerializeField] private GameObject loadingScene;
    [SerializeField] private Slider loadingBar;

    /// Can use button event to call this function  with sceneName = name of the scene to load
    public void LoadScene(string sceneName)
    {
        sceneCanvas.SetActive(false);
        loadingScene.SetActive(true);
        StartCoroutine(LoadAsync(sceneName));
    }

    IEnumerator LoadAsync(string sceneName)
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(sceneName);
        while (!loadOperation.isDone)
        {
            float progressValue = Mathf.Clamp01(loadOperation.progress / 0.99f);
            loadingBar.value = progressValue;
            Time.timeScale = 1f;
            yield return null;
        }
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
