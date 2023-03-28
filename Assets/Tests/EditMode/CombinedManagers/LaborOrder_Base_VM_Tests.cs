using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class LaborOrder_Base_VM_Tests
{
    [Test]
    public void Constructor_SetsCorrectValues()
    {
        // Arrange
        LaborType laborType = LaborType.Basic;
        float timeToComplete = 1.5f;

        // Act
        LaborOrder_Base_VM laborOrder = new LaborOrder_Base_VM(laborType, timeToComplete);

        // Assert
        Assert.AreEqual(laborType, laborOrder.GetLaborType());
        Assert.AreEqual(timeToComplete, laborOrder.GetTimeToComplete());
    }

    [Test]
    public void RandomConstructor_SetsValidValues()
    {
        // Arrange & Act
        LaborOrder_Base_VM laborOrder = new LaborOrder_Base_VM(true);

        // Assert
        Assert.IsTrue(System.Enum.IsDefined(typeof(LaborType), laborOrder.GetLaborType()));
        Assert.IsTrue(laborOrder.GetTimeToComplete() >= 0.5f && laborOrder.GetTimeToComplete() <= 1.0f);
        Assert.IsTrue(laborOrder.GetLaborOrderLocation().x >= GridManager.MIN_HORIZONTAL && laborOrder.GetLaborOrderLocation().x < GridManager.MAX_HORIZONTAL);
        Assert.IsTrue(laborOrder.GetLaborOrderLocation().y >= GridManager.MIN_VERTICAL && laborOrder.GetLaborOrderLocation().y < GridManager.MAX_VERTICAL);
    }

    [UnityTest]
    public IEnumerator Execute_WaitsForTimeToComplete()
    {
        // Arrange
        LaborType laborType = LaborType.Basic;
        float timeToComplete = 1.0f;
        LaborOrder_Base_VM laborOrder = new LaborOrder_Base_VM(laborType, timeToComplete);
        Pawn_VM pawn = new GameObject().AddComponent<Pawn_VM>();

        // Act
        float startTime = Time.time;
        yield return laborOrder.Execute(pawn);
        float endTime = Time.time;

        // Assert
        Assert.IsTrue((endTime - startTime) >= timeToComplete);
    }
}
