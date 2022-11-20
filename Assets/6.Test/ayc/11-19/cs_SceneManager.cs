using UnityEngine;

class Class_SceneLoad
{
    /*
     * 0.Start Scene
     * 1.Main Scene
     * 2.WORLD MAP
     */
    public GameObject[] scenes;
    public void SetLoadScene()
    {
        foreach (GameObject s in scenes) s.SetActive(false);
    }

    public void Method_LoadScene(int _int)
    {
        scenes[_int].SetActive(true);
    }
}

class StartScene : Class_SceneLoad
{
}

public class cs_SceneManager: MonoBehaviour
{
}


