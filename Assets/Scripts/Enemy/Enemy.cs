using UnityEngine;
using UnityEngine.AI;

public class Enemy : BaseEnemy
{
    protected NavMeshAgent agent;
    public void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    protected override void HandleCombat()
    {

    }

    protected override void HandleMovement()
    {
        agent.SetDestination(target.transform.position);
        if (agent)
        {
            if (agent.speed != moveSpeed)
            {
                agent.speed = moveSpeed;
            }
        }
    }
}
