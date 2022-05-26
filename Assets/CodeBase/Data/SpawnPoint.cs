using System;
using UnityEngine;

namespace CodeBase.Data
{
  [Serializable]
  public class SpawnPoint
  {
    public Vector3 Position;
    public Quaternion Rotation;

    public SpawnPoint(Vector3 position, Quaternion rotation)
    {
      Position = position;
      Rotation = rotation;
    }
  }
}