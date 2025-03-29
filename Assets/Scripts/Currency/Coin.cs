using UnityEngine;

public class Coin : MonoBehaviour, Collectable
{
    [SerializeField] float rotationSpeed = 1f;
    [SerializeField] float speed = 1f;
    [SerializeField] private float value = 1;
    public float Value => value;
    private Transform player;
    private bool flying = false;
    void Start()
    {
        
    }

    void Update()
    {
        transform.Rotate(new Vector3(1, 1, 1) * Time.deltaTime * rotationSpeed);
        if(flying && player != null)
        {
            transform.position = Vector3.Lerp(transform.position, player.position, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, player.position) < 0.5f)
            {
                OnPickup();
                Destroy(gameObject);
            }
        }


    }

    public void OnPickup()
    {
        CoinManager.Instance.PickUp(value);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PickupRadius"))
        {

            player = other.transform;
            flying = true;
        }
    }
}
