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
    string fileName = "/LifeCounterList.json";
    public void Initialize()
    {
        base.Initialize(fileName, ShowCounterScreen, ShowLifeCounterSettings);   
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


}



