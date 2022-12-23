using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClickerHomeScreen : MonoBehaviour
{
    [SerializeField] List<GameObject> clothesRacksGO;
    [SerializeField] List<ClothesRack> clothesRacks;
    [HideInInspector] public List<GameObject> displayedRacks;
    [SerializeField] private Shop shop;

    public List<string> slotsAvailable;

    private void Start() 
    {
        slotsAvailable = new List<string>();
        displayedRacks = new List<GameObject>();
        slotsAvailable.Add("shirts");

        DisplayRacks();
    }

    public void DisplayRacks()
    {
        foreach(ClothesRack clothesRack in clothesRacks)
        {
            foreach(string slotName in slotsAvailable)
            {
                if(clothesRack.requirementForClothesRack.Equals(slotName))
                {
                    GameObject newSlot = Instantiate(clothesRack.gameObject, gameObject.transform);
                    shop.clickTrackers.Add(newSlot.GetComponent<ClothesRack>().clickTracker);
                    displayedRacks.Add(newSlot);
                    clothesRacksGO.Remove(clothesRack.gameObject);
                }
            }
        }

        foreach(GameObject rack in displayedRacks)
        {
            ClothesRack removedRack = rack.GetComponent<ClothesRack>();
            foreach(string slotName in slotsAvailable)
            {
            if(removedRack.requirementForClothesRack.Equals(slotName))
            clothesRacks.Remove(removedRack);
            }
        }

        ReformatClothingRacks();
    }

    public void ReformatClothingRacks()
    {
        for(int i = 0; i < displayedRacks.Count; i++)
        {
            displayedRacks[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(100 + 200 * i, 0);
        }
    }
}
