using System.Collections;
using System.Collections.Generic;
using CodeBase.Logic.Tower;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private RoomPresenter _roomPresenter;

    public RoomPresenter RoomPresenter => _roomPresenter;
    public void Construct(bool isFirst, Color roomColor)
    {
        _roomPresenter.SetupColor(roomColor);
        _roomPresenter.SetupArchitecture(isFirst);
    }
    
    
}