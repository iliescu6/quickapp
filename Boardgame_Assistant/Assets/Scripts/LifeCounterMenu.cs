using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LifeCounterMenu : MonoBehaviour
{
    string path;

    [SerializeField] ScriptableObjectUI colorButtonMap;
    [SerializeField] Button addPreset;
    [SerializeField] Button backButton;
    [SerializeField] Lifecounterbuttonprefab buttonPrefab;
    [SerializeField] GameObject container;
    [SerializeField] LifeCounterPresetSettings settingsMenu;
    [SerializeField] PresetList presetList;
    [SerializeField] LifeCounterScreen screen;
    List<Lifecounterbuttonprefab> prefabButtons = new List<Lifecounterbuttonprefab>();
    SaveLoadPresets data = new SaveLoadPresets();

    private void Awake()
    {
        path = Application.persistentDataPath + "/LifeCounterList.json";
        data.LoadData(path, ref presetList);

        addPreset.onClick.AddListener(AddNewPreset);
        backButton.onClick.AddListener(BackButton);

        if (presetList.list != null)
        {
            for (int i = 0; i < presetList.list.Count; i++)
            {
                Lifecounterbuttonprefab go = (Lifecounterbuttonprefab)Instantiate(buttonPrefab, container.transform);
                ButtonColor color = colorButtonMap.buttonColors.FirstOrDefault(x => x.color == presetList.list[i].buttonColor);

                Sprite sprite = colorButtonMap.buttonColors[0].sprite;
                if (color != null)
                {
                    sprite = color.sprite;
                }

                SerializableLifeCounterPreset tempPreset = presetList.list[i];
                go.SetUpButton(presetList.list[i].currentButtonName, presetList.list[i].buttonSettings, presetList.list[i], sprite, delegate { ShowCounterScreen(tempPreset); });
                go.editButton.onClick.AddListener(delegate { ShowLifeCounterSettings(go.localPreset); });
                prefabButtons.Add(go);
            }
        }
    }

    public void BackButton()
    {
        ScreensController.Instance.ShowMaineMenu();
        gameObject.SetActive(false);
    }

    public void Show(SerializableLifeCounterPreset preset = null)
    {
        gameObject.SetActive(true);
        if (preset != null)
        {
            Lifecounterbuttonprefab go = prefabButtons.FirstOrDefault(x => x.buttonName.text == preset.currentButtonName);
            if (go != null)
            {
                Sprite sprite = colorButtonMap.buttonColors.FirstOrDefault(x => x.color == preset.buttonColor).sprite;
                go.SetUpButton(preset.currentButtonName, preset.buttonSettings, preset, sprite, delegate { ShowCounterScreen(preset); });
            }
        }
        if (presetList.list.Count != prefabButtons.Count)
        {
            for (int i = prefabButtons.Count; i < presetList.list.Count; i++)
            {
                Lifecounterbuttonprefab go = (Lifecounterbuttonprefab)Instantiate(buttonPrefab, container.transform);

                Sprite sprite = colorButtonMap.buttonColors[0].sprite;
                ButtonColor color = colorButtonMap.buttonColors.FirstOrDefault(x => x.color == preset.buttonColor);
                if (color != null)
                {
                    sprite = color.sprite;
                }
                SerializableLifeCounterPreset tempPreset = presetList.list[i];
                go.SetUpButton(presetList.list[i].currentButtonName, presetList.list[i].buttonSettings, presetList.list[i], sprite, delegate { ShowCounterScreen(tempPreset); });
                go.editButton.onClick.AddListener(delegate { ShowLifeCounterSettings(go.localPreset); });
                prefabButtons.Add(go);
            }
        }
    }

    public void ShowCounterScreen(SerializableLifeCounterPreset preset)
    {
        screen.Initialize(preset);
        screen.gameObject.SetActive(true);
    }

    public void ShowLifeCounterSettings(SerializableLifeCounterPreset preset)
    {
        settingsMenu.gameObject.SetActive(true);
        settingsMenu.Initialize(preset);
        gameObject.SetActive(false);
    }

    public void AddNewPreset()
    {
        settingsMenu.gameObject.SetActive(true);
        SerializableLifeCounterPreset newPreset = new SerializableLifeCounterPreset();
        settingsMenu.Initialize(newPreset);
        gameObject.SetActive(false);
    }

    public void SavePreset(SerializableLifeCounterPreset preset)
    {
        if (presetList.list == null || !presetList.list.Contains(preset))
        {
            if (presetList.list == null)
            {
                presetList.list = new List<SerializableLifeCounterPreset>();
            }
            presetList.list.Add(preset);
            data.SaveData(path, ref presetList);
        }
        else if (presetList.list.FirstOrDefault(x => x.currentButtonName == preset.currentButtonName) != null || presetList.list.FirstOrDefault(x => x.currentButtonName == preset.currentButtonName) != null)
        {
            SerializableLifeCounterPreset temp;
            temp = presetList.list.FirstOrDefault(x => x.currentButtonName == preset.currentButtonName);
            if (temp == null)
            {
                temp = presetList.list.FirstOrDefault(x => x.currentButtonName == preset.previousButtonName);
            }
            temp = preset;
            data.SaveData(path, ref presetList);
        }
    }
}

[Serializable]
public class PresetList
{
    public List<SerializableLifeCounterPreset> list = new List<SerializableLifeCounterPreset>();
}

