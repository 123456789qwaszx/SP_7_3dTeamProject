using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
//using UnityEngine.InputSystem.Android;
using Random = UnityEngine.Random;


public class Bear : MonsterBase//, IDamageable
{
    
    private bool isRaging = false;
    private float rageStartTime;
    private float originalDamage;
    private float originalSpeed; 
    protected override void OnMonsterStart()
    {
       originalDamage = monsterData.damage;
       originalSpeed = monsterData.runSpeed;
    }
    protected override void OnMonsterUpdate()
    {
        if (currentHp < monsterData.health * 0.3f && !isRaging) ;
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
        isRaging = true;
        rageStartTime = Time.time;
        
        // 능력치 강화
        currentDamage *= 1.5f;
        agent.speed = monsterData.runSpeed * 1.5f; 
        
        animator.SetBool("Rage", true);
    }

    void EndRage()
    {
        isRaging = false;
        currentDamage = originalDamage;
        agent.speed = originalSpeed;
        animator.SetBool("Rage", false);
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
}



