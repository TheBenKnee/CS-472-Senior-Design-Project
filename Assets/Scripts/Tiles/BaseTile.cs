using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

// enum of the different types of tiles
public enum TileType { GENERIC, GRASS, ROCK, WATER, SAND, STAIRS, STONE }

public class BaseTile : Tile
{
    public TileType type { get; protected set; }       // type of the tile
    public Item resource { get; set; }               // resource on the tile
    public int resourceCount { get; protected set; }       // number of resources on the tile
    public Vector3 position { get; set; }               // position of the tile in 3D space
    public int level { get; set; }               // level of the tile

    public int distance { get; set; }               // distance from starting tile
    public bool visited { get; set; }               // flag to indicate if the tile has been visited
    public bool isCollision { get; set; }               // flag to indicate if the tile can be collided with
    public BaseTile parent { get; set; }               // parent tile used in pathfinding

    // method to return the x position of the tile
    public int GetXPosition()
    {
        return (int)position.x;
    }

    // method to return the y position of the tile
    public int GetYPosition()
    {
        return (int)position.y;
    }

    // isAdjacent method to check if the tile is adjacent to another tile
    public bool isAdjacent(BaseTile tile)
    {
        // check if the tile is adjacent to the current tile
        if (Mathf.Abs(tile.GetXPosition() - GetXPosition()) <= 1 && Mathf.Abs(tile.GetYPosition() - GetYPosition()) <= 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public List<BaseTile> GetNeighborTiles()
    {
        List<BaseTile> neighbors = new List<BaseTile>();

        //NW, N, NE, W, E, SW, S, SE

        neighbors.Add((BaseTile)GridManager.tileMap.GetTile(new Vector3Int((int)position.x-1, (int)position.y+1, (int)position.z)));
        neighbors.Add((BaseTile)GridManager.tileMap.GetTile(new Vector3Int((int)position.x, (int)position.y+1, (int)position.z)));
        neighbors.Add((BaseTile)GridManager.tileMap.GetTile(new Vector3Int((int)position.x+1, (int)position.y+1, (int)position.z)));
        neighbors.Add((BaseTile)GridManager.tileMap.GetTile(new Vector3Int((int)position.x-1, (int)position.y, (int)position.z)));
        neighbors.Add((BaseTile)GridManager.tileMap.GetTile(new Vector3Int((int)position.x+1, (int)position.y, (int)position.z)));
        neighbors.Add((BaseTile)GridManager.tileMap.GetTile(new Vector3Int((int)position.x-1, (int)position.y-1, (int)position.z)));
        neighbors.Add((BaseTile)GridManager.tileMap.GetTile(new Vector3Int((int)position.x, (int)position.y-1, (int)position.z)));
        neighbors.Add((BaseTile)GridManager.tileMap.GetTile(new Vector3Int((int)position.x+1, (int)position.y-1, (int)position.z)));

        return neighbors;
    }

    // Base = 0, Up = 1, Down = 2, Right = 3, Left = 4,
    // UpRight = 5, UpLeft = 6, DownRight = 7, DownLeft = 8, 
    // UpRightCorn = 9, UpLeftCorn = 10, DownRightCorn = 11, DownLeftCorn = 12
    public int DetermineSprite(BaseTile checkTileType)
    {
        List<BaseTile> neighbors = GetNeighborTiles();
        //NW, N, NE, W, E, SW, S, SE

        if(neighbors[1] != null && neighbors[1].GetType() == checkTileType.GetType())
        {
            if(neighbors[3] != null && neighbors[3].GetType() == checkTileType.GetType())
            {
                return 6;
            }
            if(neighbors[4] != null && neighbors[4].GetType() == checkTileType.GetType())
            {
                return 5;
            }
            return 1;
        }

        if(neighbors[6] != null && neighbors[6].GetType() == checkTileType.GetType())
        {
            if(neighbors[3] != null && neighbors[3].GetType() == checkTileType.GetType())
            {
                return 8;
            }
            if(neighbors[4] != null && neighbors[4].GetType() == checkTileType.GetType())
            {
                return 7;
            }
            return 2;
        }

        if(neighbors[3] != null && neighbors[3].GetType() == checkTileType.GetType())
        {
            return 4;
        }
        if(neighbors[4] != null && neighbors[4].GetType() == checkTileType.GetType())
        {
            return 3;
        }

        if(neighbors[0] != null && neighbors[0].GetType() == checkTileType.GetType())
        {
            return 10;
        }
        if(neighbors[2] != null && neighbors[2].GetType() == checkTileType.GetType())
        {
            return 9;
        }
        if(neighbors[5] != null && neighbors[5].GetType() == checkTileType.GetType())
        {
            return 12;
        }
        if(neighbors[7] != null && neighbors[7].GetType() == checkTileType.GetType())
        {
            return 11;
        }

        return 0;
    }

    public virtual int AdjustSprite()
    {
        // Adjust texture

        return 0;
    }

    // method to Set the properties of the tile
    public virtual void SetTileData(TileType tileType, bool collision, Item resource, int resourceCount, Vector3 position, int distance, bool visited, BaseTile parent, int level)
    {
        type = tileType;
        isCollision = collision;
        this.resource = resource;
        this.resourceCount = resourceCount;
        this.position = position;
        this.distance = distance;
        this.visited = visited;
        this.parent = parent;
        this.level = level;
    }

    // override of the GetTileData method from the Tile class
    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        // load a generic sprite if no sprite is found
        Sprite genericSprite = Resources.Load<Sprite>("sprites/tiles/generic");
        if (genericSprite == null)
        {
            Debug.LogError("Error loading 'generic' sprite.");
            return;
        }

        // Set the tile sprite to the generic sprite and the tile type to GENERIC
        tileData.sprite = genericSprite;
        type = TileType.GENERIC;
    }

    // override of the RefreshTile method from the Tile class
    public override void RefreshTile(Vector3Int position, ITilemap tilemap)
    {
        // call the base class RefreshTile method
        base.RefreshTile(position, tilemap);
    }

    // method to Set the tile information
    public void SetTileInformation(TileType tileType, bool collision, Item resource, int resourceCount, Vector3 position)
    {
        type = tileType;
        isCollision = collision;
        this.resource = resource;
        this.resourceCount = resourceCount;
        this.position = position;
    }

    // method to return the tile information as a string
    public override string ToString()
    {
        if (resource == null)
        {
            // print the information of the tile include the type, collision, resource, resource count, and position. left align the text using interpolation and evenly space the columns.
            return $"Tile Type: {type,-10} Collision: {isCollision,-10} Position: {position,-50}";
        }
        else
        {
            // print the information of the tile include the type, collision, resource, resource count, and position. left align the text using interpolation and evenly space the columns.
            return $"Tile Type: {type,-10} Collision: {isCollision,-10} Resource: {resource.name,-10} ResourceCount: {resourceCount,-10} Position: {position,-50}";
        }
    }

}



