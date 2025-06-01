using UnityEngine;
using System.Collections.Generic;

// Structure pour décrire une quantité d'item (ressource ou produit)
[System.Serializable]
public struct ItemAmount
{
    public ItemDefinition item;  // Référence à un ItemDefinition (ResourceDefinition ou ProductDefinition)
    public int amount; // Quantité requise
}


[CreateAssetMenu(fileName = "Product", menuName = "Production/ProductDefinition")]
public class ProductDefinition : ItemDefinition
{
    [Header("Temps de production")]
    public float craftingTime = 1f;            // Temps de fabrication en secondes

    [Header("Ingrédients requis")]
    public List<ItemAmount> ingredients;      // Liste d'items nécessaires

    [Header("Quantité produite")]
    public int outputQuantity = 1;            // Nombre d'unités produites

    [Header("Score à la vente")]
    public int score;

}
