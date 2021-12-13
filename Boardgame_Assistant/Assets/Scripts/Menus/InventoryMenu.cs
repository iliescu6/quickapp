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
        base.Initialize(fileName, ShowInventoryScreen, ShowInventorySettings);
        base.InitializeAddPresetBackButtons(AddNewPreset, BackButton);
        initialized = true;
    }

    public void ShowInventorySettings(SerializableLifeCounterPreset preset)
    {
        settingsMenu.gameObject.SetActive(true);
        settingsMenu.Initialize(preset);
        gameObject.SetActive(false);
    }

    public void ShowInventoryScreen(SerializableLifeCounterPreset preset)
    {
        screen.Initialize(preset);
        screen.gameObject.SetActive(true);
    }

    public void AddNewPreset()
    {
        settingsMenu.gameObject.SetActive(true);
        SerializableLifeCounterPreset newPreset = new SerializableLifeCounterPreset();
        settingsMenu.Initialize(newPreset);
        gameObject.SetActive(false);
    }

    public void BackButton()
    {
        ScreensController.Instance.ShowMaineMenu();
        gameObject.SetActive(false);
    }

    public void SavePreset()
    {
        //currentPreset.currentButtonName = selectedButtonNameText.text;
        //currentPreset.buttonSettings = selectedButtonSettingsText.text;
        //currentPreset.startingLifePoints = int.Parse(startingLifeInputField.text);
        //currentPreset.increment = int.Parse(incrementInputField.text);
        //currentPreset.players = playerDropDown.value;
        //currentPreset.matchType = matchType.value;
        //menu.SerializePreset(currentPreset);
        //menu.Show(currentPreset);
        //gameObject.SetActive(false);
    }
}
