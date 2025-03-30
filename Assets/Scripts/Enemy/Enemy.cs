using UnityEngine;
using UnityEngine.AI;

public class Enemy : BaseEnemy
{
    protected NavMeshAgent agent;
    [Header("Slow on hit")]
    [SerializeField] float slowAmount = 0.8f;
    [SerializeField] float slowTime = 0.1f;
    [SerializeField] float speedRestoreTimer = 0.1f;
    protected override void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        base.Awake();
    }
    protected override void HandleCombat()
    {

    }

    public override void ReduceSpeed()
    {
        agent.speed = slowAmount * moveSpeed;
        speedRestoreTimer = slowTime;
    }

    public override void UpdateMoveSpeed(float percentage)
    {
        moveSpeed *= percentage;
        agent.speed = moveSpeed;
    }

    protected override void HandleMovement()
    {
        agent.SetDestination(target.transform.position);


        if (speedRestoreTimer > 0)
        {
            speedRestoreTimer -= Time.deltaTime;
            if (speedRestoreTimer <= 0)
            {
                agent.speed = moveSpeed;
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHealth>().TakeDamage(damage);
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHealth>().TakeDamage(damage);
        }
    }
}
