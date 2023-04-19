using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LaborOrder_Destroy : LaborOrder_Base_VM
{
    GameObject itemToRemove;

    // constructor
    public LaborOrder_Destroy() : base()
    {

    }

    public override IEnumerator Execute(Pawn_VM pawn)
    {
        yield return null;
    }
}