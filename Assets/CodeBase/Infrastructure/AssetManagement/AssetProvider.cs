using UnityEngine;

namespace CodeBase.Infrastructure.AssetManagement
{
  public class AssetProvider : IAssetProvider
  {
    public GameObject Instantiate(string path)
    {
      var prefab = Resources.Load<GameObject>(path);
      return Object.Instantiate(prefab);
    }

    public GameObject Instantiate(string path, Vector3 at)
    {
      var prefab = Resources.Load<GameObject>(path);
      return Object.Instantiate(prefab, at, Quaternion.identity);
    }
    
    public GameObject Instantiate(string path, Vector3 at, Quaternion rotation)
    {
      var prefab = Resources.Load<GameObject>(path);
      return Object.Instantiate(prefab, at, rotation);
    }
    
    public GameObject Instantiate(string path, Transform parent)
    {
      var prefab = Resources.Load<GameObject>(path);
      return Object.Instantiate(prefab, parent);
    }

    public GameObject Instantiate(GameObject prefabGameObject, Vector3 position) => 
      Object.Instantiate(prefabGameObject, position, Quaternion.identity);
    
    public GameObject Instantiate(GameObject prefabGameObject, Vector3 position, Quaternion rotation) => 
      Object.Instantiate(prefabGameObject, position, rotation);
  }
}