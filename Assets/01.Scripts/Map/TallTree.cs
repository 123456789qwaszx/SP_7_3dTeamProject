using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TallTree : MonoBehaviour
{
    public GetTree _tree;
    public PlayerInteraction interaction;

    private float workSpeed = 2;


    void Start()
    {
        interaction.InteractInterval = workSpeed;
        interaction.OnPlayerInteraction = OnPlayerTreeInteraction;
    }

    void OnPlayerTreeInteraction(Player pc)
    {
        // 나중에 Ax를 들었을 때만 동작.
        // 혹은 특정 키값입력 or 능력 등등
        GameManager.Instance.SpawnLog();
        // 생성하고 나무위치로 이동
    }
}
