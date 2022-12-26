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
        public string directedClothingSlot;
        public float value;
    }

    [System.Serializable]
    public class shopRequirement
    {
        public string requirementType;
        public float value;
    }

    public Upgrade[] upgrades;
    [SerializeField] private int currentUpgrade;
    public shopRequirement requirement;
    private Button button;

    public CurrencyTracker currencyTracker;

    public ClickerHomeScreen clickerHomeScreen;

    [SerializeField] private Shop shop;

    private void Start()
    {
        currentUpgrade = 0;
        button = gameObject.GetComponent<Button>();

        currencyTracker = GameObject.Find("CurrencyTracker").GetComponent<CurrencyTracker>();
        clickerHomeScreen = GameObject.Find("ClickerScreen").GetComponent<ClickerHomeScreen>();

        shop = GameObject.Find("UpgradesScreen").GetComponent<Shop>();
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
                shop.displayedItems.Remove(gameObject);
                Destroy(gameObject);
                shop.ReformatShopItems();
            }
        }
    }

    private void CompleteUpgrade(Upgrade upgrade)
    {
        if(upgrade.type.Equals("CashMultiplier"))
        {
            foreach(GameObject rack in clickerHomeScreen.displayedRacks)
            {
                ClothesRack clothesRack = rack.GetComponent<ClothesRack>();
                if(upgrade.directedClothingSlot.Equals(clothesRack.name))
                clothesRack.cashMultiplier = upgrade.value;
            }
        }
        else if(upgrade.type.Equals("Click"))
        {
            foreach(GameObject rack in clickerHomeScreen.displayedRacks)
            {
                ClothesRack clothesRack = rack.GetComponent<ClothesRack>();
                if(upgrade.directedClothingSlot.Equals(clothesRack.name))
                clothesRack.timeToNextClick *= upgrade.value;
            }
        }
        else if(upgrade.type.Equals("AutoClick"))
        {
            foreach(GameObject rack in clickerHomeScreen.displayedRacks)
            {
                ClothesRack clothesRack = rack.GetComponent<ClothesRack>();
                if(upgrade.directedClothingSlot.Equals(clothesRack.name))
                clothesRack.timeToAutoClick = upgrade.value;
            }
        }
        else if(upgrade.type.Equals("PantsSlot"))
        {
            clickerHomeScreen.slotsAvailable.Add("pants");
        }
        else if(upgrade.type.Equals("ShoesSlot"))
        {
            clickerHomeScreen.slotsAvailable.Add("shoes");
        }
    }
}
