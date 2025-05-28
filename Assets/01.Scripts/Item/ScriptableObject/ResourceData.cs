using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ConsumableType
{
    Health,
    Hydration,
    Hunger
}

[Serializable]
public class ItemDataConsumable
{
    public ConsumableType consumabletype;
    public float value;
}

public enum ResourceType
{
    Tree,
    Rock,
    Iron,
    Mushroom
}

[Serializable]
public class ItemDataResourceHp
{
    public ResourceType Resourcetype;
    public float value;
}

[CreateAssetMenu(fileName = "Resource", menuName = "New Resource")]
public class ResourceData : ScriptableObject
{
    [Header("Info")]
    public string ResourceName;
    public string description;
    public Sprite icon;
    public GameObject dropPrefab;

    [Header("stacking")]
    public bool canStack;
    public int maxStackAmout;

    [Header("Consumable")]
    public ItemDataConsumable[] consumables;

    [Header("Resorce")]
    public ItemDataResourceHp[] resourceHp;
}
