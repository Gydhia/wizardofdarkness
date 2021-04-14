using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dungeon Part", menuName = "WOD/Dungeon Part")]
public class DungeonPartPreset : ScriptableObject
{
    public GameObject Prefab;

    public string RoomID;
    public DungeonRooms RoomType;
    public string RoomShape;

    public List<Vector2> doorsPositions = new List<Vector2>();
    public List<Orientation> doorsOrientations = new List<Orientation>();
    public int Width, Height;
}
