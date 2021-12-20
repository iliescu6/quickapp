using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourceButtonScreen : MonoBehaviour
{
    [SerializeField] TMP_Text resourceName;
    [SerializeField] Button minusSign;
    [SerializeField] Button plusSign;
    [SerializeField] TMP_InputField inputField;
    int currentValue;
    public Resource localResource;

    public void Initialize(Resource resource)
    {
        localResource = resource;
        currentValue = resource.resourceStartingAmount;
        resourceName.text = resource.resourceName;
        inputField.text = resource.resourceStartingAmount.ToString();
    }

    public void RestartButton()
    {
        currentValue = localResource.resourceStartingAmount;
        resourceName.text = localResource.resourceName;
        inputField.text = localResource.resourceStartingAmount.ToString();
    }

    public void MinusSign()
    {
        currentValue -= 1;
        inputField.text = currentValue.ToString();
    }

    public void PlusSign()
    {
        currentValue += 1;
        inputField.text = currentValue.ToString();
    }
}
