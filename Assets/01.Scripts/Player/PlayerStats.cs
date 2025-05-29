using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

public class PlayerStats : MonoBehaviour
{
    // public UIOpenClose uiOpenClose;
    public float noHungerHealthDecay = 5f;
    public UIStat uiStat;
    [SerializeField]
    private Condition _health;
    public Condition Health             // 체력
    {
        get { return _health; }
        set { _health = value; }
    }
    [SerializeField]
    private Condition _hunger;
    public Condition Hunger             // 배고픔
    {
        get { return _hunger; }
        set { _hunger = value; }
    }
    [SerializeField]
    private Condition _hydration;

    public Condition Hydration           // 수분
    {
        get { return _hydration; }
        set { _hydration = value; }
    }
    [SerializeField]
    private float _attack;

    public float Attack                  // 공격력
    {
        get { return _attack;}
        set { _attack = value; }
    }
    [SerializeField]
    private float _armor;
    public float Armor                   // 방어력
    {
        get {return _armor; }
        set { _armor = value; }
    }
    [SerializeField]
    private float _speed;

    public float Speed                   // 스피드
    {
        get {return _speed; }
        set { _speed = value; }
    }
    [SerializeField]
    private float _jumpFoce;             

    public float JumpForce               // 점프
    {
        get {return _jumpFoce; }
        set { _jumpFoce = value; }
    }
    

    void Awake()
    {
        //Managers.Player.Player.PlayerStats = this;
    }
    void Update()
    {

        // //Hunger.Subtract(noHungerHealthDecay * Time.deltaTime);
        // Hunger.Subtract(Hunger.passiveValue * Time.deltaTime);
        // Hydration.Subtract(Hydration.passiveValue * Time.deltaTime);
        //
        // if (Hydration.curValue <= 0f)
        // {
        //     Health.Subtract(noHungerHealthDecay * Time.deltaTime);
        // }
        // if (Hunger.curValue <= 0f)
        // {
        //     Health.Subtract(noHungerHealthDecay * Time.deltaTime);
        // }
        // if (Health.curValue <= 0f)
        // {
        //     Die();
        // }

    }
    // public void Heal(float amount)
    // {
    //     Health.Add(amount);
    // }
    // public void Drink(float amount)
    // {
    //     Hydration.Add(amount);
    // }
    // public void Eat(float amount)
    // {
    //     Health.Add(amount);
    // }
    // public void Die()
    // {
    //     // Debug.Log("died.");
    //     if (uiOpenClose != null)
    //     {
    //         GameManager.Instance.GameOver(); // 게임오버: 인게임 정지.
    //         uiOpenClose.OCGameOver(); // 게임오버 UI 활성화
    //     }
    //     else
    //     {
    //         Debug.LogWarning("UIOpenClose가 연결되지 않았습니다.");
    //     }
    // }
}
