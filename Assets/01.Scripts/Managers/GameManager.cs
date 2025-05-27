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
        Debug.Log("wood_resource");
        GameObject go = GameObject.Instantiate(LogPrefab, _logRoot);
        go.name = LogPrefab.name;
        go.transform.parent = LogRoot;
        return go;
    }
    #endregion

    #region Rock
    public GameObject RockPrefab;

    public Transform _rockRoot;
    public Transform RockRoot
    {
        get
        {
            if (_rockRoot == null)
            {
                GameObject go = new GameObject("@RockRoot");
                _rockRoot = go.transform;
            }
            return _rockRoot;
        }
    }

    public GameObject SpawnRock()
    {
        Debug.Log("rock_resource");
        GameObject go = GameObject.Instantiate(RockPrefab, _rockRoot);
        go.name = RockPrefab.name;
        go.transform.parent = RockRoot;
        return go;
    }
    #endregion
}