using UnityEngine;

public interface IAssets : IService
{
    public GameObject Instantiate(string path);

    public GameObject Instantiate(string path, Vector3 at);

    public GameObject Instantiate(string path, Vector3 at, Quaternion rotation);
}