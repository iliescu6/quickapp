using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// this was made gamejam style and in day 2 am crying at the lack of comments and confusing terms for screens vs menu
/// pray radu and marius don't see this code 
/// Main menu-> type menu(life counter menu)-> settings for it-> screen (actuall stuff happening(dice roll,life counter etc)
/// </summary>
public class ScreensController : SingletonTemplate<ScreensController>
{
    [SerializeField] GameObject mainMenu;
    [SerializeField] Button lifecounterMenuButton;
    [SerializeField] Button diceRollerMenuButton;
    [SerializeField] Button inventoryMenuButton;
    [SerializeField] LifeCounterMenu lifeCounterMenu;
    //[SerializeField] DiceRollerScreen diceRollerMenu;
    [SerializeField] InventoryMenu inventoryMenu;
    [SerializeField] public GameObject screensMenuPopUp;
    [SerializeField] Button optionsMenuPopup;
    [SerializeField] Button resumeButton;
    [SerializeField] Button restartButton;
    [SerializeField] Button mainMenuButton;
    [SerializeField] Button misceleneousButton;
    [SerializeField] MiscelaneousMenu misceleneousMenu;
    public ScreenInterface currentScreen;
    private void Awake()
    {
        base.Awake();
        lifecounterMenuButton.onClick.AddListener(InitializeLifeCounterMenu);
        inventoryMenuButton.onClick.AddListener(InitializeInventoryMenu);
        optionsMenuPopup.onClick.AddListener(ShowPopUpMenu);
        resumeButton.onClick.AddListener(Resume);
        restartButton.onClick.AddListener(Restart);
        mainMenuButton.onClick.AddListener(ReturnToMainMenu);
        misceleneousButton.onClick.AddListener(ShowMiscelenaousMenu);
    }

    void InitializeLifeCounterMenu()
    {
        lifeCounterMenu.Initialize();
        lifeCounterMenu.Show();
        mainMenu.SetActive(false);
    }

    void InitializeInventoryMenu()
    {
        inventoryMenu.Initialize();
        inventoryMenu.Show();
        mainMenu.SetActive(false);
    }

    public void ShowMaineMenu()
    {
        mainMenu.SetActive(true);
    }

    public void ShowMiscelenaousMenu()
    {
        misceleneousMenu.gameObject.SetActive(true);
        screensMenuPopUp.SetActive(false);
    }
    public void ShowOptionsButton()
    {
        optionsMenuPopup.gameObject.SetActive(true);
    }
    void ShowPopUpMenu()
    {
        screensMenuPopUp.SetActive(true);
    }

    void Resume()
    {
        screensMenuPopUp.SetActive(false);
    }

    void Restart()
    {
        currentScreen.Restart();
        Resume();
    }

    void ReturnToMainMenu()
    {
        currentScreen.DeleteCurrentMatchSession();
        screensMenuPopUp.gameObject.SetActive(false);
        mainMenu.SetActive(true);
    }
}
