using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
 
    public void Quit()
    {
        Application.Quit();
    }
     public void StartGame()
    {
        SceneManager.LoadScene("Level 1");
     
    }
}
