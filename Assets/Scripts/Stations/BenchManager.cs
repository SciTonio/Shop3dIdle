using UnityEngine;

public class BenchManager : MonoBehaviour
{
    public static BenchManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public LockStation GetFreeBench()
    {
        foreach (var b in transform.Children())
        {
            var bench = b.GetComponent<LockStation>();
            if (bench.IsFree)
            {
                bench.Reserve();
                return bench;
            }
        }

        return null;
    }
}
