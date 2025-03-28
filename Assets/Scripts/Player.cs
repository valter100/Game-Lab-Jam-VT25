using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] Hand leftHand;
    [SerializeField] Hand rightHand;

    [SerializeField] List<Weapon> weapons = new List<Weapon>();
    [SerializeField] List<Weapon> equippedWeapons = new List<Weapon>();

    [SerializeField] InputActionReference moveAction;
    [SerializeField] InputActionReference lookAction;

    [SerializeField] float movementSpeed;

    void Update()
    {
        leftHand.HandleInput();
        rightHand.HandleInput();

        HandleInput();
    }

    public void ReturnWeapon(Weapon weapon)
    {
        equippedWeapons.Remove(weapon);
        weapons.Add(weapon);

        weapon.GetHoldingHand().SetWeapon(null);

        weapon.transform.parent = null;
        weapon.transform.position = new Vector3(0, 0, 10000);
    }

    public Weapon GetWeapon()
    {
        Weapon weapon = weapons[0];
        weapons.RemoveAt(0);
        equippedWeapons.Add(weapon);
        return weapon;
    }

    public void HandleInput()
    {
        Vector2 moveVector = moveAction.action.ReadValue<Vector2>();

        float moveForward = moveVector.y;
        float moveSide = moveVector.x;

        Vector3 forwardMovement = transform.forward * moveForward * movementSpeed * Time.deltaTime;
        Vector3 sideMovement = transform.right * moveSide * movementSpeed * Time.deltaTime;

        Vector3 movement = forwardMovement + sideMovement;

        transform.Translate(movement, Space.World);

        //transform.position += new Vector3(.x, 0, moveAction.action.ReadValue<Vector2>().y) * movementSpeed * Time.deltaTime;
        transform.Rotate(0, lookAction.action.ReadValue<Vector2>().x,0);
    }
}
