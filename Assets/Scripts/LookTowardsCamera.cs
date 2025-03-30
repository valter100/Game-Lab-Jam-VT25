using UnityEngine;

public class LookTowardsCamera : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Camera.main.transform.position);
        transform.Rotate(0, 180, 0);
    }

    public void DestroyText()
    {
        Destroy(gameObject);
    }
}
