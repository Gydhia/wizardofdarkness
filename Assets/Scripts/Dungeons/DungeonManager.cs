using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public DungeonPart Part;
}

public class DungeonManager : MonoBehaviour
{
    public Dictionary<Orientation, Vector2> Directions = new Dictionary<Orientation, Vector2>() {
        { Orientation.Top, new Vector2(-1, 0) },
        { Orientation.Bottom, new Vector2(1, 0) },
        { Orientation.Left, new Vector2(0, -1) },
        { Orientation.Right, new Vector2(0, 1) },
    };

    public GameObject Corridor;
    public GameObject CorridorsContainer;
    public DungeonParts DungeonParts;
    public Room[,] Rooms;

    public List<DungeonRooms> UnobtainableRooms;
    public List<DungeonRooms> UnlinkableRooms;
    public List<int> NbOfRooms = new List<int>() {
        1, // Spawn
        1, // Boss
        
        1, // Mini-boss

        6, // Mobs
        4, // Agility
        3, // Puzzle
        1, // reward
        3 // Empty
    };

    public int size = 4;
    DungeonSpecification[,] dungeonPath;
    private int maxLinkedRooms = 3;
    private int emptyRooms = 2;

    public Dictionary<DungeonRooms, int> NbOfRoomsType = new Dictionary<DungeonRooms, int>();


    public TextAsset DungeonJSON;

    public static DungeonManager Instance;
    private void Awake()
    {
        // Singleton pattern
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);

