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
        PlayerMovement.Instance.move = Vector3.zero;   //problématique: ne se téléporte pas des fois à cause de déplacement.
        PlayerMovement.Instance.transform.position = transform.position;   //problématique: ne se téléporte pas des fois à cause de déplacement.
       // StopCoroutine(cor);
        skillAccessor.canLaunch = false;
        Destroy(gameObject,0.25f);
    }
}
