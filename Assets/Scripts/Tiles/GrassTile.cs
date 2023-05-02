using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class GrassTile : BaseTile
{
    // Method to Get the tile data for the tile
    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        tileData.sprite = Resources.Load<Sprite>("sprites/tiles/grass");
    }

    public override void AdjustSprite()
    {
        List<BaseTile> neighbors = base.GetNeighborTiles();
        
        string waterLocations = "";
        foreach(BaseTile baseTile in neighbors)
        {
            
        }
    }

}