using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

// GridManager class to manage and generate grid of tiles in a scene
public class GridManager : MonoBehaviour
{
    // Static references to the Grid and Tilemap components
    public static Grid grid;
    public static Tilemap tileMap;

    // Constants for the size of a level
    public static List<Level> mapLevels;
    public static readonly int LEVEL_WIDTH = 100;
    public static readonly int LEVEL_HEIGHT = 50;

    // Method to reset grid values for pathfinding
    public static void ResetGrid(int levelNumber)
    {
        // Reset only the level used for pathfinding
        Level level = mapLevels[levelNumber];
        for (int x = level.getXMin(); x < level.getXMax(); x++)
        {
            for (int y = level.getYMin(); y < level.getYMax(); y++)
            {
                BaseTile_VM tile = (BaseTile_VM)GridManager.tileMap.GetTile(new Vector3Int(x, y, 0));
                if (tile == null)
                {
                    Debug.LogError("Tile is null @ GridManager.ResetGrid()");
                    break;
                }

                tile.visited = false;
                tile.distance = -1;
                tile.parent = null;
                tile = null;
            }
        }
    }

    // Method to Get the tile at a specific position in the grid
    public static BaseTile_VM GetTile(Vector3Int position)
    {
        return (BaseTile_VM)tileMap.GetTile(position);
    }

    // Method to create and add a level to the grid
    public static void CreateLevel()
    {
        int xMin, xMax, yMin, yMax;
        if (mapLevels.Count == 0)
        {
            xMin = 0; xMax = LEVEL_WIDTH - 1; yMin = 0; yMax = LEVEL_HEIGHT;
        }
        else
        {
            xMin = mapLevels[mapLevels.Count - 1].getXMax() + 1;
            xMax = xMin + LEVEL_WIDTH - 1;
            yMin = 0;
            yMax = LEVEL_HEIGHT;
        }

        // Add new level to map levels list
        mapLevels.Add(new Level(mapLevels.Count, xMin, xMax, yMin, yMax));

        float sandRadius = Mathf.Min(xMax, yMax) * 0.4f;
        float grassRadius = sandRadius * 0.8f;
        float jaggednessScale = 0.1f;
        Vector2 center = new Vector2(xMax / 2f, yMax / 2f);

        // Set tiles for level
        for (int x = xMin; x < xMax; x++)
        {
            for (int y = yMin; y < yMax; y++)
            {
                Vector3Int position = new Vector3Int(x, y, 0);
                Vector2 currentPos = new Vector2(x, y);

                float distanceFromCenter = Vector2.Distance(currentPos, center);
                float noiseValue = Mathf.PerlinNoise(x * jaggednessScale, y * jaggednessScale);

                // Water tiles on the outer perimeter
                if (distanceFromCenter >= sandRadius + (sandRadius * 0.5f * noiseValue))
                {
                    WaterTile_VM newWaterTile_VM = ScriptableObject.CreateInstance<WaterTile_VM>();
                    tileMap.SetTile(position, newWaterTile_VM);
                    newWaterTile_VM.SetTileData(TileType.WATER, false, null, 0, tileMap.GetCellCenterWorld(position), 0, false, null);
                }
                // Sand tiles in jagged circular portion
                else if (distanceFromCenter < sandRadius + (sandRadius * 0.5f * noiseValue) &&
                        distanceFromCenter >= grassRadius + (grassRadius * 0.5f * noiseValue))
                {
                    SandTile_VM newSandTile_VM = ScriptableObject.CreateInstance<SandTile_VM>();
                    tileMap.SetTile(position, newSandTile_VM);
                    newSandTile_VM.SetTileData(TileType.SAND, false, null, 0, tileMap.GetCellCenterWorld(position), -9, false, null);
                }
                // Grass tiles in smaller jagged circular portion
                else
                {
                    GrassTile_VM newGrassTile_VM = ScriptableObject.CreateInstance<GrassTile_VM>();
                    tileMap.SetTile(position, newGrassTile_VM);
                    newGrassTile_VM.SetTileData(TileType.GRASS, false, null, 0, tileMap.GetCellCenterWorld(position), -9, false, null);
                }
            }
        }

        // Add stairs to upper and lower levels
        if (mapLevels.Count > 1)
        {
            Vector3Int stairsPosition;
            TileType tileType;
            int randomX = UnityEngine.Random.Range(xMin, xMax);
            int randomY = UnityEngine.Random.Range(yMin, yMax);

            // Set stairs in random location in upper level
            Vector3Int upperLevelStairsPosition = new Vector3Int(randomX - LEVEL_WIDTH * (mapLevels.Count - 1), randomY, 0);
            StairsTile_VM upperLevelStairs = ScriptableObject.CreateInstance<StairsTile_VM>();
            tileMap.SetTile(upperLevelStairsPosition, upperLevelStairs);
            upperLevelStairs.SetTileData(TileType.STAIRS, false, null, 0, tileMap.GetCellCenterWorld(upperLevelStairsPosition), -9, false, null);
            mapLevels[mapLevels.Count - 2].AddDescendingStairs_VM(upperLevelStairs);

            // Set stairs in lower level
            Vector3Int lowerLevelStairsPosition = new Vector3Int(randomX, randomY, 0);
            StairsTile_VM lowerLevelStairs = ScriptableObject.CreateInstance<StairsTile_VM>();
            tileMap.SetTile(lowerLevelStairsPosition, lowerLevelStairs);
            lowerLevelStairs.SetTileData(TileType.STAIRS, false, null, 0, tileMap.GetCellCenterWorld(lowerLevelStairsPosition), -9, false, null);
            mapLevels[mapLevels.Count - 1].AddAscendingStairs_VM(lowerLevelStairs);

            // Connect upper and lower level stairs
            upperLevelStairs.setLowerLevelStairs(lowerLevelStairs);
            lowerLevelStairs.setUpperLevelStairs(upperLevelStairs);
        }
    }


    // Method to initialize the GridManager
    public static void InitializeGridManager()
    {
        grid = GameObject.Find("Grid").GetComponent<Grid>();
        tileMap = GameObject.Find("Grid").GetComponent<Tilemap>();

        mapLevels = new List<Level>();
    }

    // Spawns trees on random vacant grass tiles
    // requires GlobalInstance2 (TMPCombined) in scene
    public static void PopulateWithTrees()
    {
        TileBase[] allTiles = tileMap.GetTilesBlock(tileMap.cellBounds);
        foreach (BaseTile_VM tile in allTiles)
        {
            if (tile != null && tile.type == TileType.GRASS && tile.resource == null && Random.Range(0, 10) == 0)
            {
                GameObject treePrefab = Resources.Load<GameObject>("prefabs/items/Tree");
                GameObject treeInstance = UnityEngine.Object.Instantiate(treePrefab, tile.position, Quaternion.identity);
                treeInstance.transform.SetParent(GameObject.Find("GameManager").transform.Find("Objects"));
                tile.SetTileInformation(tile.type, true, treeInstance, tile.resourceCount, tile.position);
            }
        }
    }

    // Spawn a single tree at some random vacant grass tile
    // requires GlobalInstance2 (TMPCombined) in scene
    public static void PopulateWithTree()
    {
        TileBase[] allTiles = tileMap.GetTilesBlock(tileMap.cellBounds);
        int random = Random.Range(0, allTiles.Length);
        for (int i = 0; i < allTiles.Length; i++)
        {
            BaseTile_VM tile = (BaseTile_VM)allTiles[(i + random) % allTiles.Length];
            if (tile != null && tile.type == TileType.GRASS && tile.resource == null)
            {
                GameObject treePrefab = Resources.Load<GameObject>("prefabs/items/Tree");
                GameObject treeInstance = UnityEngine.Object.Instantiate(treePrefab, tile.position, Quaternion.identity);
                treeInstance.transform.SetParent(GameObject.Find("GameManager").transform.Find("Objects"));
                tile.SetTileInformation(tile.type, true, treeInstance, tile.resourceCount, tile.position);
                break;
            }
        }
    }
    
    public static void PopulateWithChest()
    {
        TileBase[] allTiles = tileMap.GetTilesBlock(tileMap.cellBounds);
        int random = Random.Range(0, allTiles.Length);
        for (int i = 0; i < allTiles.Length; i++)
        {
            BaseTile_VM tile = (BaseTile_VM)allTiles[(i + random) % allTiles.Length];
            if (tile != null && tile.type == TileType.GRASS && tile.resource == null)
            {
                GameObject chestPrefab = Resources.Load<GameObject>("prefabs/items/Chest");
                GameObject chestInstance = UnityEngine.Object.Instantiate(chestPrefab, tile.position, Quaternion.identity);
                chestInstance.transform.SetParent(GameObject.Find("GameManager").transform.Find("Objects"));
                tile.SetTileInformation(tile.type, true, chestInstance, tile.resourceCount, tile.position);
                break;
            }
        }
    }

    public static void PopulateWithBushes()
    {
        TileBase[] allTiles = tileMap.GetTilesBlock(tileMap.cellBounds);
        foreach (BaseTile_VM tile in allTiles)
        {
            if (tile != null && tile.type == TileType.GRASS && tile.resource == null && Random.Range(0, 10) == 0)
            {
                GameObject bushPrefab = Resources.Load<GameObject>("prefabs/items/Bush");
                GameObject bushInstance = UnityEngine.Object.Instantiate(bushPrefab, tile.position, Quaternion.identity);
                bushInstance.transform.SetParent(GameObject.Find("GameManager").transform.Find("Objects"));
                tile.SetTileInformation(tile.type, true, bushInstance, tile.resourceCount, tile.position);
            }
        }
    }

    public static void PopulateWithWheat()
    {
        TileBase[] allTiles = tileMap.GetTilesBlock(tileMap.cellBounds);
        foreach (BaseTile_VM tile in allTiles)
        {
            if (tile != null && tile.type == TileType.SAND && tile.resource == null && Random.Range(0, 10) == 0)
            {
                GameObject wheatPrefab = Resources.Load<GameObject>("prefabs/items/Wheat");
                GameObject wheatInstance = UnityEngine.Object.Instantiate(wheatPrefab, tile.position, Quaternion.identity);
                wheatInstance.transform.SetParent(GameObject.Find("GameManager").transform.Find("Objects"));
                tile.SetTileInformation(tile.type, false, wheatInstance, tile.resourceCount, tile.position);
            }
        }
    }

}
