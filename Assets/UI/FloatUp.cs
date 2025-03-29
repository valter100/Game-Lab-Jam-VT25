using UnityEngine;

public class FloatUp : MonoBehaviour
{
    public float amplitude = 2f; 
    public float frequency = 1f; 
    private float startX;
    [SerializeField] private float speed;

    void Start()
    {
        startX = transform.position.x;
    }
    public void Update()
    {
        //float xOffset = Mathf.Sin(Time.time * frequency) * amplitude;
        //transform.position = new Vector3(startX + xOffset, transform.position.y, transform.position.z);
        transform.position += speed * Time.deltaTime * Vector3.up;
        transform.LookAt(Camera.main.transform.position);
        transform.Rotate(0, 180, 0);
        //Vector3 direction = (Camera.main.transform.position - transform.position);
        //Vector3 newDirection = Vector3.RotateTowards(transform.forward, direction, 1000, 0.0f);
        //transform.rotation = Quaternion.LookRotation(newDirection);
    }
}
