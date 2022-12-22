using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CurrencyTracker : MonoBehaviour
{
    public float money;
    public TextMeshProUGUI currencyText;

    private void Update() 
    {
        currencyText.text = "$" + money;
    }
}
