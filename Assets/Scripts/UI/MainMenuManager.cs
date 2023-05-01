using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{

    [SerializeField]
    public GameObject homeScreen;
    [SerializeField]
    public GameObject newMenu;
    [SerializeField]
    public GameObject loadMenu;
    [SerializeField]
    public GameObject settingsMenu;
    [SerializeField]
    public GameObject exitMenu;

    // Start is called before the first frame update
    public void Awake()
    {
        ReturnHome();
    }

    // Disable all menus within main menu
    public void DisableMenus()
    {
        homeScreen.SetActive(false);
        newMenu.SetActive(false);
        loadMenu.SetActive(false);
        settingsMenu.SetActive(false);
        exitMenu.SetActive(false);
    }

    // Return to home screen
    public void ReturnHome()
    {
        DisableMenus();
        homeScreen.SetActive(true);
    }

    // Manages the selected option from home screen
    public void selectOption(string option)
    {
        Debug.Log("click");
        if (homeScreen.activeSelf)
        {
            DisableMenus();
            switch (option)
            {
                case "new":
                    newMenu.SetActive(true);
                    break;
                case "load":
                    loadMenu.SetActive(true);
                    break;
                case "settings":
                    settingsMenu.SetActive(true);
                    break;
                case "exit":
                    exitMenu.SetActive(true);
                    break;
                default:
                    homeScreen.SetActive(true);
                    break;
            }
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadSaveGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}