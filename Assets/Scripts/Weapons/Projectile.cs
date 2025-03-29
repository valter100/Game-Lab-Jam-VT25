using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float lifeTime;
    [SerializeField] Vector3 direction;
    [SerializeField] float speed;
    [SerializeField] float damage;

    DamageType damageType = DamageType.Air;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    public void InitializeProjectile(float damage, Vector3 direction, float projectileSpeed)
    {
        this.damage = damage;
        this.direction = direction;

        speed = projectileSpeed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<Enemy>())
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.TakeDamage(damage, damageType);

            Destroy(gameObject);
        }
    }
}
