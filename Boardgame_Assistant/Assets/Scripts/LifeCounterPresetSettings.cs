using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using System.IO;
using System.Linq;

public class LifeCounterPresetSettings : MonoBehaviour
{
    [SerializeField] ScriptableObjectUI colorButtonMap;
    [SerializeField] Image selectedButtonImage;
    [SerializeField] TMP_Text selectedButtonNameText;
    [SerializeField] TMP_Text selectedButtonSettingsText;
    [SerializeField] TMP_InputField nameInputField;
    [SerializeField] TMP_InputField startingLifeInputField;
    [SerializeField] TMP_InputField incrementInputField;
    [SerializeField] TMP_Dropdown playerDropDown;
    [SerializeField] TMP_Dropdown matchType;
    [SerializeField] Button saveButton;
    [SerializeField] Button exitButton;
    [SerializeField] List<Button> images = new List<Button>();
    [SerializeField] LifeCounterMenu menu;
    SerializableLifeCounterPreset currentPreset;
    public void Initialize(SerializableLifeCounterPreset selectedPreset)
    {
        currentPreset = selectedPreset;

        selectedButtonNameText.text = currentPreset.currentButtonName;
        nameInputField.text = currentPreset.currentButtonName;
        selectedButtonSettingsText.text = currentPreset.buttonSettings;
        startingLifeInputField.text = currentPreset.startingLifePoints.ToString();
        incrementInputField.text = currentPreset.increment.ToString();

        //Dropdown variables
        PopulateDropdown(playerDropDown, (Player)currentPreset.players);
        playerDropDown.value = currentPreset.players;
        playerDropDown.onValueChanged.AddListener(delegate { SetUpMatchType(); });       
        SetUpMatchType();
        matchType.value = currentPreset.matchType;

        Sprite sprite   = colorButtonMap.buttonColors[0].sprite; 
        if (currentPreset != null && !string.IsNullOrEmpty(currentPreset.buttonColor))
        {
            sprite = colorButtonMap.buttonColors.FirstOrDefault(x => x.color == currentPreset.buttonColor).sprite;
        }

        saveButton.onClick.AddListener(SavePreset);
        exitButton.onClick.AddListener(() =>
        {
            menu.Show();
            gameObject.SetActive(false);
        });

        foreach (Button image in images)
        {
            image.onClick.AddListener(delegate { SetSelectedImage(image); });
        }
    }

    void Update()
    {
        //Initialize();
        selectedButtonNameText.text = nameInputField.text;
        selectedButtonSettingsText.text = string.Format("P:{0} Life:{1} ±{2}", playerDropDown.value + 1, startingLifeInputField.text, incrementInputField.text);
    }

    public void SetSelectedImage(Button button)
    {
        selectedButtonImage.sprite = button.image.sprite;
        currentPreset.buttonColor = button.name;
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

    void PopulateDropdown(TMP_Dropdown dropdown, Enum targetEnum)
    {
        Type enumType = targetEnum.GetType();
        List<TMP_Dropdown.OptionData> newOptions = new List<TMP_Dropdown.OptionData>();

        for (int i = 0; i < Enum.GetNames(enumType).Length; i++)
        {
            newOptions.Add(new TMP_Dropdown.OptionData(Enum.GetName(enumType, i)));
        }

        dropdown.ClearOptions();
        dropdown.AddOptions(newOptions);
    }


    public void SavePreset()
    {

        currentPreset.currentButtonName = selectedButtonNameText.text;
        currentPreset.buttonSettings = selectedButtonSettingsText.text;
        currentPreset.startingLifePoints = int.Parse(startingLifeInputField.text);
        currentPreset.increment = int.Parse(incrementInputField.text);
        currentPreset.players = playerDropDown.value;
        currentPreset.matchType = matchType.value;
        //Texture2D tex = selectedButtonImage.sprite.texture;
        //currentPreset.xSprite = tex.width;
        //currentPreset.ySprite = tex.height;
        //currentPreset.bytes = ImageConversion.EncodeToPNG(tex);
        menu.SavePreset(currentPreset);
        //string serializedPreset = JsonUtility.ToJson(serialized);
        //string path = Application.persistentDataPath + string.Format("/{}", serialized.currentButtonName);
        //File.WriteAllText(path, serializedPreset);
        menu.Show(currentPreset);
        gameObject.SetActive(false);
    }

    public void DeserializeLifeCounterPreset()
    {
        // string path = Application.persistentDataPath + string.Format("/{}", serialized.buttonName);
        string deserializedString;// = File.ReadAllText(path);
    }
}

[Serializable]
public class SerializableLifeCounterPreset
{
    public string previousButtonName;
    public string currentButtonName;
    public string buttonSettings;
    public int startingLifePoints;
    public int increment;
    public int players;
    public int matchType;
    public string buttonColor;
    public int xSprite;
    public int ySprite;
    public byte[] bytes;
}

public enum Player { One, Two, Three, Four }

public enum TwoPlayers { OnevOne, Coop }
public enum ThreePlayers { FreeForAll, OnevTwo, Coop }
public enum FourPlayer { FreeForAll, OnevThree, TwovTwo }
