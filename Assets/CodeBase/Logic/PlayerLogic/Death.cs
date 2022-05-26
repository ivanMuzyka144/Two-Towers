using System.Collections;
using System.Collections.Generic;
using CodeBase.Services.SharedData;
using UnityEngine;

public class Death : MonoBehaviour
{
    [SerializeField] private CharacterController _characterController;
    private ISharedDataService _sharedDataService;
    
    public void Construct(ISharedDataService sharedDataService) => 
        _sharedDataService = sharedDataService;

    public void SetNewSpawnPosition(Vector3 spawnPoint) => 
        _sharedDataService.SharedData.PlayerData.SpawnPoint = spawnPoint;

    public void Die()
    {
        _characterController.enabled = false;
        Vector3 spawnPoint = _sharedDataService.SharedData.PlayerData.SpawnPoint;
        transform.position = spawnPoint;
        _characterController.enabled = true;
    }
}