        DungeonParts = new DungeonParts();
        Rooms = new Room[size, size];
    }

    public void Start()
    {
        LoadDungeonParts();
        SetupNbOfRooms();
        GenerateDungeonPath();
    }

    private void LoadDungeonParts()
    {
        DungeonParts.dungeonParts = new List<DungeonPart>();

        var partsPreset = Resources.LoadAll("DungeonMechanics", typeof(Room));
        foreach(Room preset in partsPreset) {
            DungeonPart part = new DungeonPart();

            part.Doors = new Dictionary<Orientation, Vector2>();
            for (int i = 0; i < preset.RoomDoors.Count; i++)
               part.Doors.Add(preset.RoomDoors[i].Orientation, preset.RoomDoors[i].Position);

            part.id = preset.RoomID;
            part.Prefab = preset.gameObject;
            part.RoomType = preset.RoomType;
            part.IsDefault = preset.IsDefault;
            
            DungeonParts.dungeonParts.Add(part);
        }
    }

    public void SetupNbOfRooms()
    {
        int index = 0;
        foreach (DungeonRooms room in System.Enum.GetValues(typeof(DungeonRooms))) {
            if (index < System.Enum.GetValues(typeof(DungeonRooms)).Length - 1)
                NbOfRoomsType.Add(room, NbOfRooms[index]);

            index++;
        }
    }

    private void GenerateDungeonPath() // 4x4
    {
        dungeonPath = new DungeonSpecification[size, size];

        for (int i = 0; i < size; i++)
            for (int j = 0; j < size; j++) {
                dungeonPath[i, j].roomType = DungeonRooms.None;
                dungeonPath[i, j].orientations = new List<Orientation>();
            }

        // Spawn room
        Vector2 spawnLocation = GenerateMainRoom(Orientation.Left, DungeonRooms.Spawn, dungeonPath);
        // Boss room 
        Vector2 bossLocation = GenerateMainRoom(Orientation.Right, DungeonRooms.Boss, dungeonPath);

        // Place empty rooms at free spots
        for (int i = 0; i < emptyRooms; i++)
        {
            int x = Random.Range(0, size);
            int y = Random.Range(0, size);

            if (dungeonPath[x, y].roomType != DungeonRooms.None) {
                i--;
            }
            else
            {
                dungeonPath[x, y].roomType = DungeonRooms.Empty;
                dungeonPath[x, y].position.Set((int)x, (int)y);
                NbOfRoomsType[DungeonRooms.Empty]--;
            }
        }

        // Setup the spawn orientations
        SetRoomOrientation(dungeonPath[(int)spawnLocation.x, (int)spawnLocation.y], dungeonPath);
        int pathToBeReward = Random.Range(0, dungeonPath[(int)spawnLocation.x, (int)spawnLocation.y].orientations.Count);

        List<DungeonSpecification> ActualPath = new List<DungeonSpecification>();

        // Variables for creating path
        for (int i = 0; i < 3; i++)
        {
            Vector2 direction = new Vector2(-1, -1);
            Vector2 actualPosition = spawnLocation;
            

            int iteration = 0;
            while (direction != Vector2.zero && iteration < 100)
            {
                DungeonSpecification actualRoom = dungeonPath[(int)actualPosition.x, (int)actualPosition.y];

                direction = GetNextWay(GetRandomAvailableDirections(actualPosition, dungeonPath));

                if (direction == Vector2.zero) {
                    if (i == pathToBeReward) {
                        if (actualRoom.orientations.Count == 1) {
                            NbOfRoomsType[dungeonPath[(int)actualPosition.x, (int)actualPosition.y].roomType]++;
                            actualRoom.roomType = DungeonRooms.Reward;
                            dungeonPath[(int)actualPosition.x, (int)actualPosition.y] = actualRoom;
                        }
                    }
                    continue;
                }
                DungeonSpecification nextRoom = dungeonPath[(int)(actualPosition.x + direction.x), (int)(actualPosition.y + direction.y)];
                nextRoom.position.Set((int)actualPosition.x + direction.x, (int)actualPosition.y + direction.y);
                nextRoom.roomType = GetNextRoom();

                SetRoomPathOrientation(nextRoom, actualRoom, direction);

                dungeonPath[(int)nextRoom.position.x, (int)nextRoom.position.y] = nextRoom;

                actualPosition.Set((int)(nextRoom.position.x), (int)(nextRoom.position.y));
                iteration++;
                ActualPath.Add(nextRoom);
            }
            
            if (iteration >= 99) {
                Debug.LogError("Passed way too much while generating dungeon");
            }
        }

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                if (dungeonPath[i, j].roomType == DungeonRooms.None)
                {
                    dungeonPath[i, j].roomType = GetNextRoom();
                    dungeonPath[i, j].position.Set(i, j);
                    SetRoomOrientation(dungeonPath[i, j], dungeonPath);
                }
            }
        }

        SetRoomOrientation(dungeonPath[(int)bossLocation.x, (int)bossLocation.y], dungeonPath, true);

        string dungeonText = "";
        for (int i = 0; i < size; i++)
        {
            dungeonText += ("\n");
            for (int j = 0; j < size; j++)
            {
                dungeonText += " || " + dungeonPath[i, j].position + " :" + dungeonPath[i, j].roomType;
            }
        }
        Debug.Log(dungeonText);

        StartCoroutine(GenerateDungeonPrefab());
    }

    // PREFAB GENERATOR
    // X = jOffset ; Z = iOffset
    public IEnumerator GenerateDungeonPrefab()
    {
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                if (dungeonPath[i, j].roomType != DungeonRooms.Empty)
                {
                    dungeonPath[i, j].Part = DungeonParts.GetSpecificPart(dungeonPath[i, j].roomType, dungeonPath[i, j].orientations);

                    GameObject part = Instantiate(dungeonPath[i, j].Part.Prefab, this.transform);
                    part.name = dungeonPath[i, j].Part.id + " | " + dungeonPath[i, j].position;
                    Room room = part.GetComponent<Room>();

                    part.transform.position += new Vector3(j * 140f, 0f, i * -140f);

                    room.GivenOrientations = dungeonPath[i, j].orientations;
                    room.EnableDoorFromOrientation(dungeonPath[i, j].orientations);

                    //part.transform.eulerAngles = new Vector3(
                    //    part.transform.eulerAngles.x,
                    //    (float)DungeonParts.GetPartRotation(dungeonPath[i, j].Part, dungeonPath[i, j].orientations),
                    //    part.transform.eulerAngles.z
                    //    );

                    Rooms[i, j] = room;
                    yield return new WaitForSeconds(0.1f);
                }
            }
        }

        for (int k = 0; k < size; k++)
        {
            for (int l = 0; l < size - 1; l++)
            {
                if (dungeonPath[k, l].roomType != DungeonRooms.Empty && dungeonPath[k, l + 1].roomType != DungeonRooms.Empty)
                {
                    float xOffset = 0;
                    if (l < size - 1 && Rooms[k, l + 1] != null)
                    {
                        BooleanDoor nextDoor = Rooms[k, l + 1].RoomDoors.Single(door => door.Orientation == Orientation.Left);
                        BooleanDoor actualDoor = Rooms[k, l].RoomDoors.Single(door => door.Orientation == Orientation.Right);
                        if (Rooms[k, l + 1].GivenOrientations.Contains(Orientation.Left) && Rooms[k, l].GivenOrientations.Contains(Orientation.Right)) {
                            xOffset = actualDoor.WorldPosition.y - nextDoor.WorldPosition.y;
                            Rooms[k, l + 1].gameObject.transform.position += new Vector3(0f, 0f, xOffset);
                        }
                        yield return new WaitForSeconds(0.1f);
                        
                    }
                }
            }
        }

        for (int m = 0; m < size - 1; m++)
        {
            for (int n = 0; n < size; n++)
            {
                if (dungeonPath[m, n].roomType != DungeonRooms.Empty && dungeonPath[m + 1, n].roomType != DungeonRooms.Empty)
                {
                    float yOffset = 0;
                    if (m < size - 1 && Rooms[m + 1, n] != null)
                    {
                        BooleanDoor nextDoor = Rooms[m + 1, n].RoomDoors.Single(door => door.Orientation == Orientation.Top);
                        BooleanDoor actualDoor = Rooms[m, n].RoomDoors.SingleOrDefault(door => door.Orientation == Orientation.Bottom);
                        if (Rooms[m + 1, n].GivenOrientations.Contains(Orientation.Top) && Rooms[m, n].GivenOrientations.Contains(Orientation.Bottom))  {
                            yOffset = actualDoor.WorldPosition.x - nextDoor.WorldPosition.x;
                            Rooms[m + 1, n].gameObject.transform.position += new Vector3(yOffset, 0f, 0f);
                            yield return null;
                            GenerateDungeonCorridors(actualDoor, nextDoor);
                        }
                        yield return new WaitForSeconds(0.1f);
                    }
                    
                }
            }
        }
        for (int o = 0; o < size; o++)
        {
            for (int p = 0; p < size - 1; p++)
            {
                if (dungeonPath[o, p].roomType != DungeonRooms.Empty && dungeonPath[o, p + 1].roomType != DungeonRooms.Empty)
                {
                    if (p < size - 1 && Rooms[o, p + 1] != null)
                    {
                        BooleanDoor nextDoor = Rooms[o, p + 1].RoomDoors.Single(door => door.Orientation == Orientation.Left);
                        BooleanDoor actualDoor = Rooms[o, p].RoomDoors.Single(door => door.Orientation == Orientation.Right);
                        if (Rooms[o, p + 1].GivenOrientations.Contains(Orientation.Left) && Rooms[o, p].GivenOrientations.Contains(Orientation.Right))
                        {
                            GenerateDungeonCorridors(actualDoor, nextDoor);
                            yield return new WaitForSeconds(0.1f);
                        }
                    }
                    yield return null;
                }
            }
        }
    }

    public void GenerateDungeonCorridors(BooleanDoor actualDoor, BooleanDoor nextDoor)
    {
        bool horizontal = Mathf.Abs(actualDoor.WorldPosition.x - nextDoor.WorldPosition.x) > Mathf.Abs(actualDoor.WorldPosition.y - nextDoor.WorldPosition.y);
        float corridorsWayLength = horizontal ? Mathf.Abs(actualDoor.WorldPosition.x - nextDoor.WorldPosition.x) : Mathf.Abs(actualDoor.WorldPosition.y - nextDoor.WorldPosition.y);
        
        MeshFilter[] corridorsFilter = Corridor.GetComponentsInChildren<MeshFilter>();
        bool foundFirst = false;
        Bounds corridorBounds = new Bounds();

        foreach (MeshFilter filter in corridorsFilter)
        {
            if (!foundFirst)
            {
                corridorBounds = filter.sharedMesh.bounds;
                foundFirst = true;
            }
            corridorBounds.Encapsulate(filter.sharedMesh.bounds);
        }
        float corridorLength = 5; //corridorBounds.size.z;

        GameObject corridorContainer = Instantiate(new GameObject(), CorridorsContainer.transform);
        corridorContainer.name = "Corridor";

        for (float i = 0; i < corridorsWayLength; i += corridorLength)
        {
            GameObject corridor = Instantiate(Corridor, Vector3.zero, horizontal ? Quaternion.identity : Quaternion.Euler(0f, 90f, 0f), corridorContainer.transform);
            corridor.transform.position += horizontal ?
                new Vector3(actualDoor.WorldPosition.x + i + corridorLength, 0f, actualDoor.WorldPosition.y - corridorBounds.size.x / 3f) :
                new Vector3(actualDoor.WorldPosition.x - corridorBounds.size.x, 0f, actualDoor.WorldPosition.y - (i + corridorLength));
            corridor.name = actualDoor + " to " + nextDoor;
        }
        corridorContainer.transform.position -= new Vector3(horizontal ? corridorLength / 2 : 0f, 0f, horizontal ? 0f : -corridorLength / 2);

    }

    /// <summary>
    /// Return the next direction that'll follow the path
    /// </summary>
    private List<Orientation?> GetAvailableDirections(Vector2 position, DungeonSpecification[,] dungeon, bool excludeLeft = true)
    {
        List<Orientation?> availableOrientations = new List<Orientation?>();

        
        if (position.y + 1 < size && (dungeon[(int)position.x, (int)position.y + 1].roomType == DungeonRooms.None || !excludeLeft) )
            availableOrientations.Add(Orientation.Right);

        // We don't want the path to be able to go Left
        if (!excludeLeft && position.y - 1 >= 0 && (dungeon[(int)position.x, (int)position.y - 1].roomType == DungeonRooms.None || !excludeLeft))
            availableOrientations.Add(Orientation.Left);

        if (position.x - 1 >= 0 && (dungeon[(int)position.x - 1, (int)position.y].roomType == DungeonRooms.None || !excludeLeft))
            availableOrientations.Add(Orientation.Top);

        if (position.x + 1 < size && (dungeon[(int)position.x + 1, (int)position.y].roomType == DungeonRooms.None || !excludeLeft))
            availableOrientations.Add(Orientation.Bottom);

        return availableOrientations;
    }

    private Orientation? GetRandomAvailableDirections(Vector2 position, DungeonSpecification[,] dungeon) 
    {
        List<Orientation?> orientations = GetAvailableDirections(position, dungeon);

        if (orientations.Count > 0) {
            Orientation? or = orientations[Random.Range(0, orientations.Count)];
            return or;
        }
        else return null;
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
                direction.Set(0, -1);
                break;
            case Orientation.Right:
                direction.Set(0, 1);
                break;
            case Orientation.Top:
                direction.Set(-1, 0);
                break;
            case Orientation.Bottom:
                direction.Set(1, 0);
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
        int iterations = 0;

        while (room == DungeonRooms.None && iterations < 100)
        {
            DungeonRooms randomRoom = (DungeonRooms)rooms.GetValue(Random.Range(0, rooms.Length - 1));
            if (!UnobtainableRooms.Contains(randomRoom) && NbOfRoomsType[randomRoom] > 0) {
                room = randomRoom;
                NbOfRoomsType[randomRoom]--;
            }
            iterations++;
        }
        if (iterations >= 100)
            Debug.LogError("Passed too much to find room");
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

        if (direction != null)
        {
            if (direction == Directions[Orientation.Top]) {
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
    private void SetRoomOrientation(DungeonSpecification room, DungeonSpecification[,] dungeon, bool OnlyOne = false)
    {
        if (room.orientations == null)
            room.orientations = new List<Orientation>();

        List<Orientation?> directions = GetAvailableDirections(room.position, dungeon, false);
        int nbOfLinks = 0, linkedDone = 0;

        if (!OnlyOne) {
            nbOfLinks = Random.Range(1, directions.Count);
        }

        foreach (Orientation or in directions)
        {
            DungeonSpecification adjRoom = dungeon[(int)(room.position.x + Directions[or].x), (int)(room.position.y + Directions[or].y)];
            if (!UnlinkableRooms.Contains(adjRoom.roomType)) {
                room.orientations.Add(or);
                adjRoom.orientations.Add(GetOppositeOrientation(or));
                linkedDone++;

                if (OnlyOne || linkedDone >= nbOfLinks) return;
            }
        }
    }
    private Orientation GetOppositeOrientation(Orientation orientation)
    {
        return orientation switch
        {
            Orientation.Top => Orientation.Bottom,
            Orientation.Bottom => Orientation.Top,
            Orientation.Left => Orientation.Right,
            Orientation.Right => Orientation.Left,
            _ => orientation,
        };
    }

    public string GetShapeFromOrientations(List<Orientation> orientations)
    {
        if (orientations.Count == 0) return null;

        List<Orientation> horizontalList = new List<Orientation> { Orientation.Left, Orientation.Right };
        List<Orientation> verticalList = new List<Orientation> { Orientation.Top, Orientation.Bottom};

        if (orientations.Count == 1)
            return "MonoShape";
        else if (orientations.Count == 3)
            return "TShape";
        else if (orientations.Count == 4)
            return "XShape";
        else if (orientations.Count == 2 && 
            (horizontalList.Contains(orientations[0]) && verticalList.Contains(orientations[1])) ||
            (horizontalList.Contains(orientations[1]) && verticalList.Contains(orientations[0])))
            return "LShape";
        else
            return "IShape";
    }

    private Vector2 GenerateMainRoom(Orientation orientation, DungeonRooms roomType,DungeonSpecification[,] dungeon)
    {
        int column = orientation == Orientation.Left ? 0 : size - 1;

        // [x, 0] is the spawn room
        int roomHeight = Random.Range(0, size);
        dungeon[roomHeight, column].roomType = roomType;
        NbOfRoomsType[roomType]--;
        dungeon[roomHeight, column].position.Set(roomHeight, column);
        
        return new Vector2(roomHeight, column);
    }
}