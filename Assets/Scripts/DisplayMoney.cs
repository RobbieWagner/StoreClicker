using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DisplayMoney : MonoBehaviour
{
    public CurrencyTracker currencyTracker;
    public TextMeshProUGUI currencyText;

    private void Update() 
    {
        currencyText.text = "$" + currencyTracker.money;
    }
}
