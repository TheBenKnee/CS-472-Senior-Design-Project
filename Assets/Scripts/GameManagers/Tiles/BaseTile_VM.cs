using UnityEngine;
using UnityEngine.Tilemaps;

// enum of the different types of tiles
public enum TileType { GENERIC, GRASS, ROCK, WATER, SAND, STAIRS }

public class BaseTile_VM : Tile
{
    public TileType type        { get; private set; }       // type of the tile
    public GameObject resource  { get; private set; }       // resource on the tile
    public int resourceCount    { get; private set; }       // number of resources on the tile
    public Vector3 position     { get; private set; }       // position of the tile in 3D space

    public int distance         { get; set; }               // distance from starting tile
    public bool visited         { get; set; }               // flag to indicate if the tile has been visited
    public bool isCollision     { get; set; }               // flag to indicate if the tile can be collided with
    public BaseTile_VM parent   { get; set; }               // parent tile used in pathfinding

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

    // method to Set the properties of the tile
    public virtual void SetTileData(TileType tileType, bool collision, GameObject resource, int resourceCount, Vector3 position, int distance, bool visited, BaseTile_VM parent)
    {
        type = tileType;
        isCollision = collision;
        this.resource = resource;
        this.resourceCount = resourceCount;
        this.position = position;
        this.distance = distance;
        this.visited = visited;
        this.parent = parent;
    }

    // override of the GetTileData method from the Tile class
    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        // load a generic sprite if no sprite is found
        Sprite genericSprite = Resources.Load<Sprite>("tiles/generic");
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
    public void SetTileInformation(TileType tileType, bool collision, GameObject resource, int resourceCount, Vector3 position)
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