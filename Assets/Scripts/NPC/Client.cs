using EasyButtons;
using System.Collections;
using UnityEngine;

public class Client : MonoBehaviour
{
    private Vector3 origin;
    private bool delivered = false;
    public string product;
    private NPCIcon icon;
    void Start()
    {
        origin = transform.position;
        icon = GetComponentInChildren<NPCIcon>(true);
        StartCoroutine(DoTask());
    }

    public void OnDelivery()
    {
        //give the reward
        delivered = true;
        icon.Hide();
    }

    public (ProductDefinition product, int quantity) AskOrder()
    {
        var p = ResourceManager.Instance.GetProduct();
        icon.SetImage(p.product.image);
        icon.Show();
        product = p.product.itemName;
        return p;
    }

    private IEnumerator DoTask()
    {
        //go to spot
        var target = ShopManager.Instance.GetAndReserve(this);
        yield return GetComponent<NPCAgent>().MoveTo(target.clientSpot);

        target.client = this;
        target.waitingSeller = true;

        //wait order
        while (delivered == false)
        {
            yield return new WaitForSeconds(0.1f);
        }

        //give reward


        //go to exit
        yield return GetComponent<NPCAgent>().MoveTo(origin);
        ShopManager.Instance.ReleaseSpot(target);
        Destroy(gameObject);
    }
}
