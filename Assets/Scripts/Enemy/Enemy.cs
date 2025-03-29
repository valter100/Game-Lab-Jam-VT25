using UnityEngine;

public class Enemy : BaseEnemy
{
    protected override void HandleCombat()
    {

    }

    protected override void HandleMovement()
    {
        agent.SetDestination(target.transform.position);
    }
}
