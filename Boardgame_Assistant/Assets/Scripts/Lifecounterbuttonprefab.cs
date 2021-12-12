using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Lifecounterbuttonprefab : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] public TMP_Text buttonName;
    [SerializeField] TMP_Text buttonSettingsText;
    [SerializeField] GameObject nameContainer;
    [SerializeField] GameObject buttonsContainer;
    [SerializeField] Button presetButton;
    [SerializeField] public Button editButton;
    [SerializeField] Button deleteButton;
    [SerializeField] Button returnButton;
    [SerializeField] Image buttonImage;
    [SerializeField] float requiredHoldTime;
    UnityAction openScreen;

    bool holdingButton;
    float holdTimer;

    public SerializableLifeCounterPreset localPreset;

    public void SetUpButton(string name, string settings, SerializableLifeCounterPreset prest, Sprite sprite,UnityAction action)
    {
        buttonName.text = name;
        buttonSettingsText.text = settings;
        localPreset = prest;
        buttonImage.sprite = sprite;
        openScreen = action;
        returnButton.onClick.AddListener(ReturnButton);
        deleteButton.onClick.AddListener(DeleteButton);
    }

    public void ReturnButton()
    {
        buttonsContainer.SetActive(false);
        nameContainer.SetActive(true);
    }

    public void DeleteButton()
    {
        Destroy(gameObject);
    }

    void Update()
    {
        if (holdingButton)
        {
            holdTimer += Time.deltaTime;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        holdingButton = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        holdingButton = false;
        if (holdTimer > requiredHoldTime)
        {
            buttonsContainer.SetActive(true);
            nameContainer.SetActive(false);
        }
        else
        {
            openScreen.Invoke();
        }
        holdTimer = 0;
    }
}
