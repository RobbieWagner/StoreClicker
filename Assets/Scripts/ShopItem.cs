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

    public Upgrade[] upgrades;
    private int currentUpgrade;
    private Button button;

    public CurrencyTracker currencyTracker;
    public ClothesRack clothesRack;

    private void Start()
    {
        currentUpgrade = 0;
        button = gameObject.GetComponent<Button>();
    }
    public void MakePurhcase()
    {
        if(currencyTracker.money >= upgrades[currentUpgrade].cost)
        {
            currencyTracker.money -= upgrades[currentUpgrade].cost;
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
