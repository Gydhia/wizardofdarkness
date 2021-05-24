using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCoroutines : MonoBehaviour
{
    /*Here lies the coroutines we can't put in our skills neither anywhere else, because skills aren't active.*/
    public static PlayerCoroutines Instance;

    private void Awake()
    {
        Instance = this;
    }
    public void LaunchRoutine(IEnumerator cor)
    {
        StartCoroutine(cor);
    }
    /****************************************** VOID ************************************************************/
    //public IEnumerator VoidDash(float warpTime, Dash script)
    //{
    //    //Passage en mode gigatrigger: plus rien ne nous arrête à part les murs (on ignore les collision de quasiment tout)
    //    gameObject.layer = 9;
    //    script.CanLaunch = false;
    //    //Giga movespeed
    //    //StartCoroutine(PlayerController.Instance.StatBuff(warpTime, EStatsDebuffs.MoveSpeed,300));
    //    //Invisible?
    //    yield return new WaitForSeconds(warpTime);
    //    //Annuler tout ça
    //    gameObject.layer = 8;
    //}
    /****************************************** EARTH ***********************************************************/

    //public IEnumerator SwordSwing(float timeToHit, SwordSwing script)
    //{
    //    script.CanLaunch = false;
    //    //PlayerStats.Instance.hammer.SetTrigger("Hit");
    //    yield return new WaitForSeconds(timeToHit);
    //    Vector3 rayOrigin = new Vector3(0.5f, 0.5f, 0f); // center of the screen
    //    float rayLength = script.range;
    //    LayerMask enemy = LayerMask.GetMask("Enemy");
    //    Ray ray = Camera.main.ViewportPointToRay(rayOrigin);
    //    RaycastHit hit;
    //    if (Physics.Raycast(ray, out hit, rayLength, enemy))
    //    {
    //        //hit.collider.gameObject.GetComponent<EnemyStats>().TakeDamage(script.dmg);
    //    }
    //}

    /****************************************** WIND ***********************************************************/
    
}
