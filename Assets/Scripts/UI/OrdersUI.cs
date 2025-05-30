using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OrdersUI : MonoBehaviour
{
    public TMP_Text text;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        OrderManager.Instance.OnOrder += (orders) => RefreshOrders(orders);
    }

    private void RefreshOrders(List<ProductDefinition> orders)
    {
        string s = "";
        foreach (var o in orders)
        {
            s += $"{o.itemName}\n";
        }

        text.text = s;
    }
}
