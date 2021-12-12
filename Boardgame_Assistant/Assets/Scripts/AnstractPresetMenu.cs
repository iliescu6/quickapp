using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnstractPresetMenu : MonoBehaviour
{
    [SerializeField] ScriptableObjectUI colorButtonMap;
    [SerializeField] Button addPreset;
    [SerializeField] Button backButton;
    [SerializeField] Lifecounterbuttonprefab buttonPrefab;
    [SerializeField] GameObject container;
    [SerializeField] LifeCounterPresetSettings settingsMenu;
    [SerializeField] PresetList presetList;
    [SerializeField] LifeCounterScreen screen;
    List<Lifecounterbuttonprefab> prefabButtons = new List<Lifecounterbuttonprefab>();
    SaveLoadPresets data = new SaveLoadPresets();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
