using System.Collections;
using UnityEngine;

public class Transporter : MonoBehaviour
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
            var item = StorageManager.Instance.GetFirstMineItem();
            if (item != null)
            {
                StorageManager.Instance.RemoveMine(item);
                yield return agent.MoveTo(StorageManager.Instance.GetMineStorage());
                icon.SetImage(item.image);
                icon.Show();
                yield return agent.MoveTo(StorageManager.Instance.GetCraftingStorage());
                StorageManager.Instance.AddCrafting(item);
                icon.Hide();
                continue;
            }

            yield return agent.MoveTo(TransporterManager.Instance.GetIdleSpot());

        }
    }
}
