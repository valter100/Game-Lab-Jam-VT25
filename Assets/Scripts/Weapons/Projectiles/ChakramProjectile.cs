using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class ChakramProjectile : Projectile
{
    [SerializeField] int bounceLeft;
    [SerializeField] float bounceRadius;

    List<Collider> hitEnemies = new List<Collider>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        if(hitEnemies.Contains(collision.collider))
        {
            return;
        }

        if (collision.gameObject.GetComponent<Enemy>())
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.TakeDamage(damage, damageType);

            if(bounceLeft > 0)
            {
                hitEnemies.Add(collision.collider);

                Collider[] nearbyEnemies = Physics.OverlapSphere(collision.transform.position, bounceRadius, LayerMask.GetMask("Enemy"));

                float closestDistance = Mathf.Infinity;
                Collider closestCollider = null;

                foreach(Collider col in nearbyEnemies)
                {
                    if(hitEnemies.Contains(col))
                    {
                        continue;
                    }

                    float distance = Vector3.Distance(collision.transform.position, col.transform.position);

                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestCollider = col;
                    }
                }

                if(closestCollider)
                {
                    direction = (closestCollider.transform.position - collision.transform.position).normalized;
                }
                else
                {
                    Destroy(gameObject);
                }
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
