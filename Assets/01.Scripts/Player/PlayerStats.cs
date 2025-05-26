using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private float _health;
    public float Health
    {
        get{ return _health; }
        set{ _health = value; }
    }

    void Awake()
    {
        Managers.Player.Player.PlayerStats = this;
    }

}
