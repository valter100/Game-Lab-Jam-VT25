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

        //transform.rotation = Quaternion.LookRotation(direction);
    }

    public void SetBounces(int amount)
    {
        bounceLeft = amount;
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
            enemy.TakeDamage(damage, transform.position, damageType);

            //GetComponent<AudioSource>().pitch = Random.Range(0.85f, 1.15f);
            //GetComponent<AudioSource>().Play();

            SpawnEnemyEffect();

            if (bounceLeft > 0)
            {
                hitEnemies.Add(collision.collider);
                bounceLeft--;

                Collider[] nearbyEnemies = Physics.OverlapSphere(collision.transform.position, bounceRadius, LayerMask.GetMask("Enemy"));

                float closestDistance = Mathf.Infinity;
                Collider closestCollider = null;

                foreach(Collider col in nearbyEnemies)
                {
                    if(hitEnemies.Contains(col))
                    {
                        continue;
                    }

                    float distance = Vector3.Distance(transform.position, col.transform.position);

                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestCollider = col;
                    }
                }

                if(closestCollider)
                {
                    direction = (closestCollider.transform.position - transform.position).normalized;
                }
                else
                {
                    //GetComponentInChildren<Renderer>().enabled = false;
                    //GetComponent<Collider>().enabled = false;
                    Destroy(gameObject);
                }
            }
            else
            {
                Destroy(gameObject, 0.5f);
            }
        }
        else if(collision.transform.tag == "Object")
        {
            SpawnWallEffect();
        }
    }
}
