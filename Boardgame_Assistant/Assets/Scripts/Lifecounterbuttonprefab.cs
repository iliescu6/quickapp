using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Lifecounterbuttonprefab : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
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

    public SerializablePreset localPreset;

    public void SetUpButton(Sprite sprite, ShowSceneAction action, SerializablePreset preset, DeletePresetAction deleteIt)
    {
        buttonName.text = preset.currentButtonName;
        localPreset = preset;
        if (preset.lifeCounter != null)
        {
            buttonSettingsText.text = preset.lifeCounter.buttonSettings;
        }
        else if (preset.resource != null)
        {
            buttonSettingsText.gameObject.SetActive(false);
        }

        buttonImage.sprite = sprite;
        openScreen = action;
        returnButton.onClick.AddListener(ReturnButton);
        deleteButton.onClick.AddListener(delegate { 
            deleteIt.Invoke(localPreset, true);
            Destroy(gameObject);
        });
    }

    public void ReturnButton()
    {
        buttonsContainer.SetActive(false);
        nameContainer.SetActive(true);
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
