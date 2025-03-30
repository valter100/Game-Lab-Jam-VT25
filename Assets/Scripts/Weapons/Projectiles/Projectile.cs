using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    [SerializeField] protected float lifeTime;
    [SerializeField] protected Vector3 direction;
    [SerializeField] protected float speed;
    [SerializeField] protected float damage;
    [SerializeField] protected ParticleSystem wallHitEffect;
    [SerializeField] protected ParticleSystem enemyHitEffect;

    protected DamageType damageType = DamageType.Air;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    protected void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    public void InitializeProjectile(float damage, Vector3 direction, float projectileSpeed, DamageType damageType)
    {
        this.damage = damage;
        this.direction = direction;
        this.damageType = damageType;

        speed = projectileSpeed;
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<Enemy>())
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.TakeDamage(damage, transform.position, damageType);

            SpawnEnemyEffect();

            Destroy(gameObject);
        }
        else if(collision.gameObject.tag == "Object")
        {
            SpawnWallEffect();

            Destroy(gameObject);
        }
    }

    protected void SpawnEnemyEffect()
    {
        ParticleSystem spawnedSystem = enemyHitEffect;

        var rend = spawnedSystem.GetComponent<ParticleSystemRenderer>();
        //rend.sharedMaterial.color = GetComponentInChildren<Renderer>().sharedMaterial.color;

        Instantiate(spawnedSystem.gameObject, transform.position, Quaternion.identity);
    }

    protected void SpawnWallEffect()
    {
        ParticleSystem spawnedSystem = wallHitEffect;

        var rend = spawnedSystem.GetComponent<ParticleSystemRenderer>();
        //rend.sharedMaterial.color = GetComponentInChildren<Renderer>().sharedMaterial.color;

        Instantiate(spawnedSystem, transform.position, Quaternion.identity);
    }
}
