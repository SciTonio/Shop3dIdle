using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Spot
{
    public Spot(Vector3 spot1, Vector3 spot2)
    {
        clientSpot = spot1;
        crafterSpot = spot2;
        reserved = false;
        waitingSeller = false;
        waitingOrder = false;
        client = null;
    }

    public Vector3 clientSpot;
    public Vector3 crafterSpot;
    public bool reserved;
    public bool waitingSeller;
    public bool waitingOrder;
    public Client client;
}

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance { get; private set; }

    public Transform clientsSpots;
    public Transform sellersSpots;
    public List<Spot> spots = new();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        for(int i = 0; i < clientsSpots.childCount; ++i)
        {
            spots.Add(new (clientsSpots.GetChild(i).position, sellersSpots.GetChild(i).position));
        }
    }

    public Spot GetAndReserve(Client client)
    {
        var d = spots.Select(s => s.reserved ? float.MaxValue : Vector3.Distance(s.clientSpot, client.gameObject.transform.position)).ToList();
        var idx = d.FindIndex(s => s == d.Min());
        if (idx == -1)
        {
            return null;
        }

        spots[idx].reserved = true;
        return spots[idx];
    }

    public Spot GetNextClient()
    {
        int idx = spots.FindIndex(o => o.reserved == true && o.waitingSeller == true);
        if (idx == -1)
        {
            return null;
        }

        return spots[idx];
    }

    public void ReleaseSpot(Spot spot)
    {
        spot.client.OnDelivery();
        spot.client = null;
        spot.reserved = false;
        spot.waitingSeller = false;
        spot.waitingOrder = false;
    }
}
