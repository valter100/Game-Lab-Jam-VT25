using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] Hand leftHand;
    [SerializeField] Hand rightHand;

    [SerializeField] List<Weapon> weapons = new List<Weapon>();
    [SerializeField] List<Weapon> equippedWeapons = new List<Weapon>();

    [SerializeField] InputActionReference moveAction;
    [SerializeField] InputActionReference lookAction;

    [SerializeField] float movementSpeed;
    [SerializeField] float rotationSensitivity;
    [SerializeField] GameObject playerCamera;

    [SerializeField] RawImage nextWeaponImage;
    [SerializeField] TMP_Text nextWeaponName;

    [SerializeField] int level = 1;
    [SerializeField] int expRequiredToLevel;
    [SerializeField] int[] expPerLevel;

    float xRotation = 0;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

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

        nextWeaponImage.texture = weapons[0].GetTexture();
        nextWeaponName.text = "Next: " + weapons[0].GetName();
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

        transform.Rotate(Vector3.up * lookAction.action.ReadValue<Vector2>().x);

        xRotation -= lookAction.action.ReadValue<Vector2>().y;
        xRotation = Mathf.Clamp(xRotation, -90, 90);

        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
    }

    public void GainExp(int expGained)
    {
        expRequiredToLevel -= expGained;

        if(expRequiredToLevel < 0)
        {
            LevelUp();
        }
    }

    public void LevelUp()
    {
        level++;
        expRequiredToLevel = expPerLevel[level] + expRequiredToLevel;

        //Spawn shop UI
    }

    public Vector3 GetForward() => playerCamera.transform.forward;
    public GameObject GetCamera() => playerCamera;
}
