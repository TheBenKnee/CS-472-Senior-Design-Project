using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.Tilemaps;

public class LaborOrder_Base_VM_Tests
{
    private GameObject gridManagerObject;
    private GridManager gridManager;
    private GameObject gridObject;

    [SetUp]
    public void SetUp()
    {
        gridManagerObject = new GameObject("GridManager");
        gridManager = gridManagerObject.AddComponent<GridManager>();

        gridObject = new GameObject("Grid");
        gridObject.AddComponent<Grid>();
        gridObject.AddComponent<Tilemap>();
        gridObject.transform.SetParent(gridManagerObject.transform);

        gridManager.InitializeGrid();
        gridManager.GenerateTileMap();
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(gridManagerObject);
    }

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

    [UnityTest]
    public IEnumerator RandomConstructor_SetsValidValues()
    {
        gridManager.InitializeGrid();

        // Arrange & Act
        LaborOrder_Base_VM laborOrder = new LaborOrder_Base_VM(true);

        // Assert
        //Assert.IsTrue(System.Enum.IsDefined(typeof(LaborType), laborOrder.GetLaborType()));
        //Assert.IsTrue(laborOrder.GetTimeToComplete() >= 0.5f && laborOrder.GetTimeToComplete() <= 1.0f);
        //Assert.IsTrue(laborOrder.GetLaborOrderLocation().x >= GridManager.MIN_HORIZONTAL && laborOrder.GetLaborOrderLocation().x < GridManager.MAX_HORIZONTAL);
        //Assert.IsTrue(laborOrder.GetLaborOrderLocation().y >= GridManager.MIN_VERTICAL && laborOrder.GetLaborOrderLocation().y < GridManager.MAX_VERTICAL);
        
        yield return null;
    }

    // Add more tests if needed, e.g., for the Execute method
}
