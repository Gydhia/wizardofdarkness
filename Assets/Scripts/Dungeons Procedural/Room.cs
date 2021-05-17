using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Room : MonoBehaviour
{
    public bool IsDefault = false;
    public bool IsComplete = false;

    public List<BooleanDoor> RoomDoors = new List<BooleanDoor>();
    public List<Animator> RoomDoorsAnim = new List<Animator>();
    public List<Orientation> GivenOrientations = new List<Orientation>();

    public Bounds RoomBounds = new Bounds();
    public string RoomID;
    public DungeonRooms RoomType;

    public void EnableDoorFromOrientation(List<Orientation> orientations)
    {
        foreach (BooleanDoor door in RoomDoors) {
            door.Wall.SetActive(!orientations.Contains(door.Orientation));
            door.Door.SetActive(orientations.Contains(door.Orientation));
        }
    }
    
    public void SetupDoors()
    {
        this.RoomDoors = this.GetComponentsInChildren<BooleanDoor>().ToList();
        
        foreach (BooleanDoor door in RoomDoors)
            door.SetupPosition();

        bool foundFirst = false;
        Bounds bounds = new Bounds();
        MeshRenderer[] renderers = this.GetComponentsInChildren<MeshRenderer>();

        foreach (MeshRenderer rend in renderers) {
            if (!foundFirst) {
                bounds = rend.bounds;
                foundFirst = true;
            }
            bounds.Encapsulate(rend.bounds);
        }

        RoomBounds = bounds;
    }
    public void RoomComplete()
    {
        GameController.Instance.OnRoomComplete -= RoomComplete;
        foreach (BooleanDoor bd in RoomDoors)
        {
            bd.DoorComponent.DoorAnim.SetBool("OpenDoor", true);
        }
    }
    public void RoomStart()
    {
        GameController.Instance.OnRoomComplete += RoomComplete;
        foreach (BooleanDoor bd in RoomDoors)
        {
            bd.DoorComponent.doorAnim.SetBool("OpenDoor", false);
        }
    }
}

