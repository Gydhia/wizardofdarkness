using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamCollision : MonoBehaviour
{
    public float BeamDamages;

    public float BeamTime;
    public float DamagesTickDelay = 0.1f;
    private float _actualTickDelay = 0f;
    private bool _canHit = true;

    private Coroutine _tickCoroutine;

    private void OnParticleCollision(GameObject other)
    {
        if (!_canHit) return;

        if (other.TryGetComponent(out EntityStat entity)) {
            entity.TakeDamage((int)BeamDamages);
            _tickCoroutine = StartCoroutine(_startTickDelay());
        }

        StartCoroutine(_destroyBeam(BeamTime));
    }

    private IEnumerator _startTickDelay()
    {
        _canHit = false;
        while (_actualTickDelay < DamagesTickDelay) {
            _actualTickDelay += Time.deltaTime;

            yield return null;
        }
        _canHit = true;
    }

    private void OnDestroy()
    {
        if(_tickCoroutine != null)
            StopCoroutine(_tickCoroutine);
    }

    private IEnumerator _destroyBeam(float time)
    {
        yield return new WaitForSeconds(time);

        this.GetComponent<ParticleSystem>().Stop();
        Destroy(this);
    }
}
