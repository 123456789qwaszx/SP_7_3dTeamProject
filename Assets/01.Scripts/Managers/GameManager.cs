using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public GameObject SpawnResource(GameObject prefab, Transform resourceRoot)
    {
        GameObject go = GameObject.Instantiate(prefab, resourceRoot);
        go.name = prefab.name;
        go.transform.parent = resourceRoot;
        return go;
    }
}