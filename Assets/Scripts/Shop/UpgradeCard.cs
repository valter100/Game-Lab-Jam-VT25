using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeCard : MonoBehaviour
{
    [Header("Shop")]
    [SerializeField] Shop shop;
    [Header("Information")]
    [SerializeField] TextMeshProUGUI title;
    [SerializeField] TextMeshProUGUI description;
    [SerializeField] TextMeshProUGUI cost;
    [SerializeField] RawImage image;

    BaseUpgrade upgrade;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateCard(string title, string description, string cost, BaseUpgrade upgrade, Texture imageTexture)
    {
        this.title.text = title;
        this.description.text = description;
        this.cost.text = cost;
        this.upgrade = upgrade;
        this.image.texture = imageTexture;

        if (shop.CheckPrice(upgrade.cost))
        {
            this.cost.color = Color.black;
        }
        else
        {
            this.cost.color = Color.red;
        }
    }

    public void UseUpgrade()
    {
        if (!shop.CheckPrice(upgrade.cost))
        {
            return;
        }

        upgrade.DoUpgrade();
        shop.CheckCard(upgrade);
        shop.DeactiveShop();

    }
}
