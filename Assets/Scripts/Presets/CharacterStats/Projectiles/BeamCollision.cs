using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamCollision : MonoBehaviour
{
    public float BeamDamages;
    public ParticleSystem BeamParticles;

    public float BeamTime;
    public float DamagesTickDelay = 0.2f;
    private float _actualTickDelay = 0f;
    private bool _canHit = true;

    private Coroutine _tickCoroutine;

    private void OnParticleCollision(GameObject other)
    {
        if (!_canHit) return;

        if (other.TryGetComponent(out EntityStat entity)) {

            Debug.Log("Inflicted damages : " + BeamDamages);
            entity.TakeDamage((int)BeamDamages);
            if(_tickCoroutine == null)
                _tickCoroutine = StartCoroutine(_startTickDelay());
        }

        StartCoroutine(_stopBeam(BeamTime));
    }

    private IEnumerator _startTickDelay()
    {
        _canHit = false;
        _actualTickDelay = 0f;
        while (_actualTickDelay < DamagesTickDelay) {
            _actualTickDelay += Time.deltaTime;

            yield return null;
        }
        _canHit = true;
        _tickCoroutine = null;
    }

    private IEnumerator _stopBeam(float time)
    {
        float timer = 0f;

        while (timer < time)
        {
            this.gameObject.transform.parent.rotation = Camera.main.transform.rotation;

            timer += Time.deltaTime;
            yield return null;
        }
        

        BeamParticles.Stop();
        BeamParticles.Clear();
        BeamParticles.gameObject.SetActive(false);

        if (_tickCoroutine != null) {
            StopCoroutine(_tickCoroutine);
            _tickCoroutine = null;
            _canHit = true;
        }
           
    }
}
