using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mannequin : MonoBehaviour
{
    [SerializeField] private List<GameObject> mannequins;
    [SerializeField] private ClickerHomeScreen chs;

    private bool shoes;
    private bool pants;
    private bool shirt;

    [SerializeField] private string shoesName;
    [SerializeField] private string pantsName;
    [SerializeField] private string shirtName;

    private void Start() 
    {
        shoes = false;
        pants = false;
        shirt = false;

        UpdateMannequins();
    }

    public void UpdateMannequins()
    {
        foreach(GameObject rack in chs.displayedRacks) 
        {
            ClothesRack clothesRack = rack.GetComponent<ClothesRack>();
            foreach(ClothesRack.ClothingArticle clothing in clothesRack.clothesRack.clothingArticles)
            {
                if(clothing.name != null)
                {
                    if(clothing.name.Equals(shoesName)) shoes = true;
                    else if(clothing.name.Equals(pantsName)) pants = true;
                    else if(clothing.name.Equals(shirtName)) shirt = true;
                }
            }
        }

        foreach(GameObject mannequin in mannequins) mannequin.SetActive(false);
        if(shoes && pants && shirt) mannequins[7].SetActive(true);
        else if(shirt && pants) mannequins[6].SetActive(true);
        else if(shirt && shoes) mannequins[5].SetActive(true);
        else if(shoes && pants) mannequins[4].SetActive(true);
        else if(shirt) mannequins[3].SetActive(true);
        else if(pants) mannequins[2].SetActive(true);
        else if(shoes) mannequins[1].SetActive(true);
        else mannequins[0].SetActive(true);
    }
}
