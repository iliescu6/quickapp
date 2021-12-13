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



    public void Initialize(SerializableLifeCounterPreset selectedPreset)
    {
        base.Initialize(selectedPreset, menu, SavePreset);

        selectedButtonSettingsText.text = currentPreset.buttonSettings;
        startingLifeInputField.text = currentPreset.startingLifePoints.ToString();
        incrementInputField.text = currentPreset.increment.ToString();

        //Dropdown variables
        PopulateDropdown(playerDropDown, (Player)currentPreset.players);
        playerDropDown.value = currentPreset.players;
        playerDropDown.onValueChanged.AddListener(delegate { SetUpMatchType(); });
        SetUpMatchType();
        matchType.value = currentPreset.matchType;
    }
    void Update()
    {
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
                PopulateDropdown(matchType, (TwoPlayers)currentPreset.matchType);
                break;
            case (2):
                matchType.gameObject.SetActive(true);
                PopulateDropdown(matchType, (ThreePlayers)currentPreset.matchType);
                break;
            case (3):
                matchType.gameObject.SetActive(true);
                PopulateDropdown(matchType, (FourPlayer)currentPreset.matchType);
                break;
        }
    }

    public void SavePreset()
    {
        currentPreset.currentButtonName = selectedButtonNameText.text;
        currentPreset.buttonSettings = selectedButtonSettingsText.text;
        currentPreset.startingLifePoints = int.Parse(startingLifeInputField.text);
        currentPreset.increment = int.Parse(incrementInputField.text);
        currentPreset.players = playerDropDown.value;
        currentPreset.matchType = matchType.value;
        menu.SerializePreset(currentPreset);
        menu.Show(currentPreset);
        gameObject.SetActive(false);
    }
}

[Serializable]
public class SerializableLifeCounterPreset 
{
    //should've thought this from the start   
    public string previousButtonName;
    public string currentButtonName;
    public string buttonColor;
    public string buttonSettings;
    public int startingLifePoints;
    public int increment;
    public int players;
    public int matchType;
}

public class ResourceInfo
{

}

public enum Player { One, Two, Three, Four }

public enum TwoPlayers { OnevOne, Coop }
public enum ThreePlayers { FreeForAll, OnevTwo, Coop }
public enum FourPlayer { FreeForAll, OnevThree, TwovTwo }
