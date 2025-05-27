using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ResourceType
{
    Consumable,
    Resource
}

public enum ConsumableType
{
    Health,
    Hydration,
    Hunger
}

[Serializable]
public class ItemDataConsumable
{
    public ConsumableType type;
    public float value;
}

[CreateAssetMenu(fileName = "item_Resource", menuName = "New Resource")]
public class ResourceData : ScriptableObject
{
    [Header("Info")]
    public string ResourceName;
    public string description;
    public GameObject dropPrefab;

    [Header("stacking")]
    public bool canStack;
    public int maxStackAmout;

    [Header("Consumable")]
    public ItemDataConsumable[] consumables;
}
