using UnityEngine;
using UnityEngine.UI;


public class DeathMenu : MonoBehaviour
{
    public enum DeathMenuState {Open, Closed}
    [SerializeField] private DeathMenuState menuState;
    private Transform[] buttons;
    void Awake()
    { 
        buttons = GetComponentsInChildren<Transform>();
        foreach(var button in buttons)
        {
            if(button.gameObject != gameObject)
            {
                button.gameObject.SetActive(false);
            }
        }
    }

    public void OpenMenu()
    {
        foreach(var button in buttons)
        {
            button.gameObject.SetActive(true);
        }
        
        menuState = DeathMenuState.Open;
        Time.timeScale = 0;
    }
     public void Quit()
    {
        Application.Quit();
    }
     public void TryAgain()
    {
        Time.timeScale = 1f;
    }
}
