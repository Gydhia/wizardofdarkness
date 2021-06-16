using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MovementSkill : Skill
{
    public bool IsTeleported;

    [ConditionalField("IsTeleported")]
    public ParticleSystem TeleportAnchor;
    private ParticleSystem _teleportAnchor;
    [ConditionalField("IsTeleported")]
    public float TeleportDistance;
    [ConditionalField("IsTeleported")]
    public float TeleportTime = 0f;

    [ConditionalField("IsTeleported", true)]
    public bool IsStraight = true;

    [ConditionalField("IsStraight", true)]
    public float DashHeight;
    [ConditionalField("IsStraight", true)]
    public float DashLength;

    public override void ActivatedSkill()
    {
        base.ActivatedSkill();

        ParticleAnchor();
    }

    public void ParticleAnchor()
    {
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, TeleportDistance)) {
            if (_teleportAnchor == null)
                _teleportAnchor = Instantiate(TeleportAnchor, hit.point + new Vector3(0f, 0.2f, 0f), Quaternion.identity, GameController.Instance.ProjectilePool.transform);
            else
                _teleportAnchor.transform.position = hit.point;
        } else {
            return;
        }
        _teleportAnchor.Play();
        StartCoroutine(TeleportEntity());
    }
    public IEnumerator TeleportEntity()
    {
        yield return new WaitForSeconds(TeleportTime);

        _teleportAnchor.Stop();
        _teleportAnchor.Clear();

        PlayerController.Instance.PlayerMovement.Teleport(_teleportAnchor.transform.position);
    }

    public void SetupDash()
    {
        List<Vector3> DashPath = MathsFunctions.GetBezierCurve(DashHeight, DashLength);

        
    }
}
