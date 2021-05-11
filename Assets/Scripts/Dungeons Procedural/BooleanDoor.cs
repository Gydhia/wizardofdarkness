using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BooleanDoor : MonoBehaviour
{
    public Orientation Orientation;
    public Vector2 Position = new Vector2();
    public Vector2 WorldPosition { get => new Vector2(transform.position.x, transform.position.z); }

    public GameObject Door;
    public GameObject Wall;

    MeshRenderer[] Renderers;

    public void SetupPosition()
    {
        try
        {
            Renderers = this.GetComponentsInChildren<MeshRenderer>();
            
            if (Renderers.Length < 2) 
                throw (new Exception());
            
            Door = Renderers[0].gameObject;
            Wall = Renderers[1].gameObject;

            Vector3 pos = Renderers[0].bounds.center;

            Undo.RecordObject(this, "Refreshed positions");
            Position = new Vector2(pos.x, pos.z);
            PrefabUtility.RecordPrefabInstancePropertyModifications(this);
        }
        catch(Exception e)
        {
            Debug.LogError("Boolean Door order : [0] - Door & [1] - Wall\nYou forgot to put one of these as a child\n" + e.Message);
        }
    }
}
