using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EquipmenType
{
    Weapon,
    Tool
}

[Serializable]
public class WeaponDamage
{
    public EquipmenType equipmenType;
    public float DamageToMonster;
    public float DamageToResource;
}


[CreateAssetMenu(fileName = "Equipment", menuName = "New Equipment")]
public class EquipmentData : ScriptableObject
{
    [Header("Info")]
    public string displayName;
    public string description;
    public EquipmenType type;
    public GameObject dropPrefab;

    [Header("Equip")]
    public GameObject equipPrefab;

    [Header("EquipmentStatus")]
    public WeaponDamage[] equipmentStatus;

}
