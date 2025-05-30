using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StorageManager : MonoBehaviour
{
    public static StorageManager Instance { get; private set; }
    private Dictionary<ItemDefinition, int> craftingStorage = new();
    private Dictionary<ItemDefinition, int> mineStorage = new();
    public Action<Dictionary<ItemDefinition, int>> OnCraftingStorage;
    public Action<Dictionary<ItemDefinition, int>> OnMineStorage;

    public GameObject MineSpot;
    public GameObject CraftingSpot;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void AddCrafting(ItemDefinition item, int quantity = 1)
    {
        if (craftingStorage.ContainsKey(item) == false)
        {
            craftingStorage[item] = 0;
        }

        craftingStorage[item] += quantity;

        OnCraftingStorage?.Invoke(craftingStorage);
    }

    public void AddMine(ItemDefinition item)
    {
        if (mineStorage.ContainsKey(item) == false)
        {
            mineStorage[item] = 0;
        }

        mineStorage[item] += 1;

        OnMineStorage?.Invoke(mineStorage);
    }

    public void RemoveCrafting(ItemDefinition item, int quantity = 1)
    {
        if (craftingStorage.ContainsKey(item) && craftingStorage[item] >= quantity)
        {
            craftingStorage[item] -= quantity;
            if (craftingStorage[item] == 0)
            {
                craftingStorage.Remove(item);
            }
        }

        OnCraftingStorage?.Invoke(craftingStorage);
    }

    public void RemoveMine(ItemDefinition item, int quantity = 1)
    {
        if (mineStorage.ContainsKey(item) && mineStorage[item] >= quantity)
        {
            mineStorage[item] -= quantity;
            if (mineStorage[item] == 0)
            {
                mineStorage.Remove(item);
            }
        }

        OnMineStorage?.Invoke(mineStorage);
    }

    public bool HasCrafting(ItemDefinition item, int quantity = 1)
    {
        return craftingStorage.ContainsKey(item) && craftingStorage[item] >= quantity;
    }

    public bool HasMine(ItemDefinition item, int quantity = 1)
    {
        return mineStorage.ContainsKey(item) && mineStorage[item] >= quantity;
    }

    public bool isProductible(ItemDefinition item)
    {
        var prod = item as ProductDefinition;
        if (prod != null)
        {
            foreach (var ing in prod.ingredients)
            {
                if (HasCrafting(ing.item, ing.amount) == false)
                {
                    return false;
                }
            }

            return true;
        }
        else
        {
            return HasCrafting(item);
        }
    }

    public Vector3 GetMineStorage()
    {
        return MineSpot.transform.position;
    }

    public Vector3 GetCraftingStorage()
    {
        return CraftingSpot.transform.position;
    }

    public void GetProductionItems(ProductDefinition prod)
    {
        foreach (var ing in prod.ingredients)
        {
            RemoveCrafting(ing.item, ing.amount);
        }
    }

    public ItemDefinition GetFirstMineItem()
    {
        if (mineStorage.Count == 0) return null;
        return mineStorage.First().Key;
    }
}
