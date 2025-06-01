using EasyButtons;
using Unity.AI.Navigation;
using UnityEngine;

public class PathManager : MonoBehaviour
{
    public static PathManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    [Button] public void UpdatePath()
    {
        NavMeshSurface nm = GetComponent<NavMeshSurface>();
        nm.BuildNavMesh();
    }
}
