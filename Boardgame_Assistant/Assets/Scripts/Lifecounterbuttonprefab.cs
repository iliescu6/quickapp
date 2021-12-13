using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Lifecounterbuttonprefab : MonoBehaviour, IPointerDownHandler, IPointerUpHandler,IDragHandler 
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
    ShowSceneAction openScreen;

    bool holdingButton;
    bool pointerInrect = true;
    float holdTimer;

    public SerializableLifeCounterPreset localPreset;

    public void SetUpButton(SerializableLifeCounterPreset prest, Sprite sprite, ShowSceneAction action)
    {
        buttonName.text = prest.currentButtonName;
        buttonSettingsText.text = prest.buttonSettings;
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
        pointerInrect = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        pointerInrect = false;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        holdingButton = false;
        if (holdTimer > requiredHoldTime && pointerInrect)
        {
            buttonsContainer.SetActive(true);
            nameContainer.SetActive(false);
        }
        else if (pointerInrect)
        {
            openScreen.Invoke(localPreset);
        }
        holdTimer = 0;
    }
}
