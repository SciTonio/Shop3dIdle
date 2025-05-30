using EasyButtons;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    public static OrderManager Instance { get; private set; }
    private Queue<(Spot spot, ProductDefinition product)> orderQueue = new ();
    private Queue<ProductDefinition> productionQueue = new();
    private Queue<ResourceDefinition> harvestQueue = new();

    public Action<List<ProductDefinition>> OnOrder;
    public Action<List<ProductDefinition>> OnProduction;
    public Action<List<ResourceDefinition>> OnHarvest;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void AddOrder(Spot spot, (ProductDefinition product, int quantity) order)
    {
        for (int i = 0; i < order.quantity; i++)
        {
            orderQueue.Enqueue((spot, order.product));
            AddProduction(order.product);
            productionQueue.Enqueue(order.product);
        }

        OnOrder?.Invoke(orderQueue.Select(o => o.product).ToList());
        OnProduction?.Invoke(productionQueue.ToList());
        OnHarvest?.Invoke(harvestQueue.ToList());
    }

    private void AddProduction(ProductDefinition product)
    {
        foreach (var ing in product.ingredients)
        {
            for (int j = 0; j < ing.amount; ++j)
            {
                var prod = ing.item as ProductDefinition;
                if (prod != null)
                {
                    AddProduction(prod);
                    productionQueue.Enqueue(prod);
                }
                else
                {
                    harvestQueue.Enqueue(ing.item as ResourceDefinition);
                }
            }
        }
    }

    public Spot GetOrderSpot(ProductDefinition product)
    {
        var peek = orderQueue.Peek();
        if (peek.product == product)
        {
            return peek.spot;
        }

        return null;
    }

    public (Spot spot, ProductDefinition product) GetNextOrder()
    {
        if (orderQueue.Count == 0) return (null, null);

        if (StorageManager.Instance.HasCrafting(orderQueue.Peek().product))
        {
            return orderQueue.Dequeue();
        }

        return (null, null);
    }

    public ProductDefinition GetNextProduction()
    {
        if (productionQueue.Count == 0) return null;

        if (StorageManager.Instance.isProductible(productionQueue.Peek()))
        {
            var prod = productionQueue.Dequeue();
            OnProduction?.Invoke(productionQueue.ToList());
            return prod;
        }

        return null;
    }

    public ResourceDefinition GetHarvest()
    {
        if (harvestQueue.Count == 0) return null;
        var rss = harvestQueue.Dequeue();
        OnHarvest?.Invoke(harvestQueue.ToList());
        return rss;
    }

    public ResourceDefinition GetHarvestPeek()
    {
        if (harvestQueue.Count == 0) return null;
        return harvestQueue.Peek();
    }

    public void ValidOrder()
    {
        OnOrder?.Invoke(orderQueue.Select(o => o.product).ToList());
    }
}
