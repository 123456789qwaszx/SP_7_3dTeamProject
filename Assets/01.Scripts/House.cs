using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    public PlayerInteraction interaction;

    private float workSpeed = 2;


    void Start()
    {
        interaction.InteractInterval = workSpeed;
        interaction.OnPlayerInteraction = OnPlayerInteraction;
    }


    float amount;

    void OnPlayerInteraction(Player pc)
    {
        Managers.Player.Player.PlayerStats.Heal(amount);
    }
}