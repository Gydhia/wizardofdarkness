using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindArrow : Skill
{
    [Header("Specificities to this skill:")]
    public GameObject arrowPrefab;
    public Transform arrowSpawn;
    public float shootForce = 20f;

    public override void ActivatedSkill()
    {
        arrowSpawn = PlayerStats.Instance.arrowSpawn;
        GameObject arrow = Instantiate(arrowPrefab, arrowSpawn.position, Quaternion.identity);
        Rigidbody rb = arrow.GetComponent<Rigidbody>();
        rb.velocity = Camera.main.transform.forward * shootForce;
        PlayerStats.Instance.activeArrows.Add(arrow.GetComponent<ArrowScript>());
    }
    public void Start()
    {
        
    }
    private void Update()
    {
        
    }
}
