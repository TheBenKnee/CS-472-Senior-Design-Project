using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Layer : MonoBehaviour
{
    private List<BaseTile_VM> tiles = new List<BaseTile_VM>();
    [SerializeField] private int layerLength, layerHeight, layerNumber;

    public BaseTile_VM GetTile(int x, int y)
    {
        return tiles[y*layerLength+x];
    }

    public void AddTile(BaseTile_VM newTile)
    {
        tiles.Add(newTile);
    }

    public void InitializeLayer(int length, int height, int layerNumber)
    {
        layerLength = length;
        layerHeight = height;
        this.layerNumber = layerNumber;
    }

    public int GetLayerNumber()
    {
        return layerNumber;
    }
}
