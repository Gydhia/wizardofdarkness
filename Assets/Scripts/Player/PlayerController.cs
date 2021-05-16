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

    private void Start()
    {
        this.PlayerStats = this.GetComponent<PlayerStats>();
    }

    void Update()
    {
        if (PlayerStats.Instance.elements.Count != 0 && PlayerStats.Instance.actualEElement != EElements.None)
        {
            if (Input.GetButtonDown("LeftClickSpell"))
            {
                if (PlayerStats.Instance.actualSkills[0].canLaunch)
                {
                    PlayerStats.Instance.actualSkills[0].ActivatedSkill();
                }
            }
            if (Input.GetButtonDown("RightClickSpell"))
            {
                if (PlayerStats.Instance.actualSkills[1].canLaunch)
                {
                    PlayerStats.Instance.actualSkills[1].ActivatedSkill();
                }
            }
            if (Input.GetButtonDown("FirstKeyboardSpell"))
            {
                if (PlayerStats.Instance.actualSkills[2].canLaunch)
                {
                    PlayerStats.Instance.actualSkills[2].ActivatedSkill();
                }
            }
            if (Input.GetButtonDown("SecondKeyboardSpell"))
            {
                if (PlayerStats.Instance.actualSkills[3].canLaunch)
                {
                    PlayerStats.Instance.actualSkills[3].ActivatedSkill();
                }
            }
            if (Input.GetButtonDown("ThirdKeyboardSpell"))
            {
                if (PlayerStats.Instance.actualSkills[4].canLaunch)
                {
                    PlayerStats.Instance.actualSkills[4].ActivatedSkill();
                }
            }
        }

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
                target.TriggerAggro(this.PlayerStats);
                target.gameObject.layer = _triggeredLayer;
            }
        }
    }
}
