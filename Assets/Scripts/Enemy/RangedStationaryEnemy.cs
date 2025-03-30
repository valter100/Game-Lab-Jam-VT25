using UnityEngine;

public class RangedStationaryEnemy : BaseEnemy
{
    [SerializeField] float range = 20f;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float attackSpeed = 4f;
    float attackSpeedReset = 4f;

    public void Start()
    {
        attackSpeedReset = attackSpeed;
    }
    protected override void HandleCombat()
    {
        attackSpeed -= Time.deltaTime;
        if (attackSpeed < 0)
        {
            if (CheckProjectile())
            {
                Shoot();
                attackSpeed = attackSpeedReset;
            }

        }
    }



    protected override void HandleMovement()
    {
        Vector3 pos = target.transform.position;
        pos.y = transform.position.y;
        transform.LookAt(pos);
    }

    public bool CheckProjectile()
    {
        if (Vector3.Distance(transform.position, target.transform.position) > range)
        {
            return false;
        }
        RaycastHit hit;
        Vector3 direction = (target.transform.position - transform.position).normalized;
        if (Physics.Raycast(transform.position, direction, out hit, Mathf.Infinity))
        {
            if (hit.collider.CompareTag("Player"))
            {
                return true;
            }
            Debug.Log(hit.collider.name);
        }
        else
        {
            Debug.Log("Dit not hit");
        }

        return false;

    }

    public void Shoot()
    {
        EnemyProjectile proj = Instantiate(projectilePrefab, transform.position, Quaternion.identity).GetComponent<EnemyProjectile>();
        proj.targetDirection = (target.transform.position - transform.position).normalized;
        proj.damage = damage;
    }

}
