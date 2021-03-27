using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DungeonRooms
{
    Boss,
    Miniboss,
    Mobs,
    Agility,
    Reward,
    Spawn,
    Empty,

    None
}
public enum Orientation
{
    Top,
    Bottom,
    Left,
    Right
}

public class DungeonManager : MonoBehaviour
{
    public DungeonParts dungeonParts;
    private int maxLinkedRooms = 3;
    private int emptyRooms = 3;

    private int nbOfEnemyRooms = 4;
    private int nbOfPuzzleRooms = 2;
    private int nbOfAgilityRooms = 2;
    private int nbOfBossRooms = 1;

    public TextAsset DungeonJSON;

    public static DungeonManager Instance;
    private void Awake()
    {
        // Singleton pattern
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);

        LoadDungeonParts();
    }

    private void LoadDungeonParts()
    {
        DungeonParts parts = JsonUtility.FromJson<DungeonParts>(DungeonJSON.text);

        foreach(DungeonPart part in parts.dungeonParts)
        {
            foreach(Vector2 door in part.doors)
            {
                if(Mathf.Abs(door.x) == part.width) {
                    part.doorsOrientation.Add(door.x < 0 ? Orientation.Left : Orientation.Right);
                } else {
                    part.doorsOrientation.Add(door.y < 0 ? Orientation.Bottom: Orientation.Top);
                }
            }
        }
        dungeonParts = parts;
    }

    private void GenerateDungeon(int size) // 4x4
    {
        DungeonPart[,] dungeon = new DungeonPart[size, size];
        Vector2 spawnLocation = new Vector2();
        Vector2 bossLocation = new Vector2();

        // [x, 0] is the spawn room
        int spawnHeight = Random.Range(0, size);
        if(spawnHeight == size || spawnHeight == 0) {
            dungeon[spawnHeight, 0] = dungeonParts.GetRandomPartPerDoorsConstraint(DungeonRooms.Spawn, 
                spawnHeight > 0 ? new List<Orientation> { Orientation.Left, Orientation.Bottom } : new List<Orientation> { Orientation.Left, Orientation.Top});
        } else {
            dungeon[spawnHeight, 0] = dungeonParts.GetRandomPartPerDoorsConstraint(DungeonRooms.Spawn, new List<Orientation> { Orientation.Left });
        }
        spawnLocation.Set(0, spawnHeight);

        // [x, size] is the boss room
        int bossHeight = Random.Range(0, size);
        if (bossHeight == size || bossHeight == 0)
        {
            dungeon[bossHeight, 0] = dungeonParts.GetRandomPartPerDoorsConstraint(DungeonRooms.Boss,
                bossHeight > 0 ? new List<Orientation> { Orientation.Left, Orientation.Bottom } : new List<Orientation> { Orientation.Right, Orientation.Top });
        }
        else
        {
            dungeon[bossHeight, 0] = dungeonParts.GetRandomPartPerDoorsConstraint(DungeonRooms.Boss, new List<Orientation> { Orientation.Right });
        }
        bossLocation.Set(size, bossHeight);

        // Place empty rooms at free spots
        for (int i = 0; i < emptyRooms; i++)
        {
            int x = Random.Range(0, size);
            int y = Random.Range(0, size);

            if (dungeon[x, y].roomType == DungeonRooms.Spawn)
                i--;
            else
                dungeon[x, y] = dungeonParts.GetRandomPart(DungeonRooms.Empty);
        }

        

    }

    public List<Orientation> GetDoorsConstraint(DungeonPart part)
    {
        List<Orientation> doorsConstraints = new List<Orientation>();




        return doorsConstraints;
    }
    private void LinkRooms()
    {

    }
    
}
