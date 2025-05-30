using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ProductionsUI : MonoBehaviour
{
    public TMP_Text text;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        OrderManager.Instance.OnProduction += (orders) => RefreshProdcution(orders);
    }

    private void RefreshProdcution(List<ProductDefinition> orders)
    {
        string s = "";
        foreach (var o in orders)
        {
            s += $"{o.itemName}\n";
        }

        text.text = s;
    }
}
