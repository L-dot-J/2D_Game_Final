using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class WinMenu : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private GameObject sfxPlayerWin;

    void Awake()
    {
        Instantiate(sfxPlayerWin, transform.position, Quaternion.identity);
    }
     public void Quit()
    {
        Application.Quit();
    }
     public void TryAgain()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("LoaderScene");
     
    }
    
}
