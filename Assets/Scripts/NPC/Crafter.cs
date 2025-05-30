using System.Collections;
using UnityEngine;

public class Crafter : MonoBehaviour
{
    public string currentProd;

    private NPCIcon icon;
    private NPCAgent agent;

    void Start()
    {
        icon = GetComponentInChildren<NPCIcon>(true);
        agent = GetComponent<NPCAgent>();
        StartCoroutine(doTask());
    }

    private IEnumerator doTask()
    {
        while (true)
        {
            //check production
            var bench = BenchManager.Instance.GetFreeBench();
            if (bench)
            {
                var prod = OrderManager.Instance.GetNextProduction();
                if (prod)
                {
                    currentProd = prod.itemName;

                    //Debug.Log("Get resource in storage");
                    StorageManager.Instance.GetProductionItems(prod);
                    yield return agent.MoveTo(StorageManager.Instance.GetCraftingStorage());

                    yield return agent.MoveTo(bench.transform.position);
                    icon.SetImage(prod.image);
                    icon.Show();
                    icon.StartCompletion(prod.craftingTime);
                    yield return new WaitForSeconds(prod.craftingTime);
                    bench.Free();

                    //drop in storage
                    yield return agent.MoveTo(StorageManager.Instance.GetCraftingStorage());
                    StorageManager.Instance.AddCrafting(prod, prod.outputQuantity);
                    icon.Hide();
                    continue;
                }

                bench.Free();
            }

            yield return agent.MoveTo(CrafterManager.Instance.GetIdleSpot());
        }
    }

}
