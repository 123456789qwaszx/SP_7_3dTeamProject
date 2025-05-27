using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Monster Data", menuName = "Monster Data")]
public class MonsterData : ScriptableObject
{
    [Header("Basic Info")]
    public string monsterName;
    public float health;
    public float damage;
    public float attackRate;
    public float detectDistance;
    public float attackDistance;

    [Header("Movement")] public float walkSpeed;
    public float runSpeed;
    
    [Header("Rewards")]
    public DropItem[] dropItems;

}
[System.Serializable]
public class DropItem
{
    public GameObject itemPrefab;
    [Range(0f,1f)]
    public float dropRate;
    public int minQuantity = 1;
    public int maxQuantity = 1;
}
