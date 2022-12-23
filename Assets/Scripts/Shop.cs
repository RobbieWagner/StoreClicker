using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{

    [SerializeField] List<GameObject> shopItemsGO;
    [SerializeField] private List<GameObject> displayedItems;
    [SerializeField] List<ShopItem> shopItems;

    public List<ClickTracker> clickTrackers;

    private void Start() 
    {
        clickTrackers = new List<ClickTracker>();
    }

    public void CheckForNewPurchases()
    {
        foreach(ShopItem shopItem in shopItems)
        {
            if(shopItem.requirement.requirementType.Equals("clicks"))
            {
                bool clickRequirementMet = false;
                foreach(ClickTracker clickTracker in clickTrackers){
                    if(shopItem.requirement.value <= clickTracker.clicks)
                    {
                        clickRequirementMet = true;
                    }
                }
                if(clickRequirementMet)
                {
                    GameObject newItem = Instantiate(shopItem.gameObject, gameObject.transform);
                    displayedItems.Add(newItem);
                    shopItemsGO.Remove(shopItem.gameObject);
                }
            }
        }

        foreach(GameObject item in displayedItems)
        {
            ShopItem removedItem = item.GetComponent<ShopItem>();
            if(shopItems.Contains(removedItem))
            shopItems.Remove(removedItem);
        }

        ReformatShopItems();
    }

    public void ReformatShopItems()
    {
        for(int i = 0; i < displayedItems.Count; i++)
        {
            displayedItems[i].GetComponent<RectTransform>().anchoredPosition = new Vector2 (400, 400 - (i * 50));
        }
    }
}
