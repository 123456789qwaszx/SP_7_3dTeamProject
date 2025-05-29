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
    public ConsumableType type;
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
    public ResourceType type;
    public int value;
}


public enum EquipmentType
{
    Weapon,
    Resource,
    Food
}

public enum DamageType
{
    ForMonster,
    ForResource
}

[Serializable]
public class WeaponDamage
{
    public DamageType type;
    public float value;
}


[CreateAssetMenu(fileName = "Equipment", menuName = "New Equipment")]
public class EquipmentData : ScriptableObject
{
    [Header("Info")]
    public string displayName;
    public string description;
    public Sprite icon;
    public EquipmentType type;
    public ResourceType resourcetype;
    public GameObject dropPrefab;

    public int quantity;

    [Header("Equip")]
    public GameObject equipPrefab;

    [Header("EquipmentStatus")]
    public WeaponDamage[] weaponDamages;


    [Header("stacking")]
    public bool canStack;
    public int maxStackAmount;

    [Header("Consumable")]
    public ItemDataConsumable[] consumables;

    [Header("Resource")]
    public ItemDataResourceHp[] resources;

}

