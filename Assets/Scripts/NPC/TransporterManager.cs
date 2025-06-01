using UnityEngine;

public class TransporterManager : InstanceManager
{
    public static TransporterManager Instance { get; private set; }
    public Transform idleSpot;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public Vector3 GetIdleSpot() => idleSpot.position;
}
