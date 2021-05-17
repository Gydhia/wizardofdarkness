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
        EnemyAnimator.SetFloat("VerticalSpeed", Agent.velocity.z);
        EnemyAnimator.SetFloat("HorizontalSpeed", Agent.velocity.x);
        EnemyAnimator.SetBool("IsMoving", Agent.velocity.z > 0f || Agent.velocity.x > 0f);
    }

    public void TriggerAggro(PlayerStats target)
    {
        Target = target;
        TriggeredAggro = true;
        StartCoroutine(StartBehaviour());
    }

    public virtual IEnumerator StartBehaviour()
    {
        yield return null;
    }

    public override void TakeDamage(int value)
    {
        base.TakeDamage(value);
        EnemyAnimator.SetTrigger("Hitted");
    }


}
