using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CounterLogic : MonoBehaviour
{
    [SerializeField] ScriptableObjectUI buttonBolors;
    [SerializeField] TMP_Text playerName;
    [SerializeField] TMP_Text counterValue;
    [SerializeField] Button decreaseButton;
    [SerializeField] Button increaseButton;
    [SerializeField] TMP_InputField inputFieldFourPlayer;
    [SerializeField] TMP_InputField inputField;
    [SerializeField] TMP_InputField activeInputField;
    [SerializeField] LayoutElement layoutElementCounter;
    [SerializeField] public RectTransform rectTransform;
    [SerializeField] Image image;
    int currentValue;
    int increment;
    //[SerializeField] int incrementAmount;

    private void Awake()
    {

    }
    public void Initialize(SerializablePreset preset, int playerIndex, int layoutElementValue = 0)
    {
        //clear buttons for reset
        increaseButton.onClick.RemoveAllListeners();
        decreaseButton.onClick.RemoveAllListeners();
        inputField.onValueChanged.RemoveAllListeners();
        inputFieldFourPlayer.onValueChanged.RemoveAllListeners();

        currentValue = preset.lifeCounter.startingLifePoints;
        counterValue.text = currentValue.ToString();
        playerName.text = string.Format("Player {0}", playerIndex);
        if (playerIndex <= 5)
        {
            image.sprite = buttonBolors.buttonColors[playerIndex - 1].sprite;
        }
        else
        {
            image.sprite = buttonBolors.buttonColors[0].sprite;
        }
        increaseButton.onClick.AddListener(() => SetIncrementButtons(preset.lifeCounter.increment));
        decreaseButton.onClick.AddListener(() => SetIncrementButtons(-preset.lifeCounter.increment));
        inputField.onValueChanged.AddListener(delegate { InputFieldUpdate(); });
        inputFieldFourPlayer.onValueChanged.AddListener(delegate { InputFieldUpdate(); });
        if (preset.lifeCounter.players == 3)
        {
            inputField.gameObject.SetActive(false);
            inputFieldFourPlayer.gameObject.SetActive(true);
            activeInputField = inputFieldFourPlayer;
        }
        else
        {
            inputFieldFourPlayer.gameObject.SetActive(false);
            inputField.gameObject.SetActive(true);
            activeInputField = inputField;
        }
        if (layoutElementValue != 0)
        {
            layoutElementCounter.preferredHeight = layoutElementValue;
        }
        activeInputField.text = currentValue.ToString();
    }

    public void InputFieldUpdate()
    {
        currentValue = int.Parse(activeInputField.text);
    }
    public void SetIncrementButtons(int incrementAmount)
    {
        currentValue += incrementAmount;
        counterValue.text = currentValue.ToString();
        activeInputField.text = currentValue.ToString();
    }
}
