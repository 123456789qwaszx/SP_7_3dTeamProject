using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
//using UnityEngine.InputSystem.Android;
using Random = UnityEngine.Random;


public class FrostBear : MonsterBase//, IDamageable
{
    private bool isRaging = false;
    private float rageStartTime;
    private float originalDamage;
    private float originalSpeed; 
    private bool hasRaged = false;
    protected override void OnMonsterStart()
    {
        originalDamage = monsterData.damage;
        originalSpeed = monsterData.runSpeed;
    }
    protected override void OnMonsterUpdate()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            TakeTestDamage(40f);
        }
        if (currentHp < monsterData.health * 0.3f && !isRaging && !hasRaged)
        {
            StartRage();
        }
        if (isRaging && Time.time - rageStartTime > GetRageAbility().duration)
        {
            EndRage();
        }
    }

    void StartRage()
    {
        hasRaged = true;
        isRaging = true;
        rageStartTime = Time.time;
        
        // 능력치 강화
        currentDamage *= 1.5f;
        agent.speed = monsterData.runSpeed * 1.5f; 
        
        animator.SetTrigger("Rage");
        
        Debug.Log("Rage 활성화");
    }

    void EndRage()
    {
        isRaging = false;
        currentDamage = originalDamage;
        agent.speed = originalSpeed;
        Debug.Log("Rage 비활성화");
    }

    MonsterAbility GetRageAbility()
    {
        if (monsterData.abilities == null || monsterData.abilities.Length == 0) return null;

        foreach (var ability in monsterData.abilities)
        {
            if (ability.abilityName.Equals("Rage"))
            {
                return ability;
            }
        }
        
        return monsterData.abilities[0];
    }
    void TakeTestDamage(float amount)
    {
        TakePhysicalDamage(amount);
        Debug.Log($"테스트 데미지: {amount}, 현재 체력: {currentHp}");
    }
}