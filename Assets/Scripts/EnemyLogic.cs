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
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if(PatrolStart)
        {
            CurrentPosition = PatrolStart.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(PatrolStart && PatrolEnd)
        Patrol();
        /*if (agent && destination)
        {
            agent.SetDestination(destination.position);
        }
        float distance = Vector3.Distance(destination.transform.position,agent.transform.position);
        if(distance  < 1.5f)
        {
            agent.isStopped = true;
            agent.velocity = Vector3.zero;
        }
        else
        {
            agent.isStopped = false;
        }*/
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
}
