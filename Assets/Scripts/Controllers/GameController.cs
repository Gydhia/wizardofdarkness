using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public delegate void ElementChange();
    public delegate void RoomComplete();
    public delegate void GameReady();
    public delegate void Death();
    public delegate void EnemyDeath();

    public event ElementChange OnElementChange;
    public event RoomComplete OnRoomComplete;
    public event GameReady OnGameReady;
    public event Death OnDeath;
    public event EnemyDeath OnEnemyDeath;

    public bool GameIsReady = false;
    public GameObject ProjectilePool;

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
    public void FireOnElementChange()
    {
        if (OnElementChange != null)
            OnElementChange.Invoke();
    }
    public void FireOnDeath()
    {
        if (OnDeath != null)
            OnDeath.Invoke();
    }
    public void FireOnEnemyDeath()
    {
        if (OnEnemyDeath != null)
            OnEnemyDeath.Invoke();
    }
}
