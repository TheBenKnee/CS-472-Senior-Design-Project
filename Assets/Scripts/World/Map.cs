using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    private List<Layer> layers = new List<Layer>();

    [SerializeField] private int worldLength, worldHeight, worldDepth;
    [SerializeField] private GameObject grassTilePrefab;
    [SerializeField] private GameObject oceanTilePrefab;
    [SerializeField] private GameObject walkwayTilePrefab;
    [SerializeField] private GameObject treePrefab;
    [SerializeField] private GameObject pawnPrefab;

    public int GetLayerNumber(Layer layer)
    {
        if(layers.Contains(layer))
        {
            return layers.IndexOf(layer);
        }
        return -1;
    }

    public Layer GetLayer(int layerNumber)
    {
        if(layerNumber >= layers.Count)
        {
            return null;
        }
        return layers[layerNumber];
    }

    public int GetMapDepth()
    {
        return worldDepth;
    }

    private void Start() 
    {
        //TestSpawnWorld();
    }

    // Returns a tile at the specified coordiantes
    public Tile GetTile(int x, int y, int z)
    {
        if(x < worldLength && x > -1 && y < worldHeight && y > -1 && z < worldDepth && z > -1)
        {
            return(layers[z].GetTile(x,y));
        }
        return null;
    }

    [ContextMenu("RaiseZAxis")]
    public void SwitchZAxis()
    {
        //Move camera Constants.LAYER_X_SEPERATION and Constants.LAYER_Y_SEPERATION positive (down) or negative (up)
    }

    public void SpawnWorld(int length, int height, int depth, Vector3 origin)
    {
        this.worldLength = length;
        this.worldHeight = height;
        this.worldDepth = depth;
        for(int k = 0; k < depth; k++)
        {
            GameObject newLayer = new GameObject();
            newLayer.name = "Layer" + k;
            newLayer.transform.SetParent(transform);
            newLayer.transform.position = new Vector3(k*Constants.LAYER_X_SEPERATION, k*Constants.LAYER_Y_SEPERATION, 0);
            Layer newLayerComp = newLayer.AddComponent<Layer>();
            newLayerComp.InitializeLayer(length, height, k);
            for(int j = 0; j < height; j++)
            {
                for(int i = 0; i < length; i++)
                {
                    GameObject latestTile;
                    if(k != 1 || i != 2 || j != 2)
                    {
                        if(i == length - 1 || i == 0 || j == height - 1 || j == 0)
                        {
                            latestTile = Instantiate(oceanTilePrefab, new Vector3(), new Quaternion());
                            latestTile.GetComponent<Tile>().InitializeTile(this, newLayerComp, i, j, k, false);
                        }
                        else
                        {
                            latestTile = Instantiate(grassTilePrefab, new Vector3(), new Quaternion());
                            latestTile.GetComponent<Tile>().InitializeTile(this, newLayerComp, i, j, k, true);
                        }
                    }
                    else
                    {
                        // To illustrate staircases
                        latestTile = Instantiate(walkwayTilePrefab, new Vector3(), new Quaternion());
                        latestTile.GetComponent<Tile>().InitializeTile(this, newLayerComp, i, j, k, false);
                    }
                    newLayerComp.AddTile(latestTile.GetComponent<Tile>());
                    latestTile.transform.SetParent(newLayer.transform);               
                    latestTile.transform.position = new Vector3(origin.x + i + newLayer.transform.position.x, origin.y - j, 0);    
                }
            }

            // adding objects to test labor order.
            // only in this file to test for now.
            // pawn immediately fulfills order by
            // teleporting to and destroying the tree
            GameObject tree = Instantiate(treePrefab, new Vector3(), new Quaternion());
            tree.transform.SetParent(newLayer.transform);
            tree.transform.position = new Vector3(origin.x+1 + newLayer.transform.position.x, origin.y-2, 0);

                // pawn
            GameObject pawn = Instantiate(pawnPrefab, new Vector3(), new Quaternion());
            pawn.transform.SetParent(newLayer.transform);
            pawn.transform.position = new Vector3(origin.x + 2 + newLayer.transform.position.x, origin.y - 1, 0);
            Pawn p = pawnPrefab.GetComponent<Pawn>();
            p.rename("test");


            // create labor order for the pawn
            LaborOrder cutTreeOrder = new LaborOrder();
            
            // this will add the labor order to the labor order manager where it will automatically be assigned to a pawn
            // from there, the pawn script will handle the rest of the labor order
            LaborOrderManager.enqueueLaborOrder(cutTreeOrder);

            layers.Add(newLayerComp);
        }
    }

    [ContextMenu("TestSpawnWorld")]
    private void TestSpawnWorld()
    {
        SpawnWorld(worldLength, worldHeight, worldDepth, new Vector3(0.5f, 0.5f, 0));
    }
}
