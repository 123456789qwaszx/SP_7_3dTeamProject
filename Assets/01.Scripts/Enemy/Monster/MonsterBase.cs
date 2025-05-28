using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class MonsterBase : MonoBehaviour
{
    public MonsterData monsterData;

    protected NavMeshAgent agent;
    protected Animator animator;
    protected Transform player;

    public float currentHp;
    public float currentDamage;
    private float playerDistance;
    private float lastAttackTime;

    public float minWanderDistance = 5f;
    public float maxWanderDistance = 10f;
    public float minWanderWaitTime = 1f;
    public float maxWanderWaitTime = 5f;
    
    public float fieldOfView = 120f;
    private SkinnedMeshRenderer[] meshRenderers;
    public enum AIState {Idle, Wandering, Attacking, Fleeing}

    public AIState currentState;
    public event Action onTakeDamage;

    protected bool isDead = false;
    protected virtual void Start()
    {
        GetComponents();
        GetData();
        OnMonsterStart();
    }

    protected virtual void Update()
    {
        if (isDead) return;
        
        playerDistance = Vector3.Distance(transform.position, player.position);       
        float speed = agent.velocity.magnitude / agent.speed;
        animator.SetFloat("Blend", speed);

        switch (currentState)
        {
            case AIState.Idle:
            case AIState.Wandering:
                PassiveUpdate();
                break;
            case AIState.Attacking:
                AttackingUpdate();
                break;
            case AIState.Fleeing:
                FleeingUpdate();
                break;
        }
        OnMonsterUpdate();
    }


    // 시작시 필요 컴포넌트 불러오기
    private void GetComponents()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        meshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();
        
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            player = playerObj.transform;

    }
    //시작 시 몬스터 데이터 가져오기
    private void GetData()
    {
        currentHp = monsterData.health;
        currentDamage = monsterData.damage;
        agent.speed = monsterData.walkSpeed;
        
        currentState = AIState.Wandering;
    }
    
    protected virtual void SetState(AIState state)
    {
        currentState = state;
        
        switch (currentState)
        {
            case AIState.Idle:
                agent.speed = 0;
                agent.isStopped = true;
                break;
            case AIState.Wandering:
                agent.speed = monsterData.walkSpeed;
                agent.isStopped = false;
                break;
            case AIState.Attacking: 
                agent.speed = monsterData.runSpeed;
                agent.isStopped = false;
                break;
            case AIState.Fleeing:
                agent.speed = monsterData.runSpeed * 1.2f;
                agent.isStopped = false;
                break;
            default: break;
        }
    }

    
    private void PassiveUpdate()
    {
        if (currentState == AIState.Wandering && agent.remainingDistance < 0.1f)
        {
            SetState(AIState.Idle);
            Invoke("WanderToNewLocation", Random.Range(minWanderWaitTime, maxWanderWaitTime));
        }

        if (playerDistance < monsterData.detectDistance)
        {
            SetState(AIState.Attacking);
        }
    }
    void WanderToNewLocation()
    {
        if (currentState != AIState.Idle) return;
        
        SetState(AIState.Wandering);
        agent.SetDestination(GetWanderLocation());
    }

    
    Vector3 GetWanderLocation()
    {
        NavMeshHit hit;
        
        NavMesh.SamplePosition(
            transform.position + (Random.onUnitSphere * Random.Range(minWanderDistance, maxWanderDistance)), out hit,
            maxWanderDistance, NavMesh.AllAreas);

        int i = 0;

        while (Vector3.Distance(transform.position, hit.position) < monsterData.detectDistance)
        {
            NavMesh.SamplePosition(
                transform.position + (Random.onUnitSphere * Random.Range(minWanderDistance, maxWanderDistance)), out hit,
                maxWanderDistance, NavMesh.AllAreas);
            i++;
            if (i == 30) break;
        }
        
        return hit.position;
    }
    private void FleeingUpdate()
    {
        Vector3 fleeDirection = (transform.position - player.position).normalized;
        Vector3 fleeTarget = transform.position + fleeDirection * 10f;
        
        NavMeshHit hit;
        if (NavMesh.SamplePosition(fleeTarget, out hit, 10f, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }
        
        // 충분히 멀어지면 일반 상태로 복귀
        if (playerDistance > monsterData.detectDistance * 1.5f)
        {
            SetState(AIState.Wandering);
        }
    }
    private void AttackingUpdate()
    {
        if (playerDistance < monsterData.attackDistance && IsPlayerInFieldOfView())
        {
            agent.isStopped = true;
            agent.SetDestination(transform.position);
            
            if (Time.time - lastAttackTime > monsterData.attackRate)
            {
                lastAttackTime = Time.time;
                //Managers.Player.Player.Controller.GetComponent<IDamageable>().TakePhysicalDamage(damage);

                int index = Random.Range(0, 4);
                animator.SetInteger("AttackIndex", index);
                
                animator.SetTrigger("Attack");
            }
        }
        else
        {
            if (playerDistance < monsterData.detectDistance)
            {
                agent.isStopped = false;
                NavMeshPath path = new NavMeshPath();
                if (agent.CalculatePath(player.position, path))
                {
                    agent.SetDestination(player.position);
                }
                else
                {
                    agent.SetDestination(transform.position);
                    agent.isStopped = true;
                    SetState(AIState.Wandering);
                }
            }
            else
            {
                agent.SetDestination(transform.position);
                agent.isStopped = true;
                SetState(AIState.Wandering);
            }
        }
    }
    bool IsPlayerInFieldOfView()
    {
        Vector3 directionToPlayer = player.position - transform.position;
        float angle = Vector3.Angle(transform.forward, directionToPlayer);
        return angle < fieldOfView * 0.5f;
    }

    public void TakePhysicalDamage(float damage)
    {
         currentHp -= damage;
        
        onTakeDamage?.Invoke();
        
        if (currentHp <= 0)
        {
            Die();
        }
        else
        {
            if (currentHp < monsterData.health * 0.1f)
            {
                SetState(AIState.Fleeing);
            }
        }
        
        StartCoroutine((DamageFlash()));

    }

    IEnumerator DamageFlash()
    {
        for (int i = 0; i < meshRenderers.Length; i++)
        {
            meshRenderers[i].material.color = new Color(1.0f, 0.6f, 0.6f);
        }
        
        yield return new WaitForSeconds(0.2f);
        
        for (int i = 0; i < meshRenderers.Length; i++)
        {
            meshRenderers[i].material.color = Color.white;
        }
    }
    
    
    void Die()
    {
        isDead = true;
        if (agent != null)
        {
            agent.isStopped = true;
            agent.enabled = false;
        }
        animator.SetTrigger("Death");
        for (int i = 0; i <monsterData.dropItems.Length; i++)
        {
            if (Random.value <= monsterData.dropItems[i].dropRate)
            {
                int quantity = Random.Range(monsterData.dropItems[i].minQuantity, monsterData.dropItems[i].maxQuantity + 1);
                for (int j = 0; j < quantity; j++)
                {
                    Instantiate(monsterData.dropItems[i].itemPrefab, transform.position+ Vector3.up *2, Quaternion.identity);
                }
            }
        }
        StartCoroutine(DelayDeath());
    }

    IEnumerator DelayDeath()
    {
        yield return new WaitForSeconds(2f);
        
        OnDeath();
        Destroy(gameObject);
     }
    
    protected virtual void OnMonsterStart() { }
    protected virtual void OnMonsterUpdate() { }
    protected virtual void OnDeath() { }
}
