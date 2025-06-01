using UnityEngine;

public class InstanceManager : MonoBehaviour
{
    public int maxInstance;
    public GameObject prefab;
    public GameObject instances;

    void Start()
    {
        this.SetInterval(1f, () => UpdateInstance());
    }

    void UpdateInstance()
    {
        if (instances.transform.childCount < maxInstance)
        {
            var n = Instantiate(prefab, instances.transform);
        }
    }
}
