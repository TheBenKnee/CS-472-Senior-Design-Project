using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum LaborType { FireFight, Patient, Doctor, Sleep, Basic, Warden, Handle, Cook, Hunt, Construct, Grow, Mine, Farm, Woodcut, Smith, Tailor, Art, Craft, Haul, Clean, Research };

[System.Serializable]
public struct LaborOrder
{
    public LaborType? laborType;
    public float timeToComplete;
    public int orderNumber;
    public static int orderCount = 0;

    public const int NUM_OF_LABOR_TYPES = 21;
    const float MIN_TTC = 3.0f;
    const float MAX_TTC = 5.0f;

    public LaborOrder(LaborType laborType, float timeToComplete)
    {
        this.laborType = laborType;
        this.timeToComplete = timeToComplete;
        orderNumber = ++orderCount;
    }

    // default constructor for testing struct
    public LaborOrder(bool isRandom){
        laborType = (LaborType)UnityEngine.Random.Range(0, NUM_OF_LABOR_TYPES);
        timeToComplete = UnityEngine.Random.Range(MIN_TTC, MAX_TTC);
        orderNumber = ++orderCount;
    }

    public LaborType getLaborType() {
        return (LaborType)laborType;
    }
}