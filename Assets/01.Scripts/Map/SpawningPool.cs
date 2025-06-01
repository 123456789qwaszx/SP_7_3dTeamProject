using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningPool : MonoBehaviour
{
    public Transform _resourceRoot;

    [SerializeField]
    private Resource _tree;
    [SerializeField]
    private Resource _rock;
    [SerializeField]
    private Resource _iron;
    [SerializeField]
    private Resource _mushroom;

    [Header("Tree")]
    [SerializeField]
    public int _treeCount = 0;
    [SerializeField]
    int _keepTreeCount = 15;
    [SerializeField]
    float _treeSpawnTime = 3f;
    [SerializeField]
    float _treeSpawnLimit = 10f;

    [Header("Rock")]
    [SerializeField]
    public int _rockCount = 0;
    [SerializeField]
    int _keepRockCount = 6;
    [SerializeField]
    float _rockSpawnTime = 5f;
    [SerializeField]
    float _rockSpawnLimit = 20f;

    [Header("Iron")]
    [SerializeField]
    public int _ironCount = 0;
    [SerializeField]
    int _keepIronCount = 3;
    [SerializeField]
    float _ironSpawnTime = 1f;
    [SerializeField]
    float _ironSpawnLimit = 40f;

    [Header("Mushroom")]
    [SerializeField]
    public int _mushroomCount = 0;
    [SerializeField]
    int _keepMushroomCount = 20;
    [SerializeField]
    float _mushroomSpawnTime = 1f;
    [SerializeField]
    float _mushroomSpawnLimit = 5f;

    [Header("")]
    [SerializeField]
    float _spawnRadius = 140f;

    int _reserveCount = 0;
    Vector3 _spawnPos;

    void Update()
    {
        while (_reserveCount + _treeCount < _keepTreeCount)
        {
            StartCoroutine(CoOBJresourceSpawn(_tree, _treeSpawnTime, _treeSpawnLimit));
        }
        while (_reserveCount + _rockCount < _keepRockCount)
        {
            StartCoroutine(CoOBJresourceSpawn(_rock, _rockSpawnTime, _rockSpawnLimit));
        }
        while (_reserveCount + _ironCount < _keepIronCount)
        {
            StartCoroutine(CoOBJresourceSpawn(_iron, _ironSpawnTime, _ironSpawnLimit));
        }
        while (_reserveCount + _mushroomCount < _keepMushroomCount)
        {
            StartCoroutine(CoOBJresourceSpawn(_mushroom, _mushroomSpawnTime, _mushroomSpawnLimit));
        }
    }

    
    IEnumerator CoOBJresourceSpawn(Resource prefab, float spawnTime, float spawnLimit)
    {
        _reserveCount++;

        yield return new WaitForSeconds(spawnTime);
        Resource go = Instantiate(prefab);

        switch (go._data.resourcetype)
            {
                case ResourceType.Tree:
                    go.hitCount = 3;
                    _treeCount++;
                    break;
                case ResourceType.Rock:
                    go.hitCount = 5;
                    _rockCount++;
                    break;
                case ResourceType.Iron:
                    go.hitCount = 10;
                     _ironCount++;
                    break;
                case ResourceType.Mushroom:
                    go.hitCount = 1;
                    _mushroomCount++;
                    break;
            }
        go.name = prefab.name;
        go.transform.parent = _resourceRoot;

        Vector3 randPos;
        Vector3 randDir = Random.insideUnitSphere * Random.Range(spawnLimit, _spawnRadius);
        randDir.y = 0;
        randPos = _spawnPos + randDir;
        // randPos가 갈 수 있는지 없는지 체크 언덕 or 이미 생성된 나무 or 건물 등

        go.transform.position = randPos;
        _reserveCount--;
    }
}
