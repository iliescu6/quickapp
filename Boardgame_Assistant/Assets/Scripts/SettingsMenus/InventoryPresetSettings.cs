using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPresetSettings : AbstractPresetSettings
{
    [SerializeField] InventoryMenu menu;
    [SerializeField] SettingsResourceButton resoureceButtonPrefab;
    [SerializeField] GameObject resourceButtonContainer;
    [SerializeField] TMP_InputField resourceNameInputField;
    [SerializeField] TMP_InputField startingValueInputField;
    [SerializeField] Button minusButton;
    [SerializeField] Button plusButton;

    SettingsResourceButton selectedResourceButton;
    List<Resource> currentList;
    List<SettingsResourceButton> settingsResourceButtons = new List<SettingsResourceButton>();

    public void Initialize(SerializablePreset selectedPreset)
    {
        base.Initialize(menu, SavePreset, selectedPreset);
        currentList = new List<Resource>(selectedPreset.resource.resourceInfo);

        for (int i = settingsResourceButtons.Count - 1; i >= 0; i--)
        {
            Destroy(settingsResourceButtons[i].gameObject);
            Destroy(settingsResourceButtons[i]);
            settingsResourceButtons.RemoveAt(settingsResourceButtons.Count - 1);
        }


        foreach (Resource r in selectedPreset.resource.resourceInfo)
        {
            CreateResourceButton(r);
        }
        if (settingsResourceButtons.Count != 0)
        {
            selectedResourceButton = settingsResourceButtons[0];
            resourceNameInputField.text = settingsResourceButtons[0].localResource.resourceName;
            startingValueInputField.text = settingsResourceButtons[0].localResource.resourceStartingAmount.ToString();
            minusButton.gameObject.SetActive(true);
        }
        else
        {
            minusButton.gameObject.SetActive(false);
        }
        if (settingsResourceButtons.Count == 5)
        {
            plusButton.gameObject.SetActive(true);
        }

        minusButton.onClick.RemoveAllListeners();
        plusButton.onClick.RemoveAllListeners();
        resourceNameInputField.onValueChanged.RemoveAllListeners();
        startingValueInputField.onValueChanged.RemoveAllListeners();

        minusButton.onClick.AddListener(MinusSignButton);
        plusButton.onClick.AddListener(PlusSignButton);
        resourceNameInputField.onValueChanged.AddListener(delegate { UpdateResourceButton(); });
        startingValueInputField.onValueChanged.AddListener(delegate { SecondUpdate(); });



    }

    void PlusSignButton()
    {
        if (settingsResourceButtons.Count < 5)
        {

            Resource temp = new Resource("Resource Name", 0);
            CreateResourceButton(temp);
            selectedResourceButton = settingsResourceButtons[settingsResourceButtons.Count - 1];
            currentList.Add(temp);

        }

        if (settingsResourceButtons.Count == 5)
        {
            plusButton.gameObject.SetActive(false);
        }
        if (settingsResourceButtons.Count > 0)
        {
            minusButton.gameObject.SetActive(true);
        }
    }

    void MinusSignButton()
    {

        if (selectedResourceButton != null && settingsResourceButtons.Count > 0)
        {
            //int index = settingsResourceButtons.IndexOf(selectedResourceButton);
            settingsResourceButtons.Remove(selectedResourceButton);
            currentList.Remove(selectedResourceButton.localResource);
            Destroy(selectedResourceButton.gameObject);
        }

        if (settingsResourceButtons.Count == 0)
        {
            minusButton.gameObject.SetActive(false);
        }

    }

    void UpdateResourceButton()
    {
        selectedResourceButton.localResource.resourceName = resourceNameInputField.text;
        selectedResourceButton.resourceNameText.text = resourceNameInputField.text;

    }

    void SecondUpdate()
    {
        selectedResourceButton.localResource.resourceStartingAmount = int.Parse(startingValueInputField.text);
        selectedResourceButton.resourceStartingAmountText.text = startingValueInputField.text;
    }

    void UpdateInputField()
    {
        //selectedResourceButton.localResource.resourceName = resourceNameInputField.text;
        //selectedResourceButton.localResource.resourceStartingAmount = int.Parse(startingValueInputField.text);
    }

    public void SavePreset()
    {
        currentPreset.currentButtonName = selectedButtonNameText.text;
        currentPreset.resource.resourceInfo = new List<Resource>();
        for (int i = 0; i < settingsResourceButtons.Count; i++)
        {
            currentPreset.resource.resourceInfo.Add(settingsResourceButtons[i].localResource);
        }
        menu.SerializePreset(currentPreset);
        menu.Show(currentPreset);
        gameObject.SetActive(false);
    }

    void CreateResourceButton(Resource resource)
    {
        if (settingsResourceButtons.Count < 5)
        {
            SettingsResourceButton temp = (SettingsResourceButton)Instantiate(resoureceButtonPrefab, resourceButtonContainer.transform);
            temp.Initialize(resource, SetSelectedResourceButton);
            settingsResourceButtons.Add(temp);
            temp.button.onClick.AddListener(() => { SetSelectedResourceButton(temp); });
        }
    }

    public void SetSelectedResourceButton(SettingsResourceButton local)
    {
        selectedResourceButton = local;
        startingValueInputField.text = local.resourceStartingAmountText.text;
        resourceNameInputField.text = local.resourceNameText.text;


    }

}
