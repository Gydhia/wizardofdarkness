using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBallScript : MonoBehaviour
{
    // Start is called before the first frame update
    [HideInInspector] public float ballMoveSpeed;
    [HideInInspector] public float ballGrowSpeed;
    [HideInInspector] public Vector3 maxScale;
    private bool launched;
    private Vector3 scale;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("LeftClickSpell") && !launched)
        {
            //On grow la bouboule...
            if (transform.localScale.x <= maxScale.x)
            {
                scale = transform.localScale;
                scale.x += ballGrowSpeed*Time.deltaTime;
                scale.y += ballGrowSpeed*Time.deltaTime;
                scale.z += ballGrowSpeed*Time.deltaTime;
                transform.localScale = scale;
            }
        }
        else
        {
            //On lache la bouboule
            if (!launched)
            {
                launched = true;
                transform.parent.gameObject.transform.DetachChildren();
                Destroy(gameObject, 10f);
            }
            else
            {
                transform.Translate(Vector3.forward * ballMoveSpeed * Time.deltaTime);
            }
        }
    }
}
