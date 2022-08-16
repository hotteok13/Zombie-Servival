using UnityEngine;


public class VersatileButton : MonoBehaviour
{
    public void Scene(string name)
    {
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
