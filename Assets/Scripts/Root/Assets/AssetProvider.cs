using UnityEngine;

public class AssetProvider : IAssets
{
    public GameObject Instantiate(string path)
    {
        GameObject prefab = Resources.Load<GameObject>(path);
        return Object.Instantiate(prefab);
    }

    public GameObject Instantiate(string path, Vector3 at)
    {
        GameObject prefab = Resources.Load<GameObject>(path);
        return Object.Instantiate(prefab, at, Quaternion.identity);
    }

    public GameObject Instantiate(string path, Vector3 at, Quaternion rotation)
    {
        GameObject heroPrefab = Resources.Load<GameObject>(path);
        return Object.Instantiate(heroPrefab, at, rotation);
    }
}