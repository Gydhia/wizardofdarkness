using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New DungeonPart", menuName = "WOD/Dungeon Part")]
public class DungeonPartPreset : ScriptableObject
{
    public GameObject Prefab;

    public string RoomID;
    public DungeonRooms RoomType;
    public string RoomShape;
    public List<Orientation> DoorsOrientations;
    public List<Vector2> DoorsPositions;
}
