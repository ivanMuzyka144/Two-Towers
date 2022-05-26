using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Data
{
  public static class DataExtensions
  {
    public static T RandomItem<T>(this List<T> list) => 
      list[Random.Range(0,list.Count)];
    public static Vector3 AddY(this Vector3 vector, float y)
    {
      vector.y = y;
      return vector;
    }
    
    public static string ToJson(this object obj) => 
      JsonUtility.ToJson(obj);

    public static T ToDeserialized<T>(this string json) =>
      JsonUtility.FromJson<T>(json);
  }
}