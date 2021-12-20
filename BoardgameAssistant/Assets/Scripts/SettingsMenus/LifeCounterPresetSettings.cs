using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using System.IO;
using System.Linq;

public class LifeCounterPresetSettings : AbstractPresetSettings
{



    [SerializeField] TMP_Text selectedButtonSettingsText;

    [SerializeField] TMP_InputField startingLifeInputField;
    [SerializeField] TMP_InputField incrementInputField;
    [SerializeField] TMP_Dropdown playerDropDown;
    [SerializeField] TMP_Dropdown matchType;


    [SerializeField] LifeCounterMenu menu;



    public void Initialize(SerializablePreset selectedPreset)
    {
        base.Initialize(menu, SavePreset, selectedPreset);

        selectedButtonSettingsText.text = currentPreset.lifeCounter.buttonSettings;
        startingLifeInputField.text = currentPreset.lifeCounter.startingLifePoints.ToString();
        incrementInputField.text = currentPreset.lifeCounter.increment.ToString();

        //Dropdown variables
        PopulateDropdown(playerDropDown, (Player)currentPreset.lifeCounter.players);
        playerDropDown.value = currentPreset.lifeCounter.players;
        playerDropDown.onValueChanged.AddListener(delegate { SetUpMatchType(); });
        SetUpMatchType();
        matchType.value = currentPreset.lifeCounter.matchType;
    }
    void Update()
    {
        base.Update();
        selectedButtonSettingsText.text = string.Format("P:{0} Life:{1} ±{2}", playerDropDown.value + 1, startingLifeInputField.text, incrementInputField.text);
    }



    void SetUpMatchType()
    {
        switch (playerDropDown.value)
        {
            case (0):
                matchType.gameObject.SetActive(false);
                break;
            case (1):
                matchType.gameObject.SetActive(true);
                PopulateDropdown(matchType, (TwoPlayers)currentPreset.lifeCounter.matchType);
                break;
            case (2):
                matchType.gameObject.SetActive(true);
                PopulateDropdown(matchType, (ThreePlayers)currentPreset.lifeCounter.matchType);
                break;
            case (3):
                matchType.gameObject.SetActive(true);
                PopulateDropdown(matchType, (FourPlayer)currentPreset.lifeCounter.matchType);
                break;
        }
    }

    public void SavePreset()
    {
        currentPreset.currentButtonName = selectedButtonNameText.text;
        currentPreset.lifeCounter.buttonSettings = selectedButtonSettingsText.text;
        currentPreset.lifeCounter.startingLifePoints = int.Parse(startingLifeInputField.text);
        currentPreset.lifeCounter.increment = int.Parse(incrementInputField.text);
        currentPreset.lifeCounter.players = playerDropDown.value;
        currentPreset.lifeCounter.matchType = matchType.value;
        //menu.SerializePreset(currentPreset);
        menu.SerializePreset(currentPreset);
        menu.Show(currentPreset);
        gameObject.SetActive(false);
    }
}

[Serializable]
public class SerializablePreset
{

    //should've thought this from the start   
    public string previousButtonName;
    public string currentButtonName;
    public string buttonColor;
    public SerializedResourcePreset resource = new SerializedResourcePreset();
    public SerializableLifePreset lifeCounter = new SerializableLifePreset();
}

[Serializable]
public class SerializableLifePreset
{
    public string buttonSettings;
    public int startingLifePoints;
    public int increment;
    public int players;
    public int matchType;
}

[Serializable]
public class SerializedResourcePreset//TODO I may just say fuck it and add it all in one....or..
{
    public List<Resource> resourceInfo = new List<Resource>();
}

[Serializable]
public class Resource
{
    public string resourceName;
    public int resourceStartingAmount;
    public string resourceIcon;//I know there was a better way but i wanna make this app fast
    public Resource(string name, int amount)
    {
        resourceName = name;
        resourceStartingAmount = amount;
    }
    //public int increment;
}

public enum Player { One, Two, Three, Four }

public enum TwoPlayers { OnevOne, Coop }
public enum ThreePlayers { FreeForAll, OnevTwo, Coop }
public enum FourPlayer { FreeForAll, OnevThree, TwovTwo }
