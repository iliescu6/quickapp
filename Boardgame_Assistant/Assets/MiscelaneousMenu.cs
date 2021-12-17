using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MiscelaneousMenu : MonoBehaviour
{
    [SerializeField] Button flipButton;
    [SerializeField] Button rollDiceButton;
    [SerializeField] Button backButton;
    [SerializeField] TMP_Text flipCoinText;
    [SerializeField] TMP_Text diceRollText;
    [SerializeField] TMP_Dropdown dropdown;
    int maxDiceValue = 5;//Add +1, range si excluding last
    private void Awake()
    {
        List<string> m_DropOptions = new List<string> { "4 Sided Dice", "6 Sided Dice", "8 Sided Dice", "10 Sided Dice", "20 Sided Dice" };
        dropdown.ClearOptions();
        dropdown.AddOptions(m_DropOptions);
        dropdown.onValueChanged.AddListener(delegate { UpdateMaxDiCeValue(); });
        flipButton.onClick.AddListener(FlipCoin);
        rollDiceButton.onClick.AddListener(RollDice);
        backButton.onClick.AddListener(BackButton);
    }

    void FlipCoin()
    {
        int result = Random.Range(0, 2);
        if (result == 0)
        {
            flipCoinText.text = "Heads";
        }
        else
        {
            flipCoinText.text = "Tails";
        }
    }

    void RollDice()
    {
        int diceResult = Random.Range(1, maxDiceValue);
        diceRollText.text = diceResult.ToString();
    }

    void UpdateMaxDiCeValue()
    {
        switch (dropdown.value)
        {
            case 0:
                maxDiceValue = 5;
                break;
            case 1:
                maxDiceValue = 7;
                break;
            case 2:
                maxDiceValue = 9;
                break;
            case 3:
                maxDiceValue = 11;
                break;
            case 4:
                maxDiceValue = 21;
                break;
        }
    }

    void BackButton()
    {
        ScreensController.Instance.screensMenuPopUp.SetActive(true);
        gameObject.SetActive(false);        
    }
}
