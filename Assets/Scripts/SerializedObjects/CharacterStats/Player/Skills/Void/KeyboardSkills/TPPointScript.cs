using UnityEngine;
using System.Collections;

public class TPPointScript : MonoBehaviour
{
    IEnumerator cor;
    public SummonTeleportPoint skillAccessor;
    // Use this for initialization
    private void OnEnable()
    {
        cor = TPSummoned();
        StartCoroutine(cor);
    }
    public IEnumerator TPSummoned()
    {
        yield return new WaitForSeconds(1.5f);
        Teleport();
    }
    public void Teleport()
    {
        PlayerStats.Instance.transform.position = transform.position;   //problématique: ne se téléporte pas des fois à cause de déplacement.
        StopCoroutine(cor);
        Destroy(gameObject,0.15f);
        skillAccessor.canLaunch = false;
    }
}
