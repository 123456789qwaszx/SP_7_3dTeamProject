using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPlayerStats : MonoBehaviour
{
    private float playerHealth;

    void Awake()
    {
        playerHealth = Managers.Player.Player.PlayerStats.Health;
    }

    void Update()
    {
        
    }
}
