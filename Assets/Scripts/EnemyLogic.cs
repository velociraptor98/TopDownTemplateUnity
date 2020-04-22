using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


enum EnemyState
{
    Idle,
    Patrol,
    Chase,
    Attack
}
public class EnemyLogic : MonoBehaviour
{
    // Start is called before the first frame update
    NavMeshAgent agent;
    EnemyState state = EnemyState.Idle;
    [SerializeField]
    private Transform destination;
    [SerializeField]
    private Transform PatrolStart;
    [SerializeField]
    private Transform PatrolEnd;
    private Vector3 CurrentPosition;
    float meeleRadius = 3.0f;
    const float MAX_ATTACK_COOLDOWN = 0.5f;
    float attackCooldown = MAX_ATTACK_COOLDOWN;
    [SerializeField]
    private int health = 30;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if(PatrolStart)
        {
            CurrentPosition = PatrolStart.position;
        }
    }

    // Update is called once per frame
    void ChasePlayer()
    {
        if (agent && destination)
        {
            agent.SetDestination(destination.position);
        }
        float distance = Vector3.Distance(destination.transform.position, agent.transform.position);
        if (distance < 2.0f)
        {
            agent.isStopped = true;
            agent.velocity = Vector3.zero;
            state = EnemyState.Attack;
        }
        else
        {
            agent.isStopped = false;
        }
    }
    void UpdateAttack()
    {
        float distance = Vector3.Distance(this.transform.position, destination.position);
        if(distance < meeleRadius)
        {
            attackCooldown -= Time.deltaTime;
            if(attackCooldown < 0.0f)
            {
                PlayerController script = destination.GetComponent<PlayerController>();
                if(script)
                {
                    script.TakeDamage(10);
                    print(0);
                }
                attackCooldown = MAX_ATTACK_COOLDOWN;
                
            }
        }
        else
        {
            state = EnemyState.Chase;
        }
    }
    void PlayerSearch()
    {
        float distance = Vector3.Distance(agent.transform.position, destination.position); 
        if(distance < 5.0f)
        {
            state = EnemyState.Chase;
        }
        else 
        {
            state = EnemyState.Patrol;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.25f);
        Gizmos.DrawSphere(transform.position, 5.0f);
    }
    void Update()
    {
        switch(state)
        {
            case EnemyState.Idle:
                PlayerSearch();
                break;
            case EnemyState.Patrol:
                PlayerSearch();
                if (PatrolStart && PatrolEnd)
                    Patrol();
                break;
            case EnemyState.Chase:
                ChasePlayer();
                break;
            case EnemyState.Attack:
                UpdateAttack();
                break;

        }
    }
    void Patrol()
    {
        if(agent && CurrentPosition != Vector3.zero)
        {
            agent.SetDestination(CurrentPosition);
        }
        
            float distance = Vector3.Distance(CurrentPosition, transform.position);
            if(distance < 1.5f)
            {
                if(CurrentPosition == PatrolStart.position)
                {
                    CurrentPosition = PatrolEnd.position;
                }
                else
                {
                    CurrentPosition = PatrolStart.position;
                }
            }
        
    }
   public void takeDamage(int hitPoint)
    {
        health -= hitPoint;
        if(health<=0)
        {
            Destroy(this.gameObject);
        }
    }
}
