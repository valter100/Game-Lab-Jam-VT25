using Unity.VisualScripting;
using UnityEngine;

public class PickupRadius : MonoBehaviour
{
    [SerializeField] float pickupRadius = 5f;
    void Start()
    {
        SphereCollider sc = GetComponent<SphereCollider>();
        sc.radius = pickupRadius;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }
}
