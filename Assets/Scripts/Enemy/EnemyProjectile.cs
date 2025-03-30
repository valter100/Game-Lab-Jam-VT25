using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public Vector3 targetDirection;
    public float damage;
    [SerializeField] float speed = 10;
    [SerializeField] float maxDistance = 20;
    Vector3 originalLocation;
    void Start()
    {
        originalLocation = transform.position;  
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += targetDirection * speed * Time.deltaTime;
        if (Vector3.Distance(transform.position, originalLocation) > maxDistance )
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHealth>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
