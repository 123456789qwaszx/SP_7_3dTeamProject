using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    
    [SerializeField]
    private float _health;
    public float Health // 체력
    {
        get{ return _health; }
        set{ _health = value; }
    }
    [SerializeField]
    private float _hunger;
    public float Hunger             // 배고픔
    {
        get{return _hunger; }
        set { _hunger = value; }
    }
    [SerializeField]
    private float _hydration;

    public float Hydration          // 수분
    {
        get{return _hydration; }
        set{_hydration = value;}
    }
    [SerializeField]
    private float _attack;

    public float Attack             // 공격력
    {
        get { return _attack;}
        set { _attack = value; }
    }
    [SerializeField]
    private float _armor;
    public float Armor              // 방어력
    {
        get {return _armor; }
        set { _armor = value; }
    }
    [SerializeField]
    private float _speed;

    public float Speed
    {
        get {return _speed; }
        set { _speed = value; }
    }
    [SerializeField]
    private float _jumpFoce;

    public float JumpForce
    {
        get {return _jumpFoce; }
        set { _jumpFoce = value; }
    }
    

    void Awake()
    {
        //Managers.Player.Player.PlayerStats = this;
    }

}
