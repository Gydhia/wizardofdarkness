using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DungeonPart
{
    public string id;
    
    public GameObject Prefab;
    public DungeonRooms RoomType;
    public Dictionary<Orientation, Vector2> Doors;
    public int Width, Height;
    public Vector2 Position;
}
public class DungeonParts// : IEnumerable<DungeonPart>
{
    public List<DungeonPart> dungeonParts { get; set; }

    public DungeonPart GetRandomPart(DungeonRooms type)
    {
        return dungeonParts.Where(room => room.RoomType == type).ElementAt(UnityEngine.Random.Range(0, dungeonParts.Where(room => room.RoomType == type).Count()));
    }
    /// <summary>
    /// Return a room that has the doors that we passed in parameter
    /// </summary>
    /// <param name="type">The type of the Room</param>
    /// <param name="orientations">The doors' orientation that will be returned</param>
    //public DungeonPart GetRandomPartPerDoors(DungeonRooms type, List<Orientation> orientations)
    //{
    //    return dungeonParts.Where(room => room.roomType == type)
    //        .Where(room => room.doorsOrientation == orientations)
    //        .ElementAt(UnityEngine.Random.Range(0, dungeonParts.Where(room => room.roomType == type).Count()));
    //}
    ///// <summary>
    ///// Return a room that hasN'T the doors that we passed in parameter
    ///// </summary>
    ///// <param name="type">The type of the Room</param>
    ///// <param name="orientations">The doors' orientation that will not be returned</param>
    //public DungeonPart GetRandomPartPerDoorsConstraint(DungeonRooms type, List<Orientation> orientations)
    //{
    //    return dungeonParts.Where(room => room.roomType == type)
    //        .Where(room => (room.doorsOrientation.Select(orientation => orientations.Contains(orientation)).Contains(true)))
    //        .ElementAt(UnityEngine.Random.Range(0, dungeonParts.Where(room => room.roomType == type).Count()));
    //}

    /// <summary>
    /// Will find a room that respect both doors constraints
    /// </summary>
    /// <param name="constraints">Doors orientation constraints. The first is for the doors that it HAS to have, and the second for the doors that it SHOULDN'T have</param>
    /// <returns></returns>
    public DungeonPart GetSpecificPart(DungeonRooms type, List<Orientation> orientations)
    {
        var parts = dungeonParts.Where(room => (room.Doors.Select(orientation => orientations.Contains(orientation.Key)).Contains(true)));
        return parts.ElementAt(UnityEngine.Random.Range(0, parts.Count()));
        // .ElementAt(UnityEngine.Random.Range(0, dungeonParts.Where(room => room.roomType == type).Count()));
        // .Where(room => room.roomType == type)
    }

    public float? GetPartRotation(DungeonPart part, List<Orientation> orientations)
    {
        float? rotation = 0f;
        // Part orientations different from given orientations to get
        if (part.Doors.Keys.Count() != orientations.Count){
            Debug.LogError("This part (" + part.id + ") doesn't fit the orientations passed");
            return null;
        }
        // Already the good rotation
        if (Enumerable.SequenceEqual(part.Doors.Keys.ToList().OrderBy(e => e), orientations.OrderBy(e => e)))
            return rotation;

        // Turn until we get the right orientations
        List<Orientation> newOrientations = new List<Orientation>();
        for (int i = 1; i < 4; i++) {
            foreach(Orientation or in orientations) {
                newOrientations.Add(RotateOrientation(or));
            }
            rotation += 90f;
            if (Enumerable.SequenceEqual(newOrientations.OrderBy(e => e), orientations.OrderBy(e => e))){
                return rotation;
            }
        }

        return null;
    }

    /// <summary>
    /// Rotate the passed orientation clockwise
    /// </summary>
    public Orientation RotateOrientation(Orientation orientation) {

        //return orientation switch
        //{
        //    Orientation.Top => Orientation.Right,
        //    Orientation.Right => Orientation.Bottom,
        //    Orientation.Bottom => Orientation.Left,
        //    Orientation.Left => Orientation.Top,
        //    _ => orientation,
        //};
        return Orientation.Left;
    }



    public IEnumerator<DungeonPart> GetEnumerator()
    {
        foreach (DungeonPart dungeonPart in dungeonParts)
            yield return dungeonPart;
    }

    //IEnumerator IEnumerable.GetEnumerator()
    //{
      //  return dungeonParts.GetEnumerator();
    //}
}
