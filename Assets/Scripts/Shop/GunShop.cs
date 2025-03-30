using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GunShop : MonoBehaviour
{
    [SerializeField] List<Weapon> weapons;
    [SerializeField] WeaponCard[] weaponDisplays;
    [SerializeField] GameObject shopCanvas;
    [SerializeField] Player player;
    [SerializeField] Shop shop;
    public GameObject ShopCanvas => shopCanvas;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            ActivateShop();
        }
    }

    public void ShuffleWeapons()
    {

        int count = weapons.Count;
        for (int i = count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            (weapons[i], weapons[randomIndex]) = (weapons[randomIndex], weapons[i]);
        }
    }

    public void ActivateShop()
    {
        if (weapons.Count == 0)
        {
            return;
        }
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        ShuffleWeapons();
        int choices = weapons.Count < 2 ? weapons.Count : 2;
        for (int i = 0; i < choices; i++)
        {
            WeaponCard card = weaponDisplays[i];
            card.UpdateCard(weapons[i].GetName(), weapons[i].GetDescription(), weapons[i], weapons[i].GetTexture());
        }
        for (int i = choices; i < weaponDisplays.Count(); i++)
        {
            weaponDisplays[i].gameObject.SetActive(false    );
        }
        shopCanvas.SetActive(true);
    }

    public void DeactivateShop()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        shopCanvas.SetActive(false);
    }

    public void ChooseWeapon(Weapon weapon)
    {
        List<BaseUpgrade> upgradesToAdd = weapon.upgradesHolder.GetComponentsInChildren<BaseUpgrade>().ToList();
        for (int j = 0; j < upgradesToAdd.Count; j++)
        {
            shop.upgrades.Add(upgradesToAdd[j]);
        }

        player.AddWeapon(weapon);
        weapons.Remove(weapon);
        DeactivateShop();
    }
}
