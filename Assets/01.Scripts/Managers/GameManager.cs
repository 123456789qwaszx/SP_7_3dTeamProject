using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    #region Log
    public GameObject LogPrefab;

    public GameObject SpawnLog()
    {
        Debug.Log("나무생성");
        GameObject go = GameObject.Instantiate(LogPrefab);
        go.name = LogPrefab.name;
        return go;
    }
    #endregion
}