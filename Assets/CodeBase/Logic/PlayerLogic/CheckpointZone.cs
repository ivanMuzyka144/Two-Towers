using System;
using CodeBase.Services.SharedData;
using UnityEngine;

namespace CodeBase.Logic.PlayerLogic
{
  public class CheckpointZone : MonoBehaviour
  {
    
    private bool _activated;
    
    private void OnTriggerEnter(Collider other)
    {
      if (!_activated)
      {
        other.GetComponent<Death>().SetNewSpawnPosition(transform.position);
        _activated = true;
      }
    }
  }
}