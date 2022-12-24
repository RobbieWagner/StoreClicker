using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomerLine : MonoBehaviour
{
    [SerializeField] private GameObject customer;
    [SerializeField] private List<GameObject> customers;
    [SerializeField] private int customerCount;
    [HideInInspector] public bool linePresent = false;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < customerCount; i++)
        {
            AddNewCustomer();
        }

        linePresent = true;
    }

    public void UpdateLine()
    {
        for(int i = 0; i < customers.Count; i++)
        {
            RectTransform pos = customers[i].GetComponent<RectTransform>();
            customers[i].transform.SetSiblingIndex(customerCount - (i+1));
            pos.anchoredPosition += new Vector2(-6, -8);
        }
    }

    public void AddNewCustomer()
    {
        float r = Random.Range(.4f,.8f);
        float g = Random.Range(.4f,.8f);
        float b = Random.Range(.4f,.8f);

        GameObject newCustomer = Instantiate(customer, transform);
        newCustomer.GetComponent<Image>().color = new Color(r,b,g, 1);
        customers.Add(newCustomer);

        UpdateLine();
    }

    public void RemoveAndAdd()
    {
        Destroy(customers[0]);
        customers.RemoveAt(0);

        AddNewCustomer();
    }
}
