using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningPool : MonoBehaviour
{
    [SerializeField]
    int _treeCount = 0;
    [SerializeField]
    int _keepTreeCount = 5;

    [SerializeField]
    float _spawnRadius = 150f;
    [SerializeField]
    float _spawnTime = 5f;

    int _reserveCount = 0;
    Vector3 _spawnPos;

    void Update()
    {
        while (_reserveCount + _treeCount < _keepTreeCount)
        {
            StartCoroutine("CoTreeSpawn");
        }
    }

    IEnumerator CoTreeSpawn()
    {
        Debug.Log("Tree");
        _reserveCount++;

        yield return new WaitForSeconds(_spawnTime);
        GameObject go = Managers.Resource.Instantiate("Tree");
        _treeCount++;

        Vector3 randPos;
        Vector3 randDir = Random.insideUnitSphere * Random.Range(0, _spawnRadius);
        randDir.y = 0;
        randPos = _spawnPos + randDir;
        // randPos가 갈 수 있는지 없는지 체크 언덕 or 이미 생성된 나무 or 건물 등

        go.transform.position = randPos;
        _reserveCount--;
    }


    public void AddTreeCount(int value)
    {
        _treeCount += value;
    }

}
