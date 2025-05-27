using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    #region Log
    public GameObject LogPrefab;

    public Transform _logRoot;
    public Transform LogRoot
    {
        get
        {
            if (_logRoot == null)
            {
                GameObject go = new GameObject("@LogRoot");
                _logRoot = go.transform;
            }
            return _logRoot;
        }
    }

    public GameObject SpawnLog()
    {
        Debug.Log("나무생성");
        GameObject go = GameObject.Instantiate(LogPrefab, _logRoot);
        go.name = LogPrefab.name;
        go.transform.parent = LogRoot;
        return go;
    }
    #endregion
}