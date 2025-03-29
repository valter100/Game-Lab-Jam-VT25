using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] List<BaseUpgrade> upgrades;
    [SerializeField] GameObject rootUpgrades;
    [SerializeField] int upgradeChoices = 3;
    [SerializeField] GameObject shopCanvas;
    [SerializeField] UpgradeCard[] upgradeDisplays;

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
        ShuffleUpgrades();
        for (int i = 0; i < upgradeChoices; i++)
        {
            UpgradeCard card = upgradeDisplays[i];
            card.UpdateCard(upgrades[i].title, upgrades[i].description, upgrades[i].cost.ToString());
        }
        shopCanvas.SetActive(true);

    }
}
