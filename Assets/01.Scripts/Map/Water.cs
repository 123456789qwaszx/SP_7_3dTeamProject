using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    public PlayerInteraction interaction;

    [SerializeField]
    private float workSpeed = 2f;
    [SerializeField]
    float amount = 50;


    void Start()
    {
        interaction.InteractInterval = workSpeed;
        interaction.OnPlayerInteraction = OnPlayerInteraction;
    }

    void OnPlayerInteraction(Player pc)
    {
        Managers.Player.Player.PlayerStats.Hydration.Add(amount);
    }
}