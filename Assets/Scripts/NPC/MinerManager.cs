using UnityEngine;

public class MinerManager : MonoBehaviour
{
    public static MinerManager Instance { get; private set; }
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
