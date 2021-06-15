using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ArrowProjectile : MonoBehaviour
{
    public Vector3 BasePosition;
    public GameObject Arrow;
    public ParticleSystem RemindParticle;
    public EntityStat LinkedEntity;

    public bool Reminded = false;
    public float DisapearTime = 5f;

    private bool _isLaunched = false;
    private bool _isCalledback = false;
    private float _projectileSpeed = 90f;

    public float PiercingDamages;
    public float CallbackDamages;

    private void Start()
    {
        RemindParticle.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (_isLaunched) {
            if (!_isCalledback) {
                transform.Translate(Vector3.forward * _projectileSpeed * Time.deltaTime);
            }
            else {
                this.transform.rotation = Quaternion.LookRotation(LinkedEntity.transform.position - this.transform.position);
                transform.Translate(Vector3.forward * (_projectileSpeed * 1.5f) * Time.deltaTime);
            }
        }
    }
    public void LaunchProjectile()
    {
        _isLaunched = true;
        if(!Reminded)
            StartCoroutine(DestroyArrow(DisapearTime));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out EntityStat entity)) {
            if (entity != LinkedEntity)
                entity.TakeDamage(_isCalledback ? (int)CallbackDamages : (int)PiercingDamages);
        } 
        else {
            if (_isCalledback){
                StartCoroutine(this.DestroyArrow(0f));
            }
            else if (Reminded) {
                this.Arrow.SetActive(false);
                this.RemindParticle.gameObject.SetActive(true);
                this.RemindParticle.Play();
                this._isLaunched = false;
                StartCoroutine(this.DestroyArrow(DisapearTime));
            }
            else {
                Destroy(this);
            }
        }
    }

    public IEnumerator DestroyArrow(float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);

        Bow bow = PlayerController.Instance.PlayerStats.Elements
            .Single(elem => elem.Type == EElements.Wind).ElementWeapon
            .GetComponent<Bow>();

        bow.RemindedArrows.Remove(this);

        this.RemindParticle.Stop();
        this.RemindParticle.Clear();
        Destroy(this);
    }

    public void CallBackArrow()
    {
        if (!this._isLaunched) {
            _isLaunched = true;
            _isCalledback = true;
        }
    }
}
