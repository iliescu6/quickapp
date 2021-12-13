using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LifeCounterMenu : AbstractPresetMenu
{
    [SerializeField] public LifeCounterScreen screen;
    [SerializeField] public LifeCounterPresetSettings settingsMenu;
    bool initialized = false;
    string fileName = "/LifeCounterList.json";

    public void Initialize()
    {
        if (initialized)
        {
            return;
        }
        base.Initialize(fileName, ShowCounterScreen, ShowLifeCounterSettings);
        base.InitializeAddPresetBackButtons(AddNewPreset, BackButton);//todo think there was a clever way but again, quick and dirty app
        initialized = true;
    }

    public void ShowLifeCounterSettings(SerializableLifeCounterPreset preset)
    {
        settingsMenu.gameObject.SetActive(true);
        settingsMenu.Initialize(preset);
        gameObject.SetActive(false);
    }

    public void ShowCounterScreen(SerializableLifeCounterPreset preset)
    {
        screen.Initialize(preset);
        screen.gameObject.SetActive(true);
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

}



