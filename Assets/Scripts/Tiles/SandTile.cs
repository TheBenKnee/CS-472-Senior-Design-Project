using UnityEngine;
using UnityEngine.Tilemaps;

public class SandTile : BaseTile
{
    TileData myData;

    // Method to Get the tile data for the tile
    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {

        int mySituation = DetermineSprite(new WaterTile());

        // Debug.Log("Situation: " + mySituation);

        tileData.sprite = Resources.Load<Sprite>("sprites/tiles/sand/SandWater/sand_water_" + mySituation.ToString());

        myData = tileData;
        // tileData.sprite = Resources.Load<Sprite>("sprites/tiles/sand");
    }

    public override int AdjustSprite()
    {
        int mySituation = DetermineSprite(new WaterTile());

        return mySituation;

        //return Resources.Load<Sprite>("sprites/tiles/sand/SandWater/sand_water_" + mySituation.ToString());
    }
}



