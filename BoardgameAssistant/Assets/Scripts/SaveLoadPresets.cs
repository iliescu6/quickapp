using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveLoadPresets
{

    public void CheckIfExists<T>(string path, ref List<T> presets)
    {
        if (File.Exists(path))
        {
            string deserializedString = File.ReadAllText(path);
            presets = JsonUtility.FromJson<List<T>>(deserializedString);
        }
        else
        {
            string serializedList = JsonUtility.ToJson(presets);
            File.WriteAllText(path, serializedList);
        }
    }

    public void SaveData(string path, ref PresetList presets)
    {
        if (File.Exists(path))
        {
            string serializedPresets = JsonUtility.ToJson(presets);
            File.WriteAllText(path, serializedPresets);
        }
        else
        {
            string serializedList = JsonUtility.ToJson(presets);
            File.WriteAllText(path, serializedList);
        }
    }

    public void LoadData(string path, ref PresetList presets)
    {
        if (File.Exists(path))
        {
            string loadedJsonDataString = File.ReadAllText(path);
            presets = JsonUtility.FromJson<PresetList>(loadedJsonDataString);
        }
        else
        {
            string serializedList = JsonUtility.ToJson(presets);
            File.WriteAllText(path, serializedList);
        }
    }
}
