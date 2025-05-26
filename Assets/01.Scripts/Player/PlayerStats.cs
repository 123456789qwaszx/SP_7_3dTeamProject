using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private float _health;
    public float Health
    {
        get;
        set;
    }

    void Awake()
    {
        Managers.Player.Player.PlayerStats = this;
    }

}
