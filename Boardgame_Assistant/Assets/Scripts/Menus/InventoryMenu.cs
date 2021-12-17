using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryMenu : AbstractPresetMenu
{
    [SerializeField] public InventoryScreen screen;
    [SerializeField] public InventoryPresetSettings settingsMenu;

    bool initialized = false;
    string fileName = "/InventoryList.json";
    public void Initialize()
    {
        if (initialized)
        {
            return;
        }
        base.Initialize(fileName, ShowInventoryScreen, ShowInventorySettings,base.SerializePreset);
        base.InitializeAddPresetBackButtons(AddNewPreset, BackButton);
        initialized = true;
    }

    public void ShowInventorySettings(SerializablePreset preset)
    {
        settingsMenu.gameObject.SetActive(true);
        settingsMenu.Initialize(preset);
        gameObject.SetActive(false);
    }

    public void ShowInventoryScreen(SerializablePreset preset)
    {
        screen.Initialize(preset);
        screen.gameObject.SetActive(true);
    }

    public void AddNewPreset()
    {
        settingsMenu.gameObject.SetActive(true);
        SerializablePreset newPreset = new SerializablePreset();
        settingsMenu.Initialize(newPreset);
        gameObject.SetActive(false);
    }

    public void BackButton()
    {
        ScreensController.Instance.ShowMaineMenu();
        gameObject.SetActive(false);
    }   
}
