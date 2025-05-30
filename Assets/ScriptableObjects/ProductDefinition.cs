using UnityEngine;
using System.Collections.Generic;

// Structure pour d�crire une quantit� d'item (ressource ou produit)
[System.Serializable]
public struct ItemAmount
{
    public ItemDefinition item;  // R�f�rence � un ItemDefinition (ResourceDefinition ou ProductDefinition)
    public int amount; // Quantit� requise
}


[CreateAssetMenu(fileName = "Product", menuName = "Production/ProductDefinition")]
public class ProductDefinition : ItemDefinition
{
    [Header("Temps de production")]
    public float craftingTime = 1f;            // Temps de fabrication en secondes

    [Header("Ingr�dients requis")]
    public List<ItemAmount> ingredients;      // Liste d'items n�cessaires

    [Header("Quantit� produite")]
    public int outputQuantity = 1;            // Nombre d'unit�s produites
    // Tu peux ajouter d'autres champs (co�t �nerg�tique, cat�gorie, ic�ne, etc.)
}
