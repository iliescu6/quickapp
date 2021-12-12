using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public delegate void ShowSceneAction(SerializableLifeCounterPreset text);
public delegate void ShowLifeCounterSettings(SerializableLifeCounterPreset text);

abstract public class AbstractPresetMenu : MonoBehaviour
{
    //todo initially this was a simple class and later decided on making it abstract thus the public, make getters/setters maybe if not too lazy 
    public string path;

    [SerializeField] public ScriptableObjectUI colorButtonMap;
    [SerializeField] public Button addPreset;
    [SerializeField] public Button backButton;
    [SerializeField] public Lifecounterbuttonprefab buttonPrefab;
    [SerializeField] public GameObject container;
    [SerializeField] public LifeCounterPresetSettings settingsMenu;
    [SerializeField] public PresetList presetList;
    [SerializeField] public LifeCounterScreen screen;
    public List<Lifecounterbuttonprefab> prefabButtons = new List<Lifecounterbuttonprefab>();
    public SaveLoadPresets data = new SaveLoadPresets();

    ShowLifeCounterSettings localShowSettingsAction;
    ShowSceneAction localShowSceneAction;


    public virtual void Initialize(string fileName, ShowSceneAction showSceneAction, ShowLifeCounterSettings showSettings)
    {
        path = Application.persistentDataPath + fileName;// "/LifeCounterList.json";
        data.LoadData(path, ref presetList);

        localShowSceneAction = showSceneAction;
        localShowSettingsAction = showSettings;


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
                go.SetUpButton(presetList.list[i].currentButtonName, presetList.list[i].buttonSettings, presetList.list[i], sprite, localShowSceneAction);
                go.editButton.onClick.AddListener(() => localShowSettingsAction.Invoke(go.localPreset) );
                prefabButtons.Add(go);
            }
        }
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
                go.SetUpButton(preset.currentButtonName, preset.buttonSettings, preset, sprite, localShowSceneAction);
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
                go.SetUpButton(presetList.list[i].currentButtonName, presetList.list[i].buttonSettings, presetList.list[i], sprite, localShowSceneAction);
                go.editButton.onClick.AddListener(()=>localShowSettingsAction.Invoke(go.localPreset));
                prefabButtons.Add(go);
            }
        }
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
