using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

namespace Tests
{
    public class InputManagerTest
    {
        private GameObject obj;

        [SetUp]
        public void Setup()
        {
            SceneManager.LoadScene("InputManagerTest");
        }

        [UnityTest]
        public IEnumerator InputManager_CheckForInput()
        {
            yield return new WaitForSeconds(0.5f);

            obj = GameObject.Find("Grid");

            var grid    = obj.GetComponent<Grid>() as Grid;
            var tm      = obj.GetComponent<Tilemap>() as Tilemap;

            tm.BoxFill(
                new Vector3Int(0, 0, 0), 
                ScriptableObject.CreateInstance(typeof(BaseTile)) as BaseTile, 
                0, 
                0, 
                10, 
                10
            );

            var gridm   = obj.AddComponent<GridManager>();
            
            GridManager.tileMap = tm;

            var im      = obj.AddComponent<InputManager>();

            GridManager.InitializeGridManager();

            InputManager.CheckForInput();
            Assert.Pass();
        }
    }
}