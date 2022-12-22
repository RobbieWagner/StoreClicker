using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    [System.Serializable]
    public class Upgrade
    {
        public float cost;
        public string type;
        public float value;
    }

    [System.Serializable]
    public class shopRequirement
    {
        public string requirementType;
        public float value;
    }

    public Upgrade[] upgrades;
    private int currentUpgrade;
    public shopRequirement requirement;
    private Button button;

    public CurrencyTracker currencyTracker;
    public ClothesRack clothesRack;

    private void Start()
    {
        currentUpgrade = 0;
        button = gameObject.GetComponent<Button>();

        currencyTracker = GameObject.Find("CurrencyTracker").GetComponent<CurrencyTracker>();
        clothesRack = GameObject.Find("ClothesRack").GetComponent<ClothesRack>();
    }
    public void MakePurhcase()
    {
        if(currencyTracker.money >= upgrades[currentUpgrade].cost)
        {
            currencyTracker.money -= upgrades[currentUpgrade].cost;
            currencyTracker.money = (float) (System.Math.Round((double)currencyTracker.money, 2));
            CompleteUpgrade(upgrades[currentUpgrade]);
            currentUpgrade++;
            if(currentUpgrade == upgrades.Length)
            {
                button.enabled = false;
            }
        }
    }

    private void CompleteUpgrade(Upgrade upgrade)
    {
        if(upgrade.type.Equals("CashMultiplier"))
        {
            clothesRack.cashMultiplier = upgrade.value;
        }
    }
}
