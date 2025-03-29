using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    [SerializeField] protected float lifeTime;
    [SerializeField] protected Vector3 direction;
    [SerializeField] protected float speed;
    [SerializeField] protected float damage;

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

            Destroy(gameObject);
        }
    }
}
