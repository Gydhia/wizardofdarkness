using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Room : MonoBehaviour
{
    public bool IsDefault = false;
    public bool IsComplete = false;
    public bool SpawnAtStart = false;

    public List<BooleanDoor> RoomDoors = new List<BooleanDoor>();
    public List<Animator> RoomDoorsAnim = new List<Animator>();
    public List<Orientation> GivenOrientations = new List<Orientation>();

    public List<Transform> MeleeEnemies;
    public List<Transform> DistanceEnemies;
    public List<Transform> BossEnemies;

    public List<EnemyStats> Enemies = new List<EnemyStats>();

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

    private void Start()
    {
        foreach (BooleanDoor bd in RoomDoors) {
            if(bd.DoorComponent != null)
                bd.DoorComponent.DoorAnim.SetBool("OpenDoor", true);
        }

        if (SpawnAtStart) {
            SpawnEnemies();
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
        foreach (EnemyStats e in Enemies)
            if (!e.IsDead) return;
        GameController.Instance.OnRoomComplete -= RoomComplete;
        foreach (BooleanDoor bd in RoomDoors)
        {
            bd.DoorComponent.DoorAnim.SetBool("OpenDoor", true);
        }
        this.IsComplete = true;
    }
    public void RoomStart()
    {
        DungeonManager.Instance.ActualRoom = this;
        //this.RoomBounds.center.x
        //this.RoomBounds.center.z
        //MapCamPos = new Vector3(DungeonManager.Instance.ActualRoom.RoomBounds.center.x, 30, DungeonManager.Instance.ActualRoom.RoomBounds.center.z);
        if (this.IsComplete)
            return;

        GameController.Instance.OnEnemyDeath += RoomComplete;
        foreach (BooleanDoor bd in RoomDoors)
        {
            Debug.Log("Rooms started");
            bd.OpeningCollider.enabled = false;
            bd.DoorComponent.DoorAnim.SetBool("OpenDoor", false);
        }
    }

    public void SpawnEnemies()
    {
        foreach(Transform t in MeleeEnemies) {
            Enemies.Add(Instantiate(DungeonManager.Instance.MeleeEnemy, t));
        }
        foreach (Transform t in DistanceEnemies)
        {
            Enemies.Add(Instantiate(DungeonManager.Instance.DistanceEnemy, t));
        }
        foreach (Transform t in BossEnemies)
        {
            Enemies.Add(Instantiate(DungeonManager.Instance.BossEnemy, t));
        }
    }
}

