using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public delegate void SavePresetButton();

abstract public class AbstractPresetSettings : MonoBehaviour
{
    [SerializeField] public ScriptableObjectUI colorButtonMap;
    [SerializeField] public Image selectedButtonImage;
    [SerializeField] public TMP_Text selectedButtonNameText;
    [SerializeField] public TMP_InputField nameInputField;
    [SerializeField] public Button saveButton;
    [SerializeField] public Button exitButton;
    [SerializeField] public List<Button> images = new List<Button>();
    public AbstractPresetMenu abstractMenu;

    public SerializableLifeCounterPreset currentPreset;
    public void Initialize(SerializableLifeCounterPreset selectedPreset, AbstractPresetMenu _abstractMenu, SavePresetButton spriteSave)
    {
        currentPreset = selectedPreset;
        abstractMenu = _abstractMenu;
        selectedButtonNameText.text = currentPreset.currentButtonName;
        nameInputField.text = currentPreset.currentButtonName;




        Sprite sprite = colorButtonMap.buttonColors[0].sprite;
        if (currentPreset != null && !string.IsNullOrEmpty(currentPreset.buttonColor))
        {
            sprite = colorButtonMap.buttonColors.FirstOrDefault(x => x.color == currentPreset.buttonColor).sprite;
        }

        saveButton.onClick.AddListener(() => spriteSave());
        exitButton.onClick.AddListener(() =>
        {
            abstractMenu.Show();
            gameObject.SetActive(false);
        });

        foreach (Button image in images)
        {
            image.onClick.AddListener(delegate { SetSelectedImage(image); });
        }
    }

    private void Update()
    {
        selectedButtonNameText.text = nameInputField.text;
    }

    public void SetSelectedImage(Button button)
    {
        selectedButtonImage.sprite = button.image.sprite;
        currentPreset.buttonColor = button.name;
    }

    public void PopulateDropdown(TMP_Dropdown dropdown, Enum targetEnum)
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
}
