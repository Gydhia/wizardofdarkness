using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DungeonRooms
{
    Spawn,
    Boss,

    Miniboss,

    Mobs,
    Agility,
    Puzzle,
    Reward,
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

public struct DungeonSpecification
{
    public DungeonRooms roomType;
    public List<Orientation> orientations;
    public Vector2 position;
}

public class DungeonManager : MonoBehaviour
{
    public Dictionary<Orientation, Vector2> Directions = new Dictionary<Orientation, Vector2>() {
        { Orientation.Top, new Vector2(0, 1) },
        { Orientation.Bottom, new Vector2(0, -1) },
        { Orientation.Left, new Vector2(-1, 0) },
        { Orientation.Right, new Vector2(1, 0) },
    };

    public int size = 4;
    public DungeonParts dungeonParts;
    private int maxLinkedRooms = 3;
    private int emptyRooms = 3;

    public Dictionary<DungeonRooms, int> NbOfRoomsType = new Dictionary<DungeonRooms, int>();


    public List<int> NbOfRooms = new List<int>() {
        1, // Spawn
        1, // Boss
        
        1, // Mini-boss

        4, // Mobs
        3, // Agility
        2, // Puzzle
        1, // reward
        3 // Empty
    };


    public TextAsset DungeonJSON;

    public static DungeonManager Instance;
    private void Awake()
    {
        // Singleton pattern
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);

        //LoadDungeonParts();
    }

    public void Start()
    {
        SetupNbOfRooms();
        GenerateDungeon();
    }

    //private void LoadDungeonParts()
    //{
    //    DungeonParts parts = JsonUtility.FromJson<DungeonParts>(DungeonJSON.text);

    //    foreach (DungeonPart part in parts.dungeonParts)
    //    {
    //        foreach (Vector2 door in part.doors)
    //        {
    //            if (Mathf.Abs(door.x) == part.width) {
    //                part.doorsOrientation.Add(door.x < 0 ? Orientation.Left : Orientation.Right);
    //            } else {
    //                part.doorsOrientation.Add(door.y < 0 ? Orientation.Bottom : Orientation.Top);
    //            }
    //        }
    //    }
    //    dungeonParts = parts;
    //}

    public void SetupNbOfRooms()
    {
        int index = 0;
        foreach(DungeonRooms room in System.Enum.GetValues(typeof(DungeonRooms))) {
            if(index < System.Enum.GetValues(typeof(DungeonRooms)).Length - 1)
                NbOfRoomsType.Add(room, NbOfRooms[index]);

            index++;
        }
    }

    private void GenerateDungeon() // 4x4
    {
        DungeonSpecification[,] dungeonPath = new DungeonSpecification[size, size];

        for (int i = 0; i < size; i++)
            for (int j = 0; j < size; j++) {
                dungeonPath[i, j].roomType = DungeonRooms.None;
                dungeonPath[i, j].orientations = new List<Orientation>();
            }


        DungeonPart[,] dungeon = new DungeonPart[size, size];
        Vector2 spawnLocation = new Vector2();
        Vector2 bossLocation = new Vector2();

        int remainingRooms = size * size - emptyRooms;

        // [x, 0] is the spawn room
        int spawnHeight = Random.Range(0, size);
        dungeonPath[spawnHeight, 0].roomType = DungeonRooms.Spawn;
        NbOfRoomsType[DungeonRooms.Spawn]--;
        dungeonPath[spawnHeight, 0].position.Set(0, spawnHeight);
        spawnLocation.Set(0, spawnHeight);

        // [x, size] is the boss room
        int bossHeight = Random.Range(0, size);
        dungeonPath[size - 1, bossHeight].roomType = DungeonRooms.Boss;
        NbOfRoomsType[DungeonRooms.Boss]--;
        dungeonPath[size - 1, bossHeight].position.Set(0, bossHeight);
        bossLocation.Set(size, bossHeight);

        // Place empty rooms at free spots
        for (int i = 0; i < emptyRooms; i++)
        {
            int x = Random.Range(0, size);
            int y = Random.Range(0, size);

            if (dungeonPath[x, y].roomType == DungeonRooms.Spawn)
                i--;
            else {
                dungeonPath[x, y].roomType = DungeonRooms.Empty;
                NbOfRoomsType[DungeonRooms.Empty]--;
            }
        }

        // Variables for creating path
        Vector2 direction = new Vector2(-1, -1);
        Vector2 actualPosition = spawnLocation;
        int iteration = 0;
        while (direction !=  Vector2.zero && iteration < 100)
        {
            direction = GetNextWay(GetRandomAvailableDirections(actualPosition, dungeonPath));
            if (direction == Vector2.zero) continue;

            DungeonSpecification actualRoom = dungeonPath[(int)actualPosition.x, (int)actualPosition.y];
            DungeonSpecification nextRoom = dungeonPath[(int)(actualPosition.x + direction.x), (int)(actualPosition.y + direction.y)];
            nextRoom.roomType = GetNextRoom();

            SetRoomPathOrientation(actualRoom, nextRoom, direction);

            actualPosition.Set((int)(actualPosition.x + direction.x), (int)(actualPosition.y + direction.y));
            iteration++;
        }

        if(iteration >= 100){
            Debug.Log("PASSED WAY TOO MUCH");
        }

        for (int i = 0; i < size; i++) {
            for (int j = 0; j < size; j++) {
                if (dungeonPath[i, j].roomType == DungeonRooms.None)
                {
                    dungeonPath[i, j].roomType = GetNextRoom();
                    SetRoomOrientation(dungeonPath[i, j], dungeonPath);
                }
            }
        }

        for (int i = 0; i < size; i++)
        {
            Debug.Log("\n");
            for (int j = 0; j < size; j++)
            {
                Debug.Log(" || " + dungeonPath[i, j].roomType);
            }
        }


    }

    /// <summary>
    /// Return the next direction that'll follow the path
    /// </summary>
    private Orientation? GetRandomAvailableDirections(Vector2 position, DungeonSpecification[,] dungeon)
    {
        List<Orientation> availableOrientations = new List<Orientation>();

        // We don't want the path to be able to go Left
        if (position.x + 1 < size && dungeon[(int)position.x + 1, (int)position.y].roomType != DungeonRooms.None) 
            availableOrientations.Add(Orientation.Right);

        if (position.y + 1 < size && dungeon[(int)position.x, (int)position.y + 1].roomType != DungeonRooms.None)
            availableOrientations.Add(Orientation.Top);

        if (position.y - 1 >= 0 && dungeon[(int)position.x, (int)position.y - 1].roomType != DungeonRooms.None)
            availableOrientations.Add(Orientation.Bottom);

        Orientation? or = null;
        if (availableOrientations.Count > 0) {
            or = availableOrientations[Random.Range(0, availableOrientations.Count)];
        }
        Debug.Log("Position : " + position.x + " | " + position.y + " - Orientation : " + or);
        return or;
    }

    /// <summary>
    /// According to the orientation, return a vector that we'll use in a 2D Array
    /// </summary>
    private Vector2 GetNextWay(Orientation? orientation)
    {
        Vector2 direction = Vector2.zero;

        switch (orientation)
        {
            case Orientation.Left:
                direction.Set(-1, 0);
                break;
            case Orientation.Right:
                direction.Set(1, 0);
                break;
            case Orientation.Top:
                direction.Set(0, 1);
                break;
            case Orientation.Bottom:
                direction.Set(0, -1);
                break;
        }

        return direction;
    }

    /// <summary>
    /// According to the remaining rooms, return randomly one of these
    /// </summary>
    private DungeonRooms GetNextRoom()
    {
        DungeonRooms room = DungeonRooms.None;
        var rooms = System.Enum.GetValues(typeof(DungeonRooms));

        while (room == DungeonRooms.None)
        {
            DungeonRooms randomRoom = (DungeonRooms)rooms.GetValue(Random.Range(0, rooms.Length - 1));
            if(NbOfRoomsType[randomRoom] > 0) {
                room = randomRoom;
            }
        }

        return room;
    }
    
    /// <summary>
    /// Setup the orientation of the room. If we're currently building the path, the <paramref name="previousRoom"/> indicate the previous room of the path.
    /// </summary>
    /// <param name="previousRoom">Previous room of the path. If null, it mean that we're adding the room to the map</param>
    private void SetRoomPathOrientation(DungeonSpecification actualRoom, DungeonSpecification previousRoom, Vector2 direction)
    {
        if (actualRoom.orientations == null)
            actualRoom.orientations = new List<Orientation>();

        if(direction != null)
        {
            if(direction == Directions[Orientation.Top]) {
                actualRoom.orientations.Add(Orientation.Bottom);
                previousRoom.orientations.Add(Orientation.Top);
            }
            else if (direction == Directions[Orientation.Bottom]) {
                actualRoom.orientations.Add(Orientation.Top);
                previousRoom.orientations.Add(Orientation.Bottom);
            }
            else if (direction == Directions[Orientation.Right]) {
                actualRoom.orientations.Add(Orientation.Left);
                previousRoom.orientations.Add(Orientation.Right);
            }
        }
    }
    private void SetRoomOrientation(DungeonSpecification room, DungeonSpecification[,] dungeon)
    {
        if (room.orientations == null)
            room.orientations = new List<Orientation>();

        List<DungeonRooms> unlinkableRooms = new List<DungeonRooms> {
            DungeonRooms.Boss,
            DungeonRooms.Empty,
            DungeonRooms.None
        };

        foreach(KeyValuePair<Orientation, Vector2> key in Directions)
        {
            DungeonSpecification adjRoom = dungeon[(int)(room.position.x + key.Value.x), (int)(room.position.y + key.Value.y)];
            if (!unlinkableRooms.Contains(adjRoom.roomType)) {
                room.orientations.Add(key.Key);
                adjRoom.orientations.Add(GetOppositeOrientation(key.Key));
            }
        }
    }
    private Orientation GetOppositeOrientation(Orientation orientation)
    {
        switch (orientation)
        {
            case Orientation.Top:
                return Orientation.Bottom;
            case Orientation.Bottom:
                return Orientation.Top;
            case Orientation.Left:
                return Orientation.Right;
            case Orientation.Right:
                return Orientation.Left;
            default:
                return orientation;
        }
    }
}

