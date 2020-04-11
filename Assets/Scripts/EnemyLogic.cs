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
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (agent)
        {
            agent.SetDestination(destination.position);
        }
    }
}
