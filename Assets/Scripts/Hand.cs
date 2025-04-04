using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Hand : MonoBehaviour
{
    [SerializeField] InputActionReference fireAction;
    [SerializeField] Weapon weapon;
    [SerializeField] TMP_Text ammoText;
    [SerializeField] HandType handType;

    public enum HandType
    {
        Left,
        Right
    }

    Player player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GetComponentInParent<Player>();
    }

    private void Update()
    {

    }
    public void EquipWeapon()
    {
        weapon = player.GetWeapon();
        weapon.SetHoldingHand(this);

        if (weapon.transform.parent != null)
        {
            Transform parent = weapon.transform.parent;
            parent.parent = transform;

            parent.transform.position = transform.position;
            parent.transform.rotation = transform.rotation;
            parent.parent = transform;
        }
        else
        {
            weapon.transform.position = transform.position;
            weapon.transform.rotation = transform.rotation;
            weapon.transform.parent = transform;
        }

        GetComponent<Renderer>().enabled = false;
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

    public Weapon GetWeapon()
    {
        return weapon;
    }

    public TMP_Text GetAmmoText() => ammoText;
    public HandType GetHandType() => handType;
}
