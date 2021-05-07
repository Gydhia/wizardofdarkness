using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "New Dungeon Part", menuName = "WOD/Dungeon Part")]
public class DungeonPartPreset : ScriptableObject
{
    public GameObject Prefab;

    public string RoomID;
    public DungeonRooms RoomType;
    public Bounds RoomBounds;
    public string RoomShape;

    public Dictionary<Orientation, Vector2> DoorsPositions = new Dictionary<Orientation, Vector2>();
    public List<Orientation> DoorsOrientations = new List<Orientation>();
    
}
