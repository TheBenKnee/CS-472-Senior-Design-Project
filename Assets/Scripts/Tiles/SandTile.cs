using UnityEngine;
using UnityEngine.Tilemaps;

public class SandTile : BaseTile
{

    // Method to Get the tile data for the tile
    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {

        int mySituation = DetermineSprite(new WaterTile());

        tileData.sprite = Resources.Load<Sprite>("sprites/tiles/sand/SandWater/sand_water_" + mySituation.ToString());
    }

    public void SetSprite()
    {
        int mySituation = DetermineSprite(new WaterTile());

        sprite = Resources.Load<Sprite>("sprites/tiles/sand/SandWater/sand_water_" + mySituation.ToString());
    }
}



