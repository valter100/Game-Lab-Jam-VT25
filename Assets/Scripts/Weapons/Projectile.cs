using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float lifeTime;
    [SerializeField] Vector3 direction;
    [SerializeField] float speed;
    [SerializeField] float damage;

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

    public void InitializeProjectile(float damage, Vector3 direction)
    {
        this.damage = damage;
        this.direction = direction;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Enemy")
        {
            Debug.Log("Enemy Hit");
        }
    }
}
