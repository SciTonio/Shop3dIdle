using UnityEngine;

// Définition d'une ressource brute
[CreateAssetMenu(fileName = "Resource", menuName = "Production/ResourceDefinition")]
public class ResourceDefinition : ItemDefinition
{
    [Header("Récolte")]
    public float Duration = 2f;  // Durée de récolte par unité
}
