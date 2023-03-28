using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEditor;

public class LaborOrderManager_VM_Tests
{
    private LaborOrderManager_VM laborOrderManager;

    [SetUp]
    public void SetUp()
    {
        GameObject laborOrderManagerPrefab = Resources.Load<GameObject>("LaborOrderManager_VM");
        GameObject laborOrderManagerInstance = UnityEngine.Object.Instantiate(laborOrderManagerPrefab);
        laborOrderManager = laborOrderManagerInstance.GetComponent<LaborOrderManager_VM>();
    }

    [TearDown]
    public void TearDown()
    {
        UnityEngine.Object.DestroyImmediate(laborOrderManager.gameObject);
    }

    [Test]
    public void TestNumberOfLaborTypes()
    {
        // Arrange
        int expectedLaborTypes = Enum.GetNames(typeof(LaborType)).Length;

        // Act
        int actualLaborTypes = LaborOrderManager_VM.GetNumberOfLaborTypes();

        // Assert
        Assert.AreEqual(expectedLaborTypes, actualLaborTypes);
    }

    [Test]
    public void TestAddPawnAndGetAvailablePawn()
    {
        // Arrange
        GameObject pawnPrefab = Resources.Load<GameObject>("Pawn_VM");
        Pawn_VM pawnToAdd = UnityEngine.Object.Instantiate(pawnPrefab).GetComponent<Pawn_VM>();

        // Act
        LaborOrderManager_VM.AddPawn(pawnToAdd);
        Pawn_VM retrievedPawn = LaborOrderManager_VM.GetAvailablePawn();

        // Assert
        Assert.AreEqual(pawnToAdd, retrievedPawn);
    }

    [Test]
    public void TestAddAndGetLaborOrder()
    {
        // Arrange
        LaborOrder_Base_VM laborOrderToAdd = new LaborOrder_Base_VM(true);

        // Act
        LaborOrderManager_VM.AddLaborOrder(laborOrderToAdd);

        // Assert
        Queue<LaborOrder_Base_VM>[] laborQueues = (Queue<LaborOrder_Base_VM>[])typeof(LaborOrderManager_VM)
            .GetField("laborQueues", BindingFlags.NonPublic | BindingFlags.Static)
            .GetValue(laborOrderManager);

        Assert.IsTrue(laborQueues[(int)laborOrderToAdd.GetLaborType()].Contains(laborOrderToAdd));
    }

    [Test]
    public void TestGetNumOfLaborOrders()
    {
        // Arrange
        LaborOrder_Base_VM laborOrder1 = new LaborOrder_Base_VM(true);
        LaborOrder_Base_VM laborOrder2 = new LaborOrder_Base_VM(true);

        // Act
        LaborOrderManager_VM.AddLaborOrder(laborOrder1);
        LaborOrderManager_VM.AddLaborOrder(laborOrder2);
        int numOfLaborOrders = LaborOrderManager_VM.GetNumOfLaborOrders();

        // Assert
        Assert.AreEqual(2, numOfLaborOrders);
    }

    [Test]
    public void TestGetLaborOrderTotal()
    {
        // Arrange
        LaborOrder_Base_VM laborOrder1 = new LaborOrder_Base_VM(true);
        LaborOrder_Base_VM laborOrder2 = new LaborOrder_Base_VM(true);

        // Act
        LaborOrderManager_VM.AddLaborOrder(laborOrder1);
        LaborOrderManager_VM.AddLaborOrder(laborOrder2);
        int laborOrderTotal = LaborOrderManager_VM.GetLaborOrderTotal();

        // Assert
        Assert.AreEqual(2, laborOrderTotal);
    }

    [Test]
    public void TestAssignPawns()
    {
        // Arrange
        for (int i = 0; i < 10; i++)
        {
            LaborOrder_Base_VM laborOrder = new LaborOrder_Base_VM(true);
            LaborOrderManager_VM.AddLaborOrder(laborOrder);
        }

        // Act

        // Assert
        int numOfLaborOrdersAfterAssign = LaborOrderManager_VM.GetNumOfLaborOrders();
        int availablePawnsAfterAssign = LaborOrderManager_VM.GetPawnCount();
        Assert.IsTrue(numOfLaborOrdersAfterAssign < 10);
        Assert.IsTrue(availablePawnsAfterAssign < 10);
    }
}

