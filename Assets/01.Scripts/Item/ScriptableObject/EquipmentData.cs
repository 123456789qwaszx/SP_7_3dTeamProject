using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EquipmenType
{
    Weapon,
    Tool
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
    public EquipmenType type;
    public GameObject dropPrefab;

    [Header("Equip")]
    public GameObject equipPrefab;

    [Header("EquipmentStatus")]
    public WeaponDamage[] weaponDamages;

}

