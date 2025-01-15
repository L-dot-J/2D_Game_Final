using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public enum MainMenuState { Open, Closed }
    [SerializeField] private MainMenuState menuState;
    private Transform[] buttons;
    void Awake()
    {
        buttons = GetComponentsInChildren<Transform>();
        foreach (var button in buttons)
        {
            if(button.gameObject != gameObject)
            {
                button.gameObject.SetActive(false);
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (menuState == MainMenuState.Open)
            {
                CloseMenu();
            }
            else 
            {
                OpenMenu();
            }
        }
    }
    public void OpenMenu()
    {
        foreach (var button in buttons)
        {
            button.gameObject.SetActive(true);
        }

        menuState = MainMenuState.Open;
        Time.timeScale = 0;
    }

    public void CloseMenu()
    {
        foreach (var button in buttons)
        {
            if(button.gameObject != gameObject)
            {
                button.gameObject.SetActive(false);
            }

            Time.timeScale = 1f;
            menuState = MainMenuState.Closed;
        }
    }
    public void Quit()
    {
        Application.Quit();
    }

    public void Continue()
    {
        CloseMenu();
    }

 
}
