using UnityEngine;

public class LockStation : MonoBehaviour
{
    public bool IsFree { get; private set; } = true;

    public void Reserve() => IsFree = false;
    public void Free() => IsFree = true;
}
