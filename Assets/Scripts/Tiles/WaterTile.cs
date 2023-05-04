using UnityEngine;
using UnityEngine.Tilemaps;

public class WaterTile : BaseTile
{
    // Method to Get the tile data for the tile
    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        int seed = Random.Range(0, 31);

        if(seed < 26)
        {
            tileData.sprite = Resources.Load<Sprite>("sprites/tiles/water/water_0");
        }
        else
        {
            if(seed == 26 || seed == 27)
            {
                tileData.sprite = Resources.Load<Sprite>("sprites/tiles/water/water_wave_0");
            }
            else
            {
                if(seed == 28 || seed == 29)
                {
                    tileData.sprite = Resources.Load<Sprite>("sprites/tiles/water/water_wave_2");
                }
                else
                {
                    if(Random.Range(0, 5) > 3)
                    {
                        tileData.sprite = Resources.Load<Sprite>("sprites/tiles/water/water_wave_1");
                    }
                    else
                    {
                        tileData.sprite = Resources.Load<Sprite>("sprites/tiles/water/water_0");
                    }
                }
            }
        }

        
    }
}



