using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class GameMenager : MonoBehaviour
{
    
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        LoadScene("Start");
    }

    public void LoadScene(string loadScene)
    {
        SceneManager.LoadScene(loadScene);
    }

    public void LoadAndUnloadScene(string loadScene, string unloadScene = null)
    {
        // if(unloadScene != null && unloadScene != SceneManager.GetActiveScene().name)
        // { ovak je prof na ponavljanju ali meni je radilo duple igrace
        //     SceneManager.UnloadSceneAsync(unloadScene);
        // }
        // SceneManager.LoadSceneAsync(loadScene, LoadSceneMode.Additive);
        SceneManager.LoadScene(loadScene);
        if (unloadScene != null && unloadScene != SceneManager.GetActiveScene().name)
        {
            SceneManager.UnloadSceneAsync(unloadScene);
        }
    }

}
