using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class GrassTile : BaseTile
{
    List<string> tileSpriteLocations = new List<string>{
        ""
    };

    // Method to Get the tile data for the tile
    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        tileData.sprite = Resources.Load<Sprite>("sprites/tiles/grass");
    }

    public override int AdjustSprite()
    {
        int mySituation = DetermineSprite(new SandTile());

        return mySituation;
    }

}