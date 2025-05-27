using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    public PlayerInteraction interaction;
    public ResourceData _data;

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


    void Start()
    {
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
        _data.dropPrefab.transform.position = pc.transform.position + new Vector3(0,2);
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


    public void DestroyResource()
    {
        Destroy(gameObject);
    }
}
