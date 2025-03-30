using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public List<BaseUpgrade> upgrades;
    [SerializeField] GameObject rootUpgrades;
    [SerializeField] int upgradeChoices = 3;
    [SerializeField] GameObject shopCanvas;
    [SerializeField] UpgradeCard[] upgradeDisplays;

    public GameObject ShopCanvas => shopCanvas;

    void Start()
    {
        upgrades = rootUpgrades.GetComponentsInChildren<BaseUpgrade>().ToList();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ActivateShop();
        }
    }

    public void ShuffleUpgrades()
    {

        int count = upgrades.Count;
        for (int i = count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            (upgrades[i], upgrades[randomIndex]) = (upgrades[randomIndex], upgrades[i]);
        }
    }

    public void ActivateShop()
    {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        ShuffleUpgrades();
        int choices = upgrades.Count < upgradeChoices ? upgrades.Count : upgradeChoices;
        for (int i = 0; i < upgradeChoices; i++)
        {
            UpgradeCard card = upgradeDisplays[i];
            card.UpdateCard(upgrades[i].title, upgrades[i].description, upgrades[i].cost.ToString(), upgrades[i], upgrades[i].imageTexture);
        }
        for (int i = choices; i < upgradeDisplays.Count(); i++)
        {
            upgradeDisplays[i].gameObject.SetActive(false);
        }
        shopCanvas.SetActive(true);

    }

    public void DeactiveShop()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        shopCanvas.SetActive(false);
    }

    public bool CheckPrice(float cost)
    {
        return CoinManager.Instance.CurrentCoins >= cost;
    }
    
    public void CheckCard(BaseUpgrade upgrade)
    {
        CoinManager.Instance.TakeCoins(upgrade.cost);
        if (upgrade.CheckMaxLevel())
        {
            upgrades.Remove(upgrade);
        }
    }
}
