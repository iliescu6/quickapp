using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public delegate void SetSelectedButton(SettingsResourceButton text);

public class SettingsResourceButton : MonoBehaviour
{
    [SerializeField] public Button button;
    [SerializeField] public TMP_Text resourceNameText;
    [SerializeField] public TMP_Text resourceStartingAmountText;
    public Resource localResource;
    UnityAction setSelectedResourceButton;
    public void Initialize(Resource resource, SetSelectedButton setSelected)
    {
        localResource = resource;
        button.onClick.AddListener(() => setSelected.Invoke(this));        
        resourceNameText.text = resource.resourceName.ToString();
        resourceStartingAmountText.text = resource.resourceStartingAmount.ToString();
    }


}
