using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class WeaponCard : MonoBehaviour
{
    [Header("Gun Shop")]
    [SerializeField] GunShop shop;
    [Header("Information")]
    [SerializeField] TextMeshProUGUI title;
    [SerializeField] TextMeshProUGUI description;
    [SerializeField] RawImage image;
    [SerializeField] Weapon weapon;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateCard(string title, string description, Weapon weapon, Texture weaponImage)
    {
        this.title.text = title;
        this.description.text = description;
        this.weapon = weapon;
        this.image.texture = weaponImage;
    }

    public void ChooseWeapon()
    {
        shop.ChooseWeapon(weapon);
    }
}
