using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPlayerStats : MonoBehaviour
{
    public PlayerStats uiStat;


    Stat health { get { return uiStat.Health; } }
    Stat hydration { get { return uiStat.Hydration; } }
    Stat hunger { get { return uiStat.Hunger; } }
    private float playerHealth;
    public float noHydrationHealthDecay;
    public float noHungerHealthDecay;
    // Start is called before the first frame update
    void Start()
    {

    }
    void Awake()
    {
        //playerHealth = Managers.Player.Player.PlayerStats.Health;
    }
    // Update is called once per frame
    void Update()
    {
        hydration.Add(hydration.passiveValue * Time.deltaTime);
        hunger.Add(hunger.passiveValue * Time.deltaTime);

        if (hydration.curValue == 0)
        {
            health.Subtract(noHydrationHealthDecay * Time.deltaTime);
        }
        if (hunger.curValue == 0)
        {
            health.Subtract(noHungerHealthDecay * Time.deltaTime);
        }
        if (health.curValue == 0)
        {
            Die();
        }

    }
    public void Die()
    {
        Debug.Log("died.");
    }
}
