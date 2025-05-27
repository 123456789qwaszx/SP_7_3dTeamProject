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
    public enum AIState {Idle, Wandering, Attacking}

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
        
        //playerDistance = Vector3.Distance(transform.position, CharacterManager.Instance.Player.transform.position);
        
        animator.SetBool("Moving", currentState != AIState.Idle);

        switch (currentState)
        {
            case AIState.Idle:
            case AIState.Wandering:
                PassiveUpdate();
                break;
            case AIState.Attacking:
                AttackingUpdate();
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
                agent.speed = monsterData.walkSpeed;
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
            default: break;
        }

        animator.speed = agent.speed/monsterData.walkSpeed;
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
    private void AttackingUpdate()
    {
        if (playerDistance < monsterData.attackDistance && IsPlayerInFieldOfView())
        {
            agent.isStopped = true;
            if (Time.time - lastAttackTime > monsterData.attackRate)
            {
                lastAttackTime = Time.time;
                //CharacterManager.Instance.Player.controller.GetComponent<IDamageable>().TakePhysicalDamage(damage);
                animator.speed = 1;

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
                // if (agent.CalculatePath(CharacterManager.Instance.Player.transform.position, path))
                // {
                //     agent.SetDestination(CharacterManager.Instance.Player.transform.position);
                // }
                // else
                // {
                //     agent.SetDestination(transform.position);
                //     agent.isStopped = true;
                //     SetState(AIState.Wandering);
                // }
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
        //Vector3 directionToPlayer = CharacterManager.Instance.Player.transform.position - transform.position;
        //float angle = Vector3.Angle(transform.forward, directionToPlayer);
        return false; //angle < fieldOfView * 0.5f;
    }

    public void TakePhysicalDamage(int damage)
    {
         currentHp -= damage;
        
        onTakeDamage?.Invoke();
        
        if (currentHp <= 0)
        {
            Die();
        }
        
        StartCoroutine((DamageFlash()));

    }

    IEnumerator DamageFlash()
    {
        for (int i = 0; i < meshRenderers.Length; i++)
        {
            meshRenderers[i].material.color = new Color(1.0f, 0.6f, 0.6f);
        }
        
        yield return new WaitForSeconds(0.1f);
        
        for (int i = 0; i < meshRenderers.Length; i++)
        {
            meshRenderers[i].material.color = Color.white;
        }
    }
    
    
    void Die()
    {
        isDead = true;
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
        
        Destroy(gameObject);
    }
    protected virtual void OnMonsterStart() { }
    protected virtual void OnMonsterUpdate() { }
    protected virtual void OnDeath() { }
}
