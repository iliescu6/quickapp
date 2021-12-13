using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPresetSettings : AbstractPresetSettings
{
    [SerializeField] InventoryMenu menu;

    public void Initialize(SerializableLifeCounterPreset selectedPreset)
    {
        base.Initialize(selectedPreset, menu, SavePreset);
    }

    public void SavePreset()
    {
        //currentPreset.currentButtonName = selectedButtonNameText.text;
        //currentPreset.buttonSettings = selectedButtonSettingsText.text;
        //currentPreset.startingLifePoints = int.Parse(startingLifeInputField.text);
        //currentPreset.increment = int.Parse(incrementInputField.text);
        //currentPreset.players = playerDropDown.value;
        //currentPreset.matchType = matchType.value;
        menu.SerializePreset(currentPreset);
        menu.Show(currentPreset);
        gameObject.SetActive(false);
    }

}
