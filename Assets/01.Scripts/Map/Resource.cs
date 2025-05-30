using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour, IDamageable
{
    public PlayerInteraction interaction;
    public EquipmentData _data;
    public ResourceCondition resourceCondition;
    public SpawningPool spawningPool;

    private float workSpeed = 1f;

    public int capacity = 1;
    public float hitCount;


    void Start()
    {
        spawningPool = GetComponentInParent<SpawningPool>();
        interaction.InteractInterval = workSpeed;
        interaction.OnPlayerInteraction = OnPlayerResourceInteraction;

        switch (_data.resourcetype)
            {
                case ResourceType.Tree:
                    capacity = 3;
                    break;
                case ResourceType.Rock:
                    capacity = 10;
                    break;
                case ResourceType.Iron:
                    capacity = 7;
                    break;
                case ResourceType.Mushroom:
                    capacity = 1;
                    break;
                default:
                    break;
            }
    }
    

    void OnPlayerResourceInteraction(Player pc)
    {
        if (capacity > 0)
        {
            _data.dropPrefab.transform.position = pc.transform.position + new Vector3(0, 2.2f);
            // 이 때 나오는 건, drop프리팹을 공유하는게 아니라, 별도로 dotTween을 활용한 애니메이션을 사용한 뒤 destroy 시키기
            SpawnResource(_data.dropPrefab);
            capacity -= 1;
            if (capacity == 0)
            {
                ResourceDestory();
            }
        }
        else
        {
            ResourceDestory();
        }
    }


    public GameObject SpawnResource(GameObject prefab)
    {
        GameObject go = GameObject.Instantiate(prefab);
        go.name = prefab.name;
        return go;
    }


    void ResourceDestory()
    {
        Destroy(gameObject);

        switch (_data.resourcetype)
            {
                case ResourceType.Tree:
                    spawningPool._treeCount -= 1;
                    break;
                case ResourceType.Rock:
                    spawningPool._rockCount -= 1;
                    break;
                case ResourceType.Iron:
                    spawningPool._ironCount -= 1;
                    break;
                case ResourceType.Mushroom:
                    spawningPool._mushroomCount -= 1;
                    break;
                default:
                    break;
            }
    }


    public void TakeDamage(float damage)
    {
        hitCount -= damage;
    }
}
