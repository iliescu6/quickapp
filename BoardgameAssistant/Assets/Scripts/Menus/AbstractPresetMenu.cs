using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public delegate void ShowSceneAction(SerializablePreset text);
public delegate void DeletePresetAction(SerializablePreset preset, bool deleteIt = false);
public delegate void ShowLifeCounterSettings(SerializablePreset text);
public delegate void AddButtonDelegate();
public delegate void BackButtonDelegate();

abstract public class AbstractPresetMenu : MonoBehaviour
{
    //todo initially this was a simple class and later decided on making it abstract thus the public, make getters/setters maybe if not too lazy 
    public string path;

    [SerializeField] public ScriptableObjectUI colorButtonMap;
    [SerializeField] public Button addPreset;
    [SerializeField] public Button backButton;
    [SerializeField] public Lifecounterbuttonprefab buttonPrefab;
    [SerializeField] public GameObject container;
    [SerializeField] public PresetList presetList;

    public List<Lifecounterbuttonprefab> prefabButtons = new List<Lifecounterbuttonprefab>();
    public SaveLoadPresets data = new SaveLoadPresets();

    ShowLifeCounterSettings localShowSettingsAction;
    ShowSceneAction localShowSceneAction;
    DeletePresetAction localDeletePresetAction;

    public virtual void Initialize(string fileName, ShowSceneAction showSceneAction, ShowLifeCounterSettings showSettings, DeletePresetAction deleteIt)
    {
        path = Application.persistentDataPath + fileName;// "/LifeCounterList.json";
        data.LoadData(path, ref presetList);
        if ((presetList == null || presetList.list.Count == 0) && PlayerPrefs.GetInt("InitialTemplate") == 0)
        {
            PlayerPrefs.SetInt("InitialTemplate", 1);
            CreateInitialPresets(ref presetList);
        }
        localShowSceneAction = showSceneAction;
        localShowSettingsAction = showSettings;
        localDeletePresetAction = deleteIt;

        if (presetList.list != null && presetList.list.Count <= 7)
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

                SerializablePreset tempPreset = presetList.list[i];
                go.SetUpButton(sprite, localShowSceneAction, presetList.list[i], localDeletePresetAction);
                go.editButton.onClick.AddListener(() => localShowSettingsAction.Invoke(go.localPreset));
                prefabButtons.Add(go);
            }
        }
    }

    public void Show(SerializablePreset preset = null)
    {
        gameObject.SetActive(true);

        //TODO figure out how to make it generic, losing too much time now
        if (preset != null)
        {
            Lifecounterbuttonprefab go = prefabButtons.FirstOrDefault(x => x.buttonName.text == preset.currentButtonName);
            if (go != null)
            {
                Sprite sprite = colorButtonMap.buttonColors.FirstOrDefault(x => x.color == preset.buttonColor).sprite;
                go.SetUpButton(sprite, localShowSceneAction, preset, localDeletePresetAction);
            }
        }
        //else if (resourcePreset != null)
        //{
        //    Lifecounterbuttonprefab go = prefabButtons.FirstOrDefault(x => x.buttonName.text == resourcePreset.currentButtonName);
        //    if (go != null)
        //    {
        //        Sprite sprite = colorButtonMap.buttonColors.FirstOrDefault(x => x.color == resourcePreset.buttonColor).sprite;
        //        go.SetUpButton( sprite, localShowSceneAction,null, resourcePreset);
        //    }
        //}




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

                if (presetList.list != null)
                {
                    SerializablePreset tempPreset = presetList.list[i];
                    go.SetUpButton(sprite, localShowSceneAction, presetList.list[i], localDeletePresetAction);
                    go.editButton.onClick.AddListener(() => localShowSettingsAction.Invoke(go.localPreset));
                }
                prefabButtons.Add(go);
            }
        }
    }

    public void InitializeAddPresetBackButtons(AddButtonDelegate addButtonDelegate, BackButtonDelegate backButtonDelegate)
    {
        addPreset.onClick.AddListener(() => addButtonDelegate.Invoke());
        backButton.onClick.AddListener(() => backButtonDelegate.Invoke());
    }

    public void SerializePreset(SerializablePreset preset, bool deleteIt = false)
    {
        if (deleteIt && presetList.list != null)
        {
            presetList.list.Remove(preset);
            data.SaveData(path, ref presetList);
        }
        else if (presetList.list == null || !presetList.list.Contains(preset))
        {
            if (presetList.list == null)
            {
                presetList.list = new List<SerializablePreset>();
            }
            presetList.list.Add(preset);
            data.SaveData(path, ref presetList);
        }
        else if (presetList.list.FirstOrDefault(x => x.currentButtonName == preset.currentButtonName) != null || presetList.list.FirstOrDefault(x => x.currentButtonName == preset.previousButtonName) != null)
        {
            SerializablePreset temp;
            temp = presetList.list.FirstOrDefault(x => x.currentButtonName == preset.currentButtonName);
            if (temp == null)
            {
                temp = presetList.list.FirstOrDefault(x => x.currentButtonName == preset.previousButtonName);
            }
            temp = preset;
            data.SaveData(path, ref presetList);
        }
    }

    public void CreateInitialPresets(ref PresetList presetList)
    {
        SerializedResourcePreset resource = new SerializedResourcePreset();
        resource.resourceInfo.Add(new Resource("Cash", 1500));
        SerializablePreset presetResources = new SerializablePreset();
        presetResources.currentButtonName = "Capitalist Game";
        presetResources.buttonColor = "red";
        presetResources.resource = resource;

        presetList.list.Add(presetResources);
        data.SaveData(Application.persistentDataPath + "/InventoryList.json", ref presetList);


        presetList.list = new List<SerializablePreset>();
        presetResources.currentButtonName = "YGO 8000";
        presetResources.lifeCounter.buttonSettings = "P:2 Life:8000 ±100";
        presetResources.lifeCounter.startingLifePoints = 8000;
        presetResources.buttonColor = "red";
        presetResources.lifeCounter.increment = 100;
        presetResources.lifeCounter.players = 1;
        presetResources.lifeCounter.matchType = 0;

        presetList.list.Add(presetResources);

        presetResources = new SerializablePreset();
        presetResources.currentButtonName = "MTG";
        presetResources.lifeCounter.buttonSettings = "P:2 Life:20 ±1";
        presetResources.lifeCounter.startingLifePoints = 20;
        presetResources.buttonColor = "red";
        presetResources.lifeCounter.increment = 1;
        presetResources.lifeCounter.players = 1;
        presetResources.lifeCounter.matchType = 0;

        presetList.list.Add(presetResources);
        data.SaveData(Application.persistentDataPath + "/LifeCounterList.json", ref presetList); 
    }

}

[Serializable]
public class PresetList
{
    public List<SerializablePreset> list = new List<SerializablePreset>();

    public void Add<T>(T preset)
    {
        if (preset.GetType() == typeof(SerializablePreset))
        {
            SerializablePreset temp = preset as SerializablePreset;
            list.Add(temp);
        }
    }

    public bool Contains<T>(T preset)
    {
        if (preset.GetType() == typeof(SerializablePreset))
        {
            SerializablePreset temp = preset as SerializablePreset;
            return list.Contains(temp);
        }

        return false;
    }
}
