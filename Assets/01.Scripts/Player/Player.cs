using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerController Controller;
    public UIPlayerStats PlayerStats;

    void Awake()
    {
        Managers.Player.Player = this;
        Controller = GetComponent<PlayerController>();
        PlayerStats = GetComponent<UIPlayerStats>();
    }
}
