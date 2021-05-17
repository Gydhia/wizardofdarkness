using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void RoomComplete();
public delegate void GameReady();

public class GameController : MonoBehaviour
{
    public event RoomComplete OnRoomComplete;
    public event GameReady OnGameReady;

    public bool GameReady = false;

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
        if (this.OnRoomComplete != null)
            this.OnRoomComplete.Invoke();
    }

    public void FireOnGameReady()
    {
        if (this.OnGameReady != null)
            this.OnGameReady.Invoke();
    }
}
