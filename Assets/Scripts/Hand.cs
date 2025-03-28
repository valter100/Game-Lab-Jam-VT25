using UnityEngine;
using UnityEngine.InputSystem;

public class Hand : MonoBehaviour
{
    [SerializeField] InputActionReference fireAction;
    [SerializeField] Weapon weapon;
    Player player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GetComponentInParent<Player>();
    }

    private void Update()
    {
        if (!weapon)
        {
            weapon = player.GetWeapon();
            weapon.SetHoldingHand(this);
            weapon.transform.position = transform.position;
            weapon.transform.rotation = transform.rotation;
            weapon.transform.parent = transform;
            GetComponent<Renderer>().enabled = false;
        }
    }

    public void HandleInput()
    {
        if (fireAction.action.inProgress)
        {
            weapon.Fire();
        }
    }

    public void SetWeapon(Weapon newWeapon)
    {
        weapon = newWeapon;
    }
}
