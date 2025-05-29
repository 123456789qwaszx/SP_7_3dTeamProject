using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour, IDamageable
{
    public PlayerInteraction interaction;
    public EquipmentData _data;
    public ResourceCondition resourceCondition;


    private float workSpeed = 0.5f;
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
        _data.dropPrefab.transform.position = pc.transform.position + new Vector3(0, 2.2f);
        // 이 때 나오는 건, drop프리팹을 공유하는게 아니라, 별도로 dotTween을 활용한 애니메이션을 사용한 뒤 destroy 시키기
        SpawnResource(_data.dropPrefab);
        capacity -= 1;
        if (capacity <= 0)
        {
            DestroyResource();
        }
        // 생성하고 나무위치로 이동
    }


    public GameObject SpawnResource(GameObject prefab)
    {
        GameObject go = GameObject.Instantiate(prefab);
        go.name = prefab.name;
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
