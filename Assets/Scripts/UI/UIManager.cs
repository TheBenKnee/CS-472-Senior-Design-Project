using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public enum UIMode { normal = 1, normalFoodFarm = 2, premiumFoodFarm = 3, clothFarm = 4, normalMedFarm = 5, premiumMedFarm = 6, destroy = 7, create = 8 };

    public UIMode myMode;

    public void SelectUIMode(int newMode)
    {
        myMode = (UIMode)newMode;
    }
}