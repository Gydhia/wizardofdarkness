using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float AggroRadius = 200f;
    private int _enemyLayer { get => LayerMask.NameToLayer("Enemy") ; }
    private int _triggeredLayer { get => LayerMask.NameToLayer("TriggeredEnemy"); }
    private int _layerMask { get => 1 << _enemyLayer; }
    private Collider[] _enemiesCollider;
    private float _checkDelay = 0.5f, _actualDelay = 0f;

    public PlayerStats PlayerStats;
    public PlayerMovement PlayerMovement;

    public static PlayerController Instance;

    private void Awake()
    {
        // Singleton pattern
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
    }

    private void Start()
    {
        this.PlayerStats = this.GetComponent<PlayerStats>();
    }

    void Update()
    {
        _actualDelay += Time.deltaTime;
        
        if(_actualDelay >= _checkDelay)
        {
            _enemiesCollider = Physics.OverlapSphere(transform.position, AggroRadius, _layerMask);
            if (_enemiesCollider.Length > 0)
                TriggerEnemiesAggro(_enemiesCollider);
            _actualDelay = 0f;
        }
    }

    public void TriggerEnemiesAggro(Collider[] enemies)
    {
        foreach(Collider enemy in enemies) {
            if(enemy.TryGetComponent(out BasicEnemy target)) {
                if (DungeonManager.Instance.ActualRoom.Enemies.Contains(target)) {
                    target.TriggerAggro(this.PlayerStats);
                    target.gameObject.layer = _triggeredLayer;
                }
            }
        }
    }
}
