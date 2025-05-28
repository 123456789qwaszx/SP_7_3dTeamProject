using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    void TakePhysicalDamage(int damage);
}

public class Resource : MonoBehaviour, IDamagable
{
    public PlayerInteraction interaction;
    public EquipmentData _data;
    public ResourceCondition resourceCondition;

    private Transform _resourceRoot;
    public Transform ResourceRoot
    {
        get
        {
            if (_resourceRoot == null)
            {
                GameObject go = new GameObject("@ResourceRoot");
                _resourceRoot = go.transform;
            }
            return _resourceRoot;
        }
    }

    private float workSpeed = 2;
    public int capacity = 3;

    public int hitCount = 1;


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

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("HardObject"))
        {
            Managers.Resource.Destroy(gameObject);
        }
    }


    void OnPlayerResourceInteraction(Player pc)
    {
        // 나중에 Ax를 들었을 때만 동작.
        // 혹은 특정 키값입력 or 능력 등등
        SpawnResource(_data.dropPrefab, ResourceRoot);
        _data.dropPrefab.transform.position = pc.transform.position + new Vector3(0, 2);
        capacity -= 1;
        if (capacity <= 0)
        {
            DestroyResource();
        }
        // 생성하고 나무위치로 이동
    }


    public GameObject SpawnResource(GameObject prefab, Transform resourceRoot)
    {
        GameObject go = GameObject.Instantiate(prefab, resourceRoot);
        go.name = prefab.name;
        go.transform.parent = resourceRoot;
        return go;
    }

    public void TakePhysicalDamage(int damage)
    {
        hitCount -= damage;
    }


    public void DestroyResource()
    {
        Destroy(gameObject);
    }
}
