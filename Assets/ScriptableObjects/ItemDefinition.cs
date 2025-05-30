// Base pour tous les items (ressources et produits)
using UnityEngine;

public abstract class ItemDefinition : ScriptableObject
{
    [Header("Général")]
    public string itemName;    // Nom affiché de l'item
    public Sprite image;
}
