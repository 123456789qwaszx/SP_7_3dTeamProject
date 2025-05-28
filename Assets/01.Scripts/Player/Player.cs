using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerController Controller;
    public PlayerStats PlayerStats;

    void Awake()
    {
        Managers.Player.Player = this;
        Controller = GetComponent<PlayerController>();
        PlayerStats = GetComponent<PlayerStats>();
    }
}
