using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UIItemsObject", menuName = "ScriptableObjects/UIItemsObject", order = 1)]
public class ScriptableObjectUI : ScriptableObject
{
    public List<ButtonColor> buttonColors = new List<ButtonColor>();
}

[Serializable]
public class ButtonColor
{
    public string color;
    public Sprite sprite;
}
