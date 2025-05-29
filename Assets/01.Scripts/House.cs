using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    public PlayerInteraction interaction;
    private float workSpeed = 0.5f;


    void Start()
    {
        interaction.InteractInterval = workSpeed;
        interaction.OnPlayerInteraction = OnPlayerInteraction;
    }

    void OnPlayerInteraction(Player pc)
    {
        Managers.Player.Player.PlayerStats.Health.curValue += 50;
    }
}