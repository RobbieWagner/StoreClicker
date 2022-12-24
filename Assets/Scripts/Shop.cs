using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{

    [SerializeField] List<GameObject> shopItemsGO;
    [SerializeField] private List<GameObject> displayedItems;
    [SerializeField] List<ShopItem> shopItems;
    List<ShopItem> removedShopItems;

    [SerializeField] ClickerHomeScreen clickerHomeScreen;

    public List<ClickTracker> clickTrackers;

    private void Start() 
    {
        clickTrackers = new List<ClickTracker>();
        removedShopItems = new List<ShopItem>();
    }

    public void CheckForNewPurchases()
    {
        foreach(ShopItem shopItem in shopItems)
        {
            if(shopItem.requirement.requirementType.Equals("clicks"))
            {
                int clicks = 0;
                foreach(ClickTracker clickTracker in clickTrackers){
                    clicks += clickTracker.clicks;
                }

                if(clicks > shopItem.requirement.value)
                {
                    GameObject newItem = Instantiate(shopItem.gameObject, gameObject.transform);
                    AddItemToShop(newItem, shopItem);
                }
            }
            else if(shopItem.requirement.requirementType.Equals("slot"))
            {
                GameObject newItem = null;
                string value = "";

                if(shopItem.requirement.value == 1) value = "pants";
                else if(shopItem.requirement.value == 2) value = "shoes";

                foreach(GameObject rack in clickerHomeScreen.displayedRacks)
                {
                    ClothesRack cr = rack.GetComponent<ClothesRack>();
                    if(cr.name.Equals(value))
                    {
                        newItem = shopItem.gameObject;
                    }
                }
                if(newItem != null)
                {
                    newItem = Instantiate(newItem, gameObject.transform);
                    AddItemToShop(newItem, shopItem);
                }
            }
        }

        foreach(ShopItem removedShopItem in removedShopItems)
        {
            shopItems.Remove(removedShopItem);
        }

        ReformatShopItems();
    }

    private void AddItemToShop(GameObject newItem, ShopItem shopItem)
    {
        displayedItems.Add(newItem);
        removedShopItems.Add(shopItem);
        shopItemsGO.Remove(shopItem.gameObject);
    }

    public void ReformatShopItems()
    {
        for(int i = 0; i < displayedItems.Count; i++)
        {
            displayedItems[i].GetComponent<RectTransform>().anchoredPosition = new Vector2 (400, 400 - (i * 50));
        }
    }
}
