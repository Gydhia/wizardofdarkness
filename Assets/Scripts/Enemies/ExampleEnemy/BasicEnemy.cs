using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyType
{
    Distance,
    Melee
}

public class BasicEnemy : EnemyStats
{
    public EnemyType Type;
    private bool IsDead = false;

    // Aggro attributes
    public NavMeshAgent Agent;
    public bool TriggeredAggro = false;
    public PlayerStats Target;
    public float AttackDelay;

    protected void Start()
    {
        base.Start();
        Agent = this.GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        EnemyAnimator.SetFloat("VerticalSpeed", Input.GetAxis("Vertical"));
        EnemyAnimator.SetFloat("HorizontalSpeed", Input.GetAxis("Horizontal"));
    }

    public override IEnumerator Flashing()
    {
        EnemyAnimator.SetTrigger("Hitted");
        base.Flashing();
        yield return null;
    }

    public void TriggerAggro(PlayerStats target)
    {
        Target = target;
        TriggeredAggro = true;
        BeginChase();
        StartCoroutine(StartBehaviour());
    }

    public void BeginChase()
    {
        Agent.SetDestination(Target.transform.position);
    }

    public virtual IEnumerator StartBehaviour()
    {
        yield return null;
    }

    
}
