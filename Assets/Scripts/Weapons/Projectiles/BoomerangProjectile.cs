using System.Collections.Generic;
using UnityEngine;

public class BoomerangProjectile : Projectile
{
    [SerializeField] float maxRange;
    [SerializeField] Boomerang firedWeapon;
    float rangedTraveled;

    bool hasBounced = false;
    Player player;

    List<BaseEnemy> hitEnemies = new List<BaseEnemy>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //base.Start();
        player = FindFirstObjectByType<Player>();
        maxRange = (direction*speed*(lifeTime/2)).magnitude;
    }

    // Update is called once per frame
    void Update()
    {
        if(hasBounced)
        {
            direction = (player.transform.position - transform.position).normalized * 2;
        }
        else if(!hasBounced )
        {
            rangedTraveled += (direction * speed * Time.deltaTime).magnitude;
        }

        transform.position += direction * speed * Time.deltaTime;
        
        if(rangedTraveled >= maxRange && !hasBounced)
        {
            Bounce();
        }
    }

    public void Bounce()
    {
        hasBounced = true;
        rangedTraveled = 0;
        hitEnemies.Clear();
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.GetComponent<Enemy>())
        {
            BaseEnemy enemy = collision.gameObject.GetComponent<BaseEnemy>();

            if(!hitEnemies.Contains(enemy))
            {
                enemy.TakeDamage(damage, damageType);
                hitEnemies.Add(enemy);
            }
        }
        else if(collision.gameObject.tag == "Object" && !hasBounced)
        {
            Bounce();
        }
        else if(collision.gameObject.tag == "Player" && hasBounced)
        {
            if(firedWeapon)
            {
                firedWeapon.SetCanThrow(true);
            }
            Destroy(gameObject);
        }
    }

    public void SetFiredWeapon(Boomerang weapon)
    {
        firedWeapon = weapon;
    }
}
