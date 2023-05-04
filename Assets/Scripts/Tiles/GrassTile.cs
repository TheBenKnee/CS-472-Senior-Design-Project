using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class GrassTile : BaseTile
{
    // Method to Get the tile data for the tile
    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {

        int mySituation = DetermineSprite(new SandTile());

        tileData.sprite = Resources.Load<Sprite>("sprites/tiles/grass/grass_sand_" + mySituation.ToString());
    }

    public void SetSprite()
    {
        int mySituation = DetermineSprite(new SandTile());

        sprite = Resources.Load<Sprite>("sprites/tiles/grass/grass_sand_" + mySituation.ToString());
    }
}