using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorChoice : MonoBehaviour
{
    public GameObject door;
    public GameObject wall;

    public void BeWall()
    {
        door.SetActive(false);
        wall.SetActive(true);
    }
    public void BeDoor()
    {
        door.SetActive(true);
        wall.SetActive(false);
    }
}
