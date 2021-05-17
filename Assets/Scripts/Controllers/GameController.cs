using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void RoomComplete();

public class GameController : MonoBehaviour
{
    public event RoomComplete OnRoomComplete;
    public static GameController Instance;
    private void Awake()
    {
        // Singleton pattern
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
    }
    public void FireOnRoomComplete()
    {
        if (OnRoomComplete != null)
            OnRoomComplete.Invoke();
    }
}
