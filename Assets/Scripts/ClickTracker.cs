using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClickTracker : MonoBehaviour
{
    public int clicks;
    [SerializeField] private TextMeshProUGUI clickText;

    private void Start() 
    {
        clicks = 0; 
        clickText.text = "clicks: " + 0;   
    }

    public void AddAClick()
    {
        clicks++;
        clickText.text = "clicks " + clicks;
    }
}
