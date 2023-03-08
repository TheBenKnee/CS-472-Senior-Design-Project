using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct LaborOrder
{
    private LaborType laborType;
    private float timeToComplete;
    private int orderNumber;

    private const float MIN_TTC = 0.5f;
    private const float MAX_TTC = 1.0f;

    public LaborOrder(LaborType laborType, float timeToComplete)
    {
        this.laborType = laborType;
        this.timeToComplete = timeToComplete;
        orderNumber = LaborOrderManager.getNumOfLaborOrders();
        LaborOrderManager.addLaborOrder(this);
    }

    public LaborOrder(bool isRandomConstructor)
    {
        laborType = (LaborType)UnityEngine.Random.Range(0, LaborOrderManager.numOfLaborTypes);
        timeToComplete = UnityEngine.Random.Range(MIN_TTC, MAX_TTC);
        orderNumber = LaborOrderManager.getNumOfLaborOrders();
        LaborOrderManager.addLaborOrder(this);
    }

    public LaborType getLaborType() {
        return laborType;
    }

    public float getTimeToComplete() {
        return timeToComplete;
    }

    public int getOrderNumber() {
        return orderNumber;
    }    
}