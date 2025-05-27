using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

public class UIPlayerStats : MonoBehaviour
{
    public UICondition uiCondition;


    Condition health { get { return uiCondition.health; } }
    Condition hydration { get { return uiCondition.hydration; } }
    Condition hunger { get { return uiCondition.hunger; } }
    private float playerHealth;
    public float noHydrationHealthDecay;
    public float noHungerHealthDecay = 5f;
    // Start is called before the first frame update
    void Start()
    {

    }
    void Awake()
    {
    }
    // Update is called once per frame
    void Update()
    {
        hunger.Subtract(noHungerHealthDecay * Time.deltaTime);
        hunger.Add(hunger.passiveValue * Time.deltaTime);
        hydration.Subtract(hydration.passiveValue * Time.deltaTime);

        if (hydration.curValue <= 0f)
        {
            health.Subtract(noHydrationHealthDecay * Time.deltaTime);
        }
        if (hunger.curValue <= 0f)
        {
            health.Subtract(noHungerHealthDecay * Time.deltaTime);
        }
        if (health.curValue <= 0f)
        {
            Die();
        }

    }
    public void Heal(float amount)
    {
        health.Add(amount);
    }
    public void Drink(float amount)
    {
        hydration.Add(amount);
    }
    public void Eat(float amount)
    {
        hunger.Add(amount);
    }
    public void Die()
    {
        Debug.Log("died.");
    }
}
