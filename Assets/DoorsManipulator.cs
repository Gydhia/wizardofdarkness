using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DoorsManipulator : MonoBehaviour
{
    List<BooleanDoor> Doors;

    private void Awake()
    {
        Doors = GetComponentsInChildren<BooleanDoor>().ToList();
    }

    public void SetupDoors(List<Orientation> orientations)
    {
        foreach(BooleanDoor door in Doors) {
        
            door.wall.SetActive(!orientations.Contains(door.orientation));
            door.door.SetActive(orientations.Contains(door.orientation));

        }
    }
}
