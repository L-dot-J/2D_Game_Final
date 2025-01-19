using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenager : MonoBehaviour
{
    void Start()
    {
        SceneManager.LoadSceneAsync("Level 1",LoadSceneMode.Additive);
    }

    void Update()
    {
        
    }
}
