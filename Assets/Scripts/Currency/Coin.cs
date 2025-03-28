using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    [SerializeField] private float value;
    public float Value => value;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.right * Time.deltaTime * speed);
    }
}
