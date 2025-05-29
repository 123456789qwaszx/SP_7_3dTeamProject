using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.VFX;

public class PlayerStats : MonoBehaviour
{
    // public UIOpenClose uiOpenClose;
    public float noHungerHealthDecay = 5f;
    public UIStat uiStat;
    [SerializeField]

    private Condition _health; 
    public SFXManager sfxManager;
    private bool _isDead = false;
    public Condition Health // 체력

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
    


    void Update()
    {
            if (sfxManager != null && sfxManager.playerDieSFX != null)
            {
                sfxManager.PlaySFX(sfxManager.playerDieSFX, transform.position);

            }
    }
}
