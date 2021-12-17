using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryScreen : MonoBehaviour, ScreenInterface
{
    [SerializeField] GameObject conainter;
    [SerializeField] TMP_Text PresetNameText;
    [SerializeField] ResourceButtonScreen prefab;
    List<ResourceButtonScreen> resources = new List<ResourceButtonScreen>();
    public void Initialize(SerializablePreset preset)
    {
        ScreensController.Instance.currentScreen = this;
        ScreensController.Instance.ShowOptionsButton();
        PresetNameText.text = preset.currentButtonName;
        for (int i = 0; i < preset.resource.resourceInfo.Count; i++)
        {
            ResourceButtonScreen temp = Instantiate(prefab, conainter.transform);
            temp.Initialize(preset.resource.resourceInfo[i]);
            resources.Add(temp);
        }
    }

    public void Restart()
    {
        foreach (ResourceButtonScreen button in resources)
        {
            button.RestartButton();
        }
    }

    public void DeleteCurrentMatchSession()
    {
        for (int i = resources.Count - 1; i >= 0; i--)
        {
            Destroy(resources[i].gameObject);
        }
        resources = new List<ResourceButtonScreen>();
        gameObject.SetActive(false);
    }
}
