using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BooleanDoor : MonoBehaviour
{
    public Orientation Orientation;
    public Vector2 Position = new Vector2();

    public GameObject Door;
    public GameObject Wall;

    public void SetupPosition()
    {
        try
        {
            MeshRenderer[] renderers = this.GetComponentsInChildren<MeshRenderer>();
            
            if (renderers.Length < 2) 
                throw (new Exception());
            
            Door = renderers[0].gameObject;
            Wall = renderers[1].gameObject;

            Vector3 pos = renderers[0].bounds.center;
            Position = new Vector3(pos.x, 0f, pos.z);
        }
        catch(Exception e)
        {
            Debug.LogError("Boolean Door order : [0] - Door & [1] - Wall\nYou forgot to put one of these as a child\n" + e.Message);
        }
    }
}
