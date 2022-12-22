using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClothesRack : MonoBehaviour
{
    [System.Serializable]
    public class ClothingArticle
    {
        public string name;
        public string type;
        public string color;
        public float price;
    }

    [System.Serializable]
    public class ClothingRegistry
    {
        public List<ClothingArticle> clothingArticles;
    }

    [System.Serializable]
    public class ClothingColor
    {
        public Color color;
        public string colorName;
    }

    public TextAsset textJson;
    public ClothingRegistry clothesRack = new ClothingRegistry();
    public ClothingRegistry clothesCatalog = new ClothingRegistry();
    private ClothingArticle currentClothingArticle;
    [SerializeField] private ClickTracker clickTracker;
    [HideInInspector] public int goalClicks;
    [SerializeField] private TextMeshProUGUI goalText;
    private int nextUnlockableItem;
    private int currentClothingArticleIndex;

    [SerializeField] private CurrencyTracker currencyTracker;

    [SerializeField] private Image currentClothingImage;
    [SerializeField] private Image[] clothingImages;
    [SerializeField] private ClothingColor[] clothingColors;
    [SerializeField] private TextMeshProUGUI clothesText;

    [HideInInspector] public float cashMultiplier;

    // Start is called before the first frame update
    private void Start()
    {
        clothesCatalog = JsonUtility.FromJson<ClothingRegistry>(textJson.text);
        nextUnlockableItem = 0;
        goalClicks = 10;
        UnlockGarment();
        NextClothingArticle();
        goalText.text = "New item in " + goalClicks + " clicks!";

        cashMultiplier = 1;
    }

    public void NextClothingArticle() 
    {
        CheckForNewClothingItems();
        goalText.text = "New item in " + (goalClicks - clickTracker.clicks) + " clicks!";

        currentClothingArticleIndex = Random.Range(0, clothesRack.clothingArticles.Count);
        if(currentClothingArticle != null)
        {
            currencyTracker.money += currentClothingArticle.price * cashMultiplier;
            currencyTracker.money = (float) (System.Math.Round((double)currencyTracker.money, 2));
        }
        currentClothingArticle = clothesRack.clothingArticles[currentClothingArticleIndex];
        
        currentClothingImage.sprite = FindClothesImage(currentClothingArticle);
        currentClothingImage.color = FindClothesColor(currentClothingArticle);
        if(currentClothingArticle.name == null || currentClothingArticle.name.Equals("")) 
            clothesText.text = currentClothingArticle.color + " " + currentClothingArticle.type;
        else clothesText.text = currentClothingArticle.name;
    }

    private void CheckForNewClothingItems()
    {
        if(clickTracker.clicks >= goalClicks)
        {
            UnlockGarment();
            goalClicks *= 10;
        }
    }

    private void UnlockGarment()
    {
        clothesRack.clothingArticles.Add(clothesCatalog.clothingArticles[nextUnlockableItem]);
        nextUnlockableItem++;
    }

    public Sprite FindClothesImage(ClothingArticle clothes)
    {
        foreach(Image clothingImage in clothingImages)
        {
            //Debug.Log(clothingImage.sprite.name);
            //Debug.Log(clothes.name);
            if(clothingImage.sprite.name.Equals(clothes.type)) return clothingImage.sprite;
        }
        return clothingImages[0].sprite;
    }

    public Color FindClothesColor(ClothingArticle clothes)
    {
        foreach(ClothingColor clothingColor in clothingColors)
        {
            if(clothingColor.colorName.Equals(currentClothingArticle.color)) return clothingColor.color;
        }
        return Color.white;
    }
}
