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
    private float originalAttackRate;
    private Vector3 originalScale;
    private Vector3 rageScale;
    private bool hasRaged = false;
    protected override void OnMonsterStart()
    {
        originalScale = transform.localScale;
        originalDamage = monsterData.damage;
        originalSpeed = monsterData.runSpeed;
        originalAttackRate = monsterData.attackRate;
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
        StartCoroutine(IncreaseScale());
        currentDamage = originalDamage * 1.5f;
        agent.speed = monsterData.runSpeed * 1.5f;
        currentAttackRate = monsterData.attackRate * 0.66f;
        
        animator.SetTrigger("Rage");
        
        Debug.Log("Rage 활성화");
    }

    void EndRage()
    {
        isRaging = false;
        transform.localScale = originalScale;
        currentDamage = originalDamage;
        currentAttackRate = originalAttackRate;
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
    private IEnumerator IncreaseScale()
    {
        rageScale = originalScale * 1.5f;
        float elapsed = 0;
        float time = 1.5f;
        while (elapsed < time)
        {
            elapsed += Time.deltaTime;
            transform.localScale = Vector3.Lerp(originalScale, rageScale, elapsed / time);
            yield return null;
        }
        transform.localScale = rageScale;
    }
    void TakeTestDamage(float amount)
    {
        TakeDamage(amount);
        Debug.Log($"테스트 데미지: {amount}, 현재 체력: {currentHp}");
    }
}