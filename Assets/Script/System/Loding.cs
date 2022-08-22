using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Loding : MonoBehaviour
{
    static string sceneName;
    [SerializeField] Slider progress;

    public static void LoadScene(string name)
    {
        sceneName = name;
        SceneManager.LoadScene("Loding");

    }
    void Start()
    {
        StartCoroutine(nameof(LoadProcess));
    }

    IEnumerator LoadProcess()
    {
        AsyncOperation scene = SceneManager.LoadSceneAsync(sceneName);

        scene.allowSceneActivation = false;

        float timer = 0f;

        while (!scene.isDone)
        {
            yield return null;

            if (scene.progress < 0.9f)
            {
                progress.value = scene.progress;
                
            }
            else
            {
                timer += Time.unscaledTime;
                progress.value = Mathf.Lerp(0.9f, 1f, timer);
                
                if (progress.value >= 1f)
                {
                    scene.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }
}
