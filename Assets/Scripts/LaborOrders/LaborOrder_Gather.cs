using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LaborOrder_Gather : LaborOrder_Base_VM
{
    // constructor
    public LaborOrder_Gather() : base()
    {

    }

    public override IEnumerator Execute(Pawn_VM pawn)
    {
        yield return null;
    }
}