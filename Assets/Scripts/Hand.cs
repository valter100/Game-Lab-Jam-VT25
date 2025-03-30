using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Hand : MonoBehaviour
{
    [SerializeField] InputActionReference fireAction;
    [SerializeField] Weapon weapon;
    [SerializeField] TMP_Text ammoText;
    Player player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GetComponentInParent<Player>();
    }

    private void Update()
    {
    }

    public void AssignWeapon()
    {
        if (weapon)
        {
            return;
        }
        weapon = player.GetWeapon();

        weapon.SetHoldingHand(this);

        weapon.transform.position = transform.position;
        weapon.transform.rotation = transform.rotation;
        weapon.transform.parent = transform;
        GetComponent<Renderer>().enabled = false;

    }

    public void HandleInput()
    {
        if (fireAction.action.inProgress && weapon != null)
        {
            weapon.Attack();
        }
    }

    public void SetWeapon(Weapon newWeapon)
    {
        weapon = newWeapon;
    }

    public TMP_Text GetAmmoText() => ammoText;
}
