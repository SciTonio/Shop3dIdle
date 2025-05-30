using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformExtensions
{
    public static List<GameObject> Children(this GameObject Go)
    {
        List<GameObject> list = new();
        for (int i = 0; i < Go.transform.childCount; i++)
        {
            list.Add(Go.transform.GetChild(i).gameObject);
        }
        return list;
    }

    public static List<Transform> Children(this Transform Go)
    {
        List<Transform> list = new();
        for (int i = 0; i < Go.transform.childCount; i++)
        {
            list.Add(Go.transform.GetChild(i));
        }
        return list;
    }


    // M�thode d'extension pour SetParent avec pr�servation de la transformation globale
    public static void SetParentAndPreserveTransform(this Transform child, Transform newParent)
    {
        // Enregistrement des donn�es de transformation globale
        Vector3 position = child.position;
        Quaternion rotation = child.rotation;
        Vector3 scale = child.lossyScale;

        // Configuration du nouveau parent
        child.SetParent(newParent);

        // R�tablissement des transformations globales
        child.position = position;
        child.rotation = rotation;

        // Calcul de l'�chelle ajust�e en fonction du nouveau parent
        if (newParent != null)
        {
            child.localScale = new Vector3(
                scale.x / newParent.lossyScale.x,
                scale.y / newParent.lossyScale.y,
                scale.z / newParent.lossyScale.z
            );
        }
        else
        {
            child.localScale = scale; // Garde la m�me �chelle si aucun parent
        }
    }
}