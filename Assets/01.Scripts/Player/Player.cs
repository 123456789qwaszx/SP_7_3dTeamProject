
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerController Controller;
    public PlayerStats PlayerStats;
    public Interaction interaction;


    public EquipmentData itemData;
    public Action addItem;

    void Awake()
    {
        Managers.Player.Player = this;
        Controller = GetComponent<PlayerController>();
        PlayerStats = GetComponent<PlayerStats>();
    }
}
