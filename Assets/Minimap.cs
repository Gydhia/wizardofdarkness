using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    [SerializeField]
    private Transform player;

    private void LateUpdate()
    {
        if (DungeonManager.Instance.ActualRoom == null)
        {
            if (DungeonManager.Instance.Rooms.Length > 0)
            {
                DungeonManager.Instance.ActualRoom = DungeonManager.Instance.Rooms[0,0];
            }
            else
            {
                Vector3 newPosition = player.position;
                newPosition.y = transform.position.y;
                transform.position = newPosition;
            }
        }
        else
        {
            Room actualRoom = DungeonManager.Instance.ActualRoom;
            transform.position = new Vector3(actualRoom.RoomBounds.center.x+ actualRoom.transform.position.x, 60, actualRoom.RoomBounds.center.z+ actualRoom.transform.position.z);
        }

    }

}