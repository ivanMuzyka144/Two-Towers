using CodeBase.Services;
using UnityEngine;

namespace CodeBase.Infrastructure.AssetManagement
{
  public interface IAssetProvider:IService
  {
    GameObject Instantiate(string path);
    GameObject Instantiate(string path, Vector3 at);
    GameObject Instantiate(string path, Vector3 at, Quaternion rotation);
    GameObject Instantiate(string path, Transform parent);
    GameObject Instantiate(GameObject prefab, Vector3 position);
    GameObject Instantiate(GameObject prefab, Vector3 position, Quaternion rotation);
  }
}