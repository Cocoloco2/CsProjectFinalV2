using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WalkerGenerator : MonoBehaviour
{
    //enum to determine what state the tiles are in the current walker position. Can only be FLOOR, WALL or EMPTY
    public enum Grid
    {
        FLOOR,
        WALL,
        EMPTY
    }


    //Variables
    public Grid[,] gridHandler; //two dimentional array since we are working with x and y values
    public List<WalkerObject> Walkers; //list of the walker class so we can track of all active wlakers and their positions
    public Tilemap tileMap; //reference for tilemap with no collision
    public Tilemap tileMap2; //reference for tilemap with collision
    public Tile Floor; //reference for floor sprite
    public Tile Wall; //reference for wall sprite
    public Tile Background;
    public int MapWidth = 30; 
    public int MapHeight = 30;
    [SerializeField] private ObjectSpawner objectSpawner; //reference to the objectSpawner
    private Vector3Int TileCenter;
    private static float x = 10.0f;
    private static float y = 10.0f;
    private float x1 = x / 5f;
    private float y1 = x / 5f;
    


    public int MaximumWalkers = 10;
    public int TileCount = default; //to count the amount of tiles
    public float FillPercentage = 0.4f; //used to compare amount of floor tiles with percentage of the total grid we want covered
    public float WaitTime = 0.05f; //pause between each succesfull movement


    void Start()
    {
        InitializeGrid();
    }


    //main setup of our grid. Grid dimension and properties setup, so we can create our first walker object
    void InitializeGrid()
    {
        gridHandler = new Grid[MapWidth, MapHeight];

        for (int x = 0; x < gridHandler.GetLength(0); x++) //loops through the grid and makes every spot empty
        {
            for (int y = 0; y < gridHandler.GetLength(1); y++)
            {
                Vector3Int pos = new Vector3Int(x, y, 0);
                tileMap.SetTile(pos, Background);
                gridHandler[x, y] = Grid.EMPTY;
            }
        }

        Walkers = new List<WalkerObject>(); //create new instance of the walker object list

        //gridHandler.GetLength(0) / 2, gridHandler.GetLength(1) / 2
        TileCenter = new Vector3Int(10, 10, 0); //reference to the exact center of tilemap

        //create walkerobject
        WalkerObject curWalker = new WalkerObject(new Vector2(TileCenter.x, TileCenter.y), GetDirection(), 0.5f); 
        gridHandler[TileCenter.x, TileCenter.y] = Grid.FLOOR; //set current grid location to floor
        tileMap.SetTile(TileCenter, Floor);
        Debug.Log(TileCenter.x + " " + TileCenter.y);
        Walkers.Add(curWalker); //add current walker to walkers list

        TileCount++; //increase tilecount

        StartCoroutine(CreateFloors()); //handles all the methods that the walker must follow

    }

    //returns a single vector direction: up, down, left or right
    Vector2 GetDirection()
    {
        int choice = Mathf.FloorToInt(UnityEngine.Random.value * 3.99f);

        switch (choice)
        {
            case 0:
                return Vector2.down;
            case 1:
                return Vector2.left;
            case 2:
                return Vector2.up;
            case 3:
                return Vector2.right;
            default:
                return Vector2.zero;
        }
    }

    IEnumerator CreateFloors()
    {
        //compare the tilecount as a float with the total size of grid, and continue looping until it becomes greater than fill percentage
        while ((float)TileCount / (float)gridHandler.Length < FillPercentage) 
        {
            bool hasCreatedFloor = false; //check if floor is created
            foreach (WalkerObject curWalker in Walkers) //loop thorugh every walker in the list
            {
                Vector3Int curPos = new Vector3Int((int)curWalker.Position.x, (int)curWalker.Position.y, 0); //reference to current position of walker

                if (gridHandler[curPos.x, curPos.y] != Grid.FLOOR) //check if tile is already an existing floor piece
                {
                    tileMap.SetTile(curPos, Floor); //create floor at current position
                    TileCount++; //increase tilecounter
                    gridHandler[curPos.x, curPos.y] = Grid.FLOOR; //flag as tile as FLOOR (enum)
                    hasCreatedFloor = true; //switch boolean
                }
            }

            //Walker Methods
            ChanceToRemove(); //loops thorugh walker list and compares random value with walkerschance. if it is less and walkercount is greater than 1 remove the walker
            ChanceToRedirect(); //calls the get direction method and changes direction of walkers 
            ChanceToCreate(); //can create one more walker if the amount of walkers is not greater than MaximumWalkers value
            UpdatePosition(); //update position of walker, so we know where it is in the grid

            if (hasCreatedFloor)
            {
                yield return new WaitForSeconds(WaitTime);
            }
        }

        StartCoroutine(CreateWalls());
        


    }

    void ChanceToRemove()
    {
        int updatedCount = Walkers.Count;
        for (int i = 0; i < updatedCount; i++)
        {
            if (UnityEngine.Random.value < Walkers[i].ChanceToChange && Walkers.Count > 1)
            {
                Walkers.RemoveAt(i);
                break;
            }
        }
    }

    void ChanceToRedirect()
    {
        for (int i = 0; i < Walkers.Count; i++)
        {
            if (UnityEngine.Random.value < Walkers[i].ChanceToChange)
            {
                WalkerObject curWalker = Walkers[i];
                curWalker.Direction = GetDirection();
                Walkers[i] = curWalker;
            }
        }
    }

    void ChanceToCreate()
    {
        int updatedCount = Walkers.Count;
        for (int i = 0; i < updatedCount; i++)
        {
            if (UnityEngine.Random.value < Walkers[i].ChanceToChange && Walkers.Count < MaximumWalkers)
            {
                Vector2 newDirection = GetDirection(); //new direction for the new walker
                Vector2 newPosition = Walkers[i].Position; //position for the new walker (same as the current walker)

                WalkerObject newWalker = new WalkerObject(newPosition, newDirection, 0.5f); //instantiate new walker
                Walkers.Add(newWalker); //add new walker
            }
        }
    }

    void UpdatePosition()
    {
        for (int i = 0; i < Walkers.Count; i++)
        {
            WalkerObject FoundWalker = Walkers[i];
            FoundWalker.Position += FoundWalker.Direction;
            FoundWalker.Position.x = Mathf.Clamp(FoundWalker.Position.x, 1, gridHandler.GetLength(0) - 2);
            FoundWalker.Position.y = Mathf.Clamp(FoundWalker.Position.y, 1, gridHandler.GetLength(1) - 2);
            Walkers[i] = FoundWalker;
        }
    }

    //creating the walls around the floors
    IEnumerator CreateWalls()
    {
        //loop through our grid. we say '-1' since there needs to be space for the walls
        for (int x = 0; x < gridHandler.GetLength(0) - 1; x++) //loop through our x-values of grid
        {
            for (int y = 0; y < gridHandler.GetLength(1) - 1; y++)  //loop through our y-values of grid
            {
                if (gridHandler[x, y] == Grid.FLOOR) //if the x and value contain a floor: continue
                {
                    bool hasCreatedWall = false;

                    //4 diffrent if statements creating a wall beside the floor tile detected
                    if (gridHandler[x + 1, y] == Grid.EMPTY)
                    {
                        tileMap2.SetTile(new Vector3Int(x + 1, y, 0), Wall);
                        gridHandler[x + 1, y] = Grid.WALL;
                        hasCreatedWall = true;
                    }
                    if (gridHandler[x - 1, y] == Grid.EMPTY)
                    {
                        tileMap2.SetTile(new Vector3Int(x - 1, y, 0), Wall);
                        gridHandler[x - 1, y] = Grid.WALL;
                        hasCreatedWall = true;
                    }
                    if (gridHandler[x, y + 1] == Grid.EMPTY)
                    {
                        tileMap2.SetTile(new Vector3Int(x, y + 1, 0), Wall);
                        gridHandler[x, y + 1] = Grid.WALL;
                        hasCreatedWall = true;
                    }
                    if (gridHandler[x, y - 1] == Grid.EMPTY)
                    {
                        tileMap2.SetTile(new Vector3Int(x, y - 1, 0), Wall);
                        gridHandler[x, y - 1] = Grid.WALL;
                        hasCreatedWall = true;
                    }

                    if (hasCreatedWall)
                    {
                        yield return new WaitForSeconds(WaitTime);
                    }
                }
            }
        }
        //sets the Vector position of the player
        Vector3 pos;
        pos = new Vector3(2f, 2f, 0f);

        //calls the SpawnPlayer method from the ObjectSpawner script
        objectSpawner.SpawnPlayer(pos, transform.rotation);
        

        for (int x = 0; x < gridHandler.GetLength(0); x++) //loop through our x-values of grid
        {
            for (int y = 0; y < gridHandler.GetLength(1); y++)  //loop through our y-values of grid
            {
                if (gridHandler[x, y] == Grid.FLOOR) //if the x and value contain a floor: continue
                {
                    //4 diffrent if statements creating a wall beside the floor tile detected
                    if (gridHandler[x + 1, y] == Grid.WALL && gridHandler[x - 1, y] == Grid.WALL && gridHandler[x, y - 1] == Grid.WALL)
                    {
                        objectSpawner.SpawnEnemy(new Vector2(x/5, y/5), transform.rotation);
                    }

                }
            }
        }
    }
}
