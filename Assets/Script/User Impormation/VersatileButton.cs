using UnityEngine;


public class VersatileButton : MonoBehaviour
{
    public void Scene(string name)
    {
        Time.timeScale = 1;
        GameManager.instance.count = 0;
        Loding.LoadScene(name);
    }

    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;

#else
        Application.Quit();
#endif
    }

}
