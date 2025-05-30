using UnityEngine;

public class CrafterManager : MonoBehaviour
{
    public static CrafterManager Instance { get; private set; }
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
