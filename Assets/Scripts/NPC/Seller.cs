using System.Collections;
using UnityEngine;

public class Seller : MonoBehaviour
{
    public Sprite question;
    public string currentProd;

    private NPCIcon icon;
    private NPCAgent agent;

    void Start()
    {
        icon = GetComponentInChildren<NPCIcon>(true);
        icon.Hide();
        agent = GetComponent<NPCAgent>();
        StartCoroutine(doTask());
    }

    private IEnumerator doTask()
    {
        while (true)
        {
            var order = OrderManager.Instance.GetNextOrder();
            if (order.spot != null)
            {
                StorageManager.Instance.RemoveCrafting(order.product);
                yield return agent.MoveTo(StorageManager.Instance.GetCraftingStorage());
                icon.SetImage(order.product.image);
                icon.Show();
                yield return agent.MoveTo(order.spot.crafterSpot);
                OrderManager.Instance.ValidOrder();
                order.spot.client.OnDelivery();
                ScoreManager.Instance.Add(order.product.score);
                icon.Hide();
                continue;
            }

            //looking for waiting client
            Spot spot = ShopManager.Instance.GetNextClient();
            if (spot != null)
            {
                spot.waitingSeller = false;
                //go to client spot
                yield return agent.MoveTo(spot.crafterSpot);
                //take an order
                icon.SetImage(question);
                icon.Show();
                icon.StartCompletion(1f, () => icon.Hide());
                yield return new WaitForSeconds(1f);
                OrderManager.Instance.AddOrder(spot, spot.client.AskOrder());
                spot.waitingOrder = true;
                continue;
            }

            yield return agent.MoveTo(SellerManager.Instance.GetIdleSpot());
        }
    }
}
