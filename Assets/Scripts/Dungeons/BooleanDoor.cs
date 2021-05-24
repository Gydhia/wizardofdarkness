using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BooleanDoor : MonoBehaviour
{
    public Room LinkedRoom;

    public Orientation Orientation;
    public Vector2 Position = new Vector2();
    public Vector2 WorldPosition { get => new Vector2(transform.position.x, transform.position.z); }
    public float WorldHeight { get => transform.position.y; }
    public Bounds DoorBounds = new Bounds();

    public GameObject Door;
    public GameObject Wall;

    public DoorOpeningSystem DoorComponent;
    public BoxCollider OpeningCollider;

    MeshRenderer[] Renderers;

    public void SetupPosition()
    {
        try
        {
            this.OpeningCollider = this.GetComponent<BoxCollider>();
            Renderers = new MeshRenderer[this.transform.childCount];
            for (int i = 0; i < Renderers.Length; i++)
            {
                if (i > 1) break;
                Renderers[i] = transform.GetChild(i).GetComponent<MeshRenderer>();

            }

            bool foundFirst = false;

            if (Renderers.Length < 2) 
                throw (new Exception());
            
            Door = Renderers[0].gameObject;
            Wall = Renderers[1].gameObject;

            Vector3 pos = Renderers[0].bounds.center;
            
            Undo.RecordObject(this, "Refreshed positions");
            LinkedRoom = transform.parent.transform.parent.GetComponent<Room>();
            DoorComponent = Door.GetComponentInChildren<DoorOpeningSystem>();
            foreach (MeshRenderer rend in Renderers) {
                if (!foundFirst) {
                    DoorBounds = rend.bounds;
                    foundFirst = true;
                }
                DoorBounds.Encapsulate(rend.bounds);
            }
            Position = new Vector2(pos.x, pos.z);
            PrefabUtility.RecordPrefabInstancePropertyModifications(this);
        }
        catch(Exception e)
        {
            Debug.LogError("Boolean Door order : [0] - Door & [1] - Wall\nYou forgot to put one of these as a child\n" + e.Message);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            LinkedRoom.RoomStart();
        }
    }
}
