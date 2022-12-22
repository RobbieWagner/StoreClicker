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
    private ClothingArticle currentClothingArticle;
    private int currentClothingArticleIndex;

    [SerializeField] private CurrencyTracker currencyTracker;

    [SerializeField] private Image currentClothingImage;
    [SerializeField] private Image[] clothingImages;
    [SerializeField] private ClothingColor[] clothingColors;
    [SerializeField] private TextMeshProUGUI clothesText;

    // Start is called before the first frame update
    private void Start()
    {
        clothesRack = JsonUtility.FromJson<ClothingRegistry>(textJson.text);
        NextClothingArticle();
    }

    public void NextClothingArticle() 
    {
        currentClothingArticleIndex = Random.Range(0, clothesRack.clothingArticles.Count);
        if(currentClothingArticle != null)
        {
            currencyTracker.money += currentClothingArticle.price;
        }
        currentClothingArticle = clothesRack.clothingArticles[currentClothingArticleIndex];
        
        currentClothingImage.sprite = FindClothesImage(currentClothingArticle);
        currentClothingImage.color = FindClothesColor(currentClothingArticle);
        if(currentClothingArticle.name == null || currentClothingArticle.name.Equals("")) 
            clothesText.text = currentClothingArticle.color + " " + currentClothingArticle.type;
        else clothesText.text = currentClothingArticle.name;
    }

    public Sprite FindClothesImage(ClothingArticle clothes)
    {
        foreach(Image clothingImage in clothingImages)
        {
            Debug.Log(clothingImage.sprite.name);
            Debug.Log(clothes.name);
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
