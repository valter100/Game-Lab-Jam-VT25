using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
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
    [SerializeField] int currentExp;
    [SerializeField] int[] expPerLevel;

    [SerializeField] Slider expSlider;
    [SerializeField] TMP_Text currentLevelText;
    [SerializeField] TMP_Text nextLevelText;

    float xRotation = 0;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        expRequiredToLevel = expPerLevel[level - 1];
        expSlider.maxValue = expRequiredToLevel;
        currentLevelText.text = level.ToString();
        nextLevelText.text = (level + 1).ToString();

        leftHand.EquipWeapon();
        rightHand.EquipWeapon();
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

        Hand hand = weapon.GetHoldingHand();

        weapon.GetHoldingHand().SetWeapon(null);

        hand.EquipWeapon();

        Transform weaponTransform = null;

        if(weapon.transform.parent && !weapon.transform.parent.GetComponent<Hand>())
        {
            weaponTransform = weapon.transform.parent;
        }
        else
        {
            weaponTransform = weapon.transform;
        }

        weaponTransform.transform.parent = null;
        weaponTransform.transform.position = new Vector3(0, 0, 10000);
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

        transform.Rotate(Vector3.up * lookAction.action.ReadValue<Vector2>().x * rotationSensitivity);

        xRotation -= lookAction.action.ReadValue<Vector2>().y * rotationSensitivity;
        xRotation = Mathf.Clamp(xRotation, -90, 90);

        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
    }

    public void GainExp(int expGained)
    {
        currentExp += expGained;
        expSlider.value = currentExp;

        if (currentExp >= expRequiredToLevel)
        {
            LevelUp(currentExp - expRequiredToLevel);
        }
    }

    public void LevelUp(int overShoot)
    {
        level++;
        expRequiredToLevel = expPerLevel[level - 1];

        currentExp = overShoot;

        expSlider.maxValue = expRequiredToLevel;
        expSlider.value = currentExp;

        currentLevelText.text = level.ToString();
        nextLevelText.text = (level + 1).ToString();

        //Spawn shop UI
    }

    public Vector3 GetForward() => playerCamera.transform.forward;
    public GameObject GetCamera() => playerCamera;
}
