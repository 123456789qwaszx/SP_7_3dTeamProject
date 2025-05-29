using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour, IDamageable
{
    public PlayerInteraction interaction;
    public EquipmentData _data;
    public ResourceCondition resourceCondition;

    public Transform _dropPosition;


    private float workSpeed = 2;
    public int capacity = 3;

    public float hitCount = 1;


    void Start()
    {
        for (int i = 0; i < _data.resourceHp.Length; i++)
        {
            switch (_data.resourceHp[i].type)
            {
                case ResourceType.Tree:
                    this.hitCount = _data.resourceHp[i].value;
                    break;
                case ResourceType.Rock:
                    this.hitCount = _data.resourceHp[i].value;
                    break;
                case ResourceType.Iron:
                    this.hitCount = _data.resourceHp[i].value;
                    break;
                case ResourceType.Mushroom:
                    this.hitCount = _data.resourceHp[i].value;
                    break;
            }
        }

        interaction.InteractInterval = workSpeed;
        interaction.OnPlayerInteraction = OnPlayerResourceInteraction;
    }

    void OnPlayerResourceInteraction(Player pc)
    {
        // 나중에 Ax를 들었을 때만 동작.
        // 혹은 특정 키값입력 or 능력 등등
        SpawnResource(_data);
        capacity -= 1;
        if (capacity <= 0)
        {
            DestroyResource();
        }
        // 생성하고 나무위치로 이동
    }


    GameObject SpawnResource(EquipmentData data)
    {
        GameObject go = Instantiate(data.dropPrefab, _dropPosition.position, Quaternion.Euler(Vector3.one));
        go.name = data.name;
        return go;
    }

    public void TakeDamage(float damage)
    {
        hitCount -= damage;
    }


    public void DestroyResource()
    {
        Destroy(gameObject);
    }
}
