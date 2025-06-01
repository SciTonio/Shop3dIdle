using EasyButtons;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public List<ResourceDefinition> raw;
    public List<ProductDefinition> products;
    public List<ProductDefinition> activeProducts;

    public static ResourceManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        activeProducts = products.Take(2).ToList();
    }

    public (ProductDefinition product, int quantity) GetProduct()
    {
        var rnd = activeProducts.OrderBy(x => Random.value).ToList();
        return (rnd.First(), 1);
    }

    public ResourceNode GetHarvestSpot(ResourceDefinition rss)
    {
        foreach (var c in transform.Children())
        {
            var def = c.gameObject.GetComponent<ResourceNode>();
            if (def.resourceDefinition == rss && def.GetComponent<LockStation>().IsFree)
            {
                def.GetComponent<LockStation>().Reserve();
                return def;
            }
        }

        return null;
    }

#if UNITY_EDITOR
    [MenuItem("Supercube/Refresh raw ressource list")]
    public static void RefreshEquilibrage()
    {
        Object rssManager = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Resources/ResourceManager.prefab", typeof(GameObject));
        Undo.RecordObject(rssManager, "refresh list");
        (rssManager as GameObject).GetComponent<ResourceManager>().RefreshRawResourcesList();
        (rssManager as GameObject).GetComponent<ResourceManager>().RefreshProductList();
        PrefabUtility.RecordPrefabInstancePropertyModifications(rssManager);
        EditorUtility.SetDirty(rssManager);
        Debug.Log("Refresh completed");
    }

    [Button] public void RefreshRawResourcesList()
    {
        raw.Clear();
        string[] assetNames = AssetDatabase.FindAssets("", new[] { "Assets/ScriptableObjects" });

        foreach (string SOName in assetNames)
        {
            string SOpath = AssetDatabase.GUIDToAssetPath(SOName);
            Debug.Log("SOpath:" + SOpath);
            ResourceDefinition def = AssetDatabase.LoadAssetAtPath<ResourceDefinition>(SOpath);
            if (def != null)
            {
                raw.Add(def);
            }
        }

        //écriture de l'énum
        string filePathAndName = "Assets/Scripts/Enums/EnumRss.cs";
        using (var streamWriter = new StreamWriter(filePathAndName))
        {
            streamWriter.WriteLine("public enum RESOURCES");
            streamWriter.WriteLine("{");
            raw.ForEach(r =>
            {
                streamWriter.WriteLine("\t" + r.name.ToUpper() + ",");
            });

            streamWriter.WriteLine("}");
        }

        AssetDatabase.Refresh();

        EditorUtility.SetDirty(gameObject);
    }

    [Button] public void RefreshProductList()
    {
        products.Clear();
        string[] assetNames = AssetDatabase.FindAssets("", new[] { "Assets/ScriptableObjects" });

        foreach (string SOName in assetNames)
        {
            string SOpath = AssetDatabase.GUIDToAssetPath(SOName);
            Debug.Log("SOpath:" + SOpath);
            ProductDefinition def = AssetDatabase.LoadAssetAtPath<ProductDefinition>(SOpath);
            if (def != null)
            {
                products.Add(def);
            }
        }

        //écriture de l'énum
        string filePathAndName = "Assets/Scripts/Enums/EnumProduct.cs";
        using (var streamWriter = new StreamWriter(filePathAndName))
        {
            streamWriter.WriteLine("public enum PRODUCTS");
            streamWriter.WriteLine("{");
            products.ForEach(p =>
            {
                streamWriter.WriteLine("\t" + p.name.ToUpper() + ",");
            });

            streamWriter.WriteLine("}");
        }

        AssetDatabase.Refresh();

        EditorUtility.SetDirty(gameObject);
    }
#endif
}
