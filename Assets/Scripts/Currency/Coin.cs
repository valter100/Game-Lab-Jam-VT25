using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    [SerializeField] private float value = 1;
    public float Value => value;
    void Start()
    {
        
    }

    void Update()
    {
        transform.Rotate(Vector3.right * Time.deltaTime * speed);
    }

    public void PickUp()
    {
        CoinManager.Instance.PickUp(value);
    }
}
