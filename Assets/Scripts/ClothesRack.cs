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

    public string requirementForClothesRack;
    public TextAsset textJson;
    public ClothingRegistry clothesRack = new ClothingRegistry();
    public ClothingRegistry clothesCatalog = new ClothingRegistry();
    private ClothingArticle currentClothingArticle;
    public ClickTracker clickTracker;
    [HideInInspector] public int goalClicks;
    private int nextUnlockableItem;
    private int currentClothingArticleIndex;

    private CurrencyTracker currencyTracker;

    [SerializeField] private Image currentClothingImage;
    [SerializeField] private Image[] clothingImages;
    [SerializeField] private ClothingColor[] clothingColors;
    [SerializeField] private TextMeshProUGUI clothesText;

    [HideInInspector] public float cashMultiplier;

    public float timeToAutoClick;
    [SerializeField] private Slider autoClickSlider;
    [SerializeField] private Slider goalClickSlider;
    [SerializeField] private Slider clickSlider;

    public float timeToNextClick;
    private bool canClick;

    public string name;

    [SerializeField] CustomerLine customerLine;

    // Start is called before the first frame update
    private void Start()
    {
        currencyTracker = GameObject.Find("CurrencyTracker").GetComponent<CurrencyTracker>();

        clothesCatalog = JsonUtility.FromJson<ClothingRegistry>(textJson.text);
        nextUnlockableItem = 0;

        goalClicks = 10;
        goalClickSlider.minValue = 0;
        goalClickSlider.maxValue = goalClicks;
        goalClickSlider.value = 0;

        timeToNextClick = 1f;
        clickSlider.minValue = 0;
        clickSlider.maxValue = timeToNextClick;
        clickSlider.value = 0;

        canClick = true;

        UnlockGarment();
        NextClothingArticle(true);

        cashMultiplier = 1;

        StartCoroutine(AutoClick());
    }

    public void NextClothingArticle(bool isAutoClick) 
    {
        if(canClick || isAutoClick)
        {
            if(!isAutoClick)
            { 
                canClick = false;
                StartCoroutine(PauseClicker());
            }

            clickTracker.AddAClick();
            
            CheckForNewClothingItems();

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

            if(customerLine.linePresent) customerLine.RemoveAndAdd();
        }
    }

    private void CheckForNewClothingItems()
    {
        if(clickTracker.clicks >= goalClicks && nextUnlockableItem < clothesCatalog.clothingArticles.Count)
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

    public void AddClickToGoal()
    {
        goalClickSlider.value = clickTracker.clicks;
        goalClickSlider.maxValue = goalClicks;
    }

    private IEnumerator AutoClick()
    {
        Debug.Log("Waiting");
        autoClickSlider.minValue = 0;
        autoClickSlider.maxValue = timeToAutoClick;
        Debug.Log(timeToAutoClick);
        autoClickSlider.value = 0;
        yield return null;
        while(autoClickSlider.value < autoClickSlider.maxValue)
        {
            yield return new WaitForSeconds(1f);
            autoClickSlider.value++;
        }
        Debug.Log("AutoClicked");
        clickTracker.AddAClick();
        NextClothingArticle(true);
        StartCoroutine(AutoClick());
    }

    private IEnumerator PauseClicker()
    {
        canClick = false;
        while(clickSlider.value < clickSlider.maxValue)
        {
            yield return new WaitForSeconds(.01f);
            clickSlider.value += .01f;
        }
        clickSlider.value = 0;
        canClick = true;
        StopCoroutine(PauseClicker());
    }
}
