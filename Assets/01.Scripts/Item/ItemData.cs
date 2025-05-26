using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ItemType
{
    Eat,
    Drink,
    Equip,
    Resource
}

public class ItemData : ScriptableObject
{
    [Header("Info")]
    public string displayName;
    public string description;
    public ItemType type;
}
