using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HarvestUI : MonoBehaviour
{
    public TMP_Text text;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        OrderManager.Instance.OnHarvest += (rss) => RefreshHarvest(rss);
    }

    private void RefreshHarvest(List<ResourceDefinition> rss)
    {
        string s = "";
        foreach (var r in rss)
        {
            s += $"{r.itemName}\n";
        }

        text.text = s;
    }
}
