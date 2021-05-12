using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicEnemy : EnemyStats
{
    public Animator EnemyAnimator;

    // Aggro attributes
    public NavMeshAgent Agent;
    public float AggroRadius = 50f;
    public bool TriggeredAggro = false;
    public GameObject TargettedDestination;

    private void Start()
    {
        EnemyAnimator = this.GetComponent<Animator>();
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

    public void TriggerAggro(GameObject target)
    {
        TargettedDestination = target;
        TriggeredAggro = true;
        BeginChase();
    }

    public void BeginChase()
    {
        Agent.SetDestination(TargettedDestination.transform.position);
    }
}
