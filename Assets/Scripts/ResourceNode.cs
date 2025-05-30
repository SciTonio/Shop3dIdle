using System.Collections;
using UnityEngine;

public class ResourceNode : MonoBehaviour
{
    [Header("Type d'item")]
    public ResourceDefinition resourceDefinition;

    [Header("Quantité")]
    public int maxQuantity;               // Quantité initiale
    public int currentQuantity;           // Quantité restante

    private void Awake()
    {
        // Initialisation de la quantité actuelle
        currentQuantity = maxQuantity;
    }
}
