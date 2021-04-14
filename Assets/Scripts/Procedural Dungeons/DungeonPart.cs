using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DungeonPart
{
    public string id;
    
    public GameObject Prefab;
    public string roomShape;
    public DungeonRooms roomType;
    public Dictionary<Orientation, Vector2> Doors;
    public int width, height;
    public Vector2 position;
}
public class DungeonParts : IEnumerable<DungeonPart>
{
    public List<DungeonPart> dungeonParts;

    public DungeonPart GetRandomPart(DungeonRooms type)
    {
        return dungeonParts.Where(room => room.roomType == type).ElementAt(UnityEngine.Random.Range(0, dungeonParts.Where(room => room.roomType == type).Count()));
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
    public DungeonPart GetSpecificPart(DungeonRooms type, string shape)
    {
        return dungeonParts
            .Where(room => room.roomShape == shape)
            .ElementAt(UnityEngine.Random.Range(0, dungeonParts.Where(room => room.roomShape == shape).Count()));
        // .ElementAt(UnityEngine.Random.Range(0, dungeonParts.Where(room => room.roomType == type).Count()));
        // .Where(room => room.roomType == type)
    }

    public IEnumerator<DungeonPart> GetEnumerator()
    {
        foreach (DungeonPart dungeonPart in dungeonParts)
            yield return dungeonPart;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return dungeonParts.GetEnumerator();
    }
}
