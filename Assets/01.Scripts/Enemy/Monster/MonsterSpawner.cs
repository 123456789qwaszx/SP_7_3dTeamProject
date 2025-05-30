using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

[System.Serializable]

public class SpawnData
{
    public GameObject monsterPrefab;
    public MonsterData monsterData;
    public int maxCount;
    public float spawnRate;
    public float spawnRadius;
}
public class MonsterSpawner : MonoBehaviour
{
    [Header("스폰 설정")] public SpawnData[] spawnDatas;
    public Transform[] spawnPoints;
    public float playerCheckRadius = 20f;
    
    [Header("풀 설정")] public int initialPoolSize = 10;
    
    private Transform player;
    private Dictionary<string, int> currentMonsterCounts = new Dictionary<string, int>();
    private Dictionary<string, float> lastSpawnTimes = new Dictionary<string, float>();

    private Dictionary<string, List<MonsterBase>> activeMonsters = new Dictionary<string, List<MonsterBase>>();

    public SFXManager sfxManager;
    void Start()
    {
        InitializeSpawnManager();
    }
    

    void Update()
    {
        CheckAndSpawnMonsters();
    }
    
    private void InitializeSpawnManager()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            player = playerObj.transform;

        foreach (var spawnData in spawnDatas)
        {
            string monsterName = spawnData.monsterPrefab.name;
            
            Managers.Pool.CreatePool(spawnData.monsterPrefab, initialPoolSize);

            currentMonsterCounts[monsterName] = 0;
            lastSpawnTimes[monsterName] = 0;
            activeMonsters[monsterName] = new List<MonsterBase>();
        }
    }

    private void CheckAndSpawnMonsters()
    {
        if (player == null) return;

        foreach (var spawnData in spawnDatas)
        {
            string monsterName = spawnData.monsterPrefab.name;
            
            //현재 몬스터 수 체크
            CleanupInactiveMonsters(monsterName);
            
            if (currentMonsterCounts[monsterName] >= spawnData.maxCount)
                continue;

            if (Time.time - lastSpawnTimes[monsterName] < spawnData.spawnRate)
                continue;

            SpawnMonster(spawnData);
            lastSpawnTimes[monsterName] = Time.time;
        }
    }

    private void SpawnMonster(SpawnData spawnData)
    {
        Debug.Log($" {spawnData.monsterPrefab.name}의 monsterData: {spawnData.monsterData}");
        Vector3 spawnPosition = GetValidSpawnPosition(spawnData.spawnRadius);
        if (spawnPosition == Vector3.zero)
        {
            Debug.Log($"{spawnData.monsterPrefab.name}의 위치 찾기 실패");
            return;
        }

        Poolable poolable = Managers.Pool.Pop(spawnData.monsterPrefab);
        if (poolable == null) return;

        GameObject monsterObj = poolable.gameObject;
        MonsterBase monster = monsterObj.GetComponent<MonsterBase>();

        monsterObj.transform.position = spawnPosition;
        monsterObj.transform.rotation = Quaternion.identity;

        monster.monsterData = spawnData.monsterData;

        monster.SetMonsterData(spawnData.monsterData);
        
        monster.OnSpawnFromPool();
        
        string monsterName = spawnData.monsterPrefab.name;
        activeMonsters[monsterName].Add(monster);
        currentMonsterCounts[monsterName]++;

        monster.onTakeDamage += () => OnMonsterTakeDamage(monster);
        
        Debug.Log($"{monsterName} 스폰 됨. 현재 수 : {currentMonsterCounts[monsterName]}");
    }

    private Vector3 GetValidSpawnPosition(float spawnRadius)
    {
        int attempts = 30;

        for (int i = 0; i < attempts; i++)
        {
            Vector3 randomPosition;

            if (spawnPoints.Length > 0)
            {
                Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
                Vector2 randomCircle = Random.insideUnitCircle * spawnRadius;
                randomPosition = randomSpawnPoint.position + new Vector3(randomCircle.x, 0, randomCircle.y);
            }
            else
            {
                //스폰포인트 없으면 플레이어 주변에서 스폰
                Vector2 randomCircle = Random.insideUnitCircle * spawnRadius;
                randomPosition = player.position + new Vector3(randomCircle.x, 0, randomCircle.y);
            }
            
            //플레이어와 너무 가까우면 스킵
            if(Vector3.Distance(randomPosition,player.position) < playerCheckRadius) continue;

            
            // NavMesh 위의 유효한 위치인지 체크
            UnityEngine.AI.NavMeshHit hit;
            if (UnityEngine.AI.NavMesh.SamplePosition(randomPosition, out hit, 5f, UnityEngine.AI.NavMesh.AllAreas))
            {
                return hit.position;
            }
        }

        return Vector3.zero;
    }

    private void CleanupInactiveMonsters(string monsterName)
    {
        activeMonsters[monsterName].RemoveAll(monster => monster == null || !monster.gameObject.activeInHierarchy);
        currentMonsterCounts[monsterName] = activeMonsters[monsterName].Count;
    }
    private void OnMonsterTakeDamage(MonsterBase monster)
    {
        sfxManager.PlaySFX(sfxManager.bearHitSFX, monster.transform.position);
    }

    public void SpawnMonsterImmediate(string monsterName, Vector3 position)
    {
        SpawnData targetSpawnData = null;
        foreach (var spawnData in spawnDatas)
        {
            if (spawnData.monsterPrefab.name == monsterName)
            {
                targetSpawnData = spawnData;
                break;
            }
        }

        if (targetSpawnData == null)
        {
            Debug.Log($"{monsterName}을 찾을 수 없습니다.");
            return;
        }

        Poolable poolable = Managers.Pool.Pop(targetSpawnData.monsterPrefab);
        if (poolable == null) return;

        GameObject monsterObj = poolable.gameObject;
        MonsterBase monster = monsterObj.GetComponent<MonsterBase>();
        
        monsterObj.transform.position = position;
        monsterObj.transform.rotation = Quaternion.identity;

        monster.SetMonsterData(targetSpawnData.monsterData);
        monster.OnSpawnFromPool();
        
        activeMonsters[monsterName].Add(monster);
        currentMonsterCounts[monsterName]++;

        monster.onTakeDamage += () => OnMonsterTakeDamage(monster);
    }

    public void ClearAllMonsters()
    {
        foreach (var monsterList in activeMonsters.Values)
        {
            foreach (var monster in monsterList)
            {
                if (monster != null && monster.gameObject.activeInHierarchy)
                {
                    Poolable poolable = monster.GetComponent<Poolable>();
                    if (poolable != null)
                    {
                        Managers.Pool.Push(poolable);
                    }
                }
            }
        }
        foreach (var key in activeMonsters.Keys)
        {
            activeMonsters[key].Clear();
            currentMonsterCounts[key] = 0;
        }
    }
}
