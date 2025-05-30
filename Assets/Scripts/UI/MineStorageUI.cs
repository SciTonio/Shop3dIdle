using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MineStorageUI : MonoBehaviour
{
    public TMP_Text text;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StorageManager.Instance.OnMineStorage += (storage) => RefreshStorage(storage);
    }

    private void RefreshStorage(Dictionary<ItemDefinition, int> storage)
    {
        string s = "";
        foreach (var o in storage)
        {
            s += $"{o.Key.itemName} ({o.Value})\n";
        }

        text.text = s;
    }
}
