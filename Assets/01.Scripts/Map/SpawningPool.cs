using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningPool : MonoBehaviour
{
    public Transform _resourceRoot;

    [SerializeField]
    private GameObject _tree;
    [SerializeField]
    private GameObject _rock;
    [SerializeField]
    private GameObject _iron;
    [SerializeField]
    private GameObject _mushroom;

    [SerializeField]
    int _treeCount = 0;
    [SerializeField]
    int _rockCount = 0;
    [SerializeField]
    int _ironCount = 0;
    [SerializeField]
    int _mushroomCount = 0;

    [SerializeField]
    int _keepTreeCount = 15;
    [SerializeField]
    int _KeepRockCount = 6;
    [SerializeField]
    int _keepIronCount = 3;
    [SerializeField]
    int _keepMushroomCount = 20;


    [SerializeField]
    float _spawnRadius = 150f;
    [SerializeField]
    float _spawnTime = 5f;
    [SerializeField]
    int _reserveCount = 0;
    Vector3 _spawnPos;

    void Update()
    {
        while (_reserveCount + _treeCount < _keepTreeCount)
        {
            StartCoroutine(CoOBJresourceSpawn(_tree));
            _treeCount++;

        }
        while (_reserveCount + _rockCount < _KeepRockCount)
        {
            StartCoroutine(CoOBJresourceSpawn(_rock));
            _rockCount++;
        }
        while (_reserveCount + _ironCount < _keepIronCount)
        {
            StartCoroutine(CoOBJresourceSpawn(_iron));
            _ironCount++;
        }
        while (_reserveCount + _mushroomCount < _keepMushroomCount)
        {
            StartCoroutine(CoOBJresourceSpawn(_mushroom));
            _mushroomCount++;
        }
    }

    
    IEnumerator CoOBJresourceSpawn(GameObject prefab)
    {
        _reserveCount++;

        yield return new WaitForSeconds(_spawnTime);
        GameObject go = Instantiate(prefab);
        go.name = prefab.name;
        go.transform.parent = _resourceRoot;

        Vector3 randPos;
        Vector3 randDir = Random.insideUnitSphere * Random.Range(10, _spawnRadius);
        randDir.y = 0;
        randPos = _spawnPos + randDir;
        // randPos가 갈 수 있는지 없는지 체크 언덕 or 이미 생성된 나무 or 건물 등

        go.transform.position = randPos;
        _reserveCount--;
    }
}
