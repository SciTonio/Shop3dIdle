using UnityEngine;

public class ClientManager : MonoBehaviour
{
    public int maxClient;
    public GameObject clientPrefab;
    public GameObject instances;
    void Start()
    {
        this.SetInterval(1f, () => UpdateClient());
    }

    void UpdateClient()
    {
        if (instances.transform.childCount < maxClient)
        {
            var n = Instantiate(clientPrefab, instances.transform);
        }
    }
}
