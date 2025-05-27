using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ItemType
{
    Consumable,
    Equipable,
    Resource
}

public enum ConsumableType
{
    Health,
    Hunger
}

[SerializeField]
public class ItemDataConsumable
{
    public ConsumableType type;
    public float value;
}



[CreateAssetMenu(fileName = "Item", menuName = "New Item")]
public class ItemData : ScriptableObject
{
    [Header("Info")]
    public string displayName;
    public string description;
    public ItemType type;

}
