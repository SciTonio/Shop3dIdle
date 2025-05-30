using System.Collections;
using UnityEngine;

public class Miner : MonoBehaviour
{
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
            var harvest = OrderManager.Instance.GetHarvestPeek();
            if (harvest)
            {
                var spot = ResourceManager.Instance.GetHarvestSpot(harvest);
                if (spot)
                {
                    harvest = OrderManager.Instance.GetHarvest();
                    //Debug.Log("Move to mining spot");
                    yield return agent.MoveTo(spot.transform.position);
                    icon.SetImage(harvest.image);
                    icon.Show();
                    icon.StartCompletion(harvest.Duration);
                    yield return new WaitForSeconds(harvest.Duration);
                    spot.GetComponent<LockStation>().Free();
                    var pos = StorageManager.Instance.GetMineStorage();
                    yield return agent.MoveTo(pos);
                    StorageManager.Instance.AddMine(harvest);
                    icon.Hide();
                }
                else
                {
                    yield return agent.MoveTo(MinerManager.Instance.GetIdleSpot());
                    continue;
                }
            }
            else
            {
                yield return agent.MoveTo(MinerManager.Instance.GetIdleSpot());
                continue;
            }
        }
    }
}
