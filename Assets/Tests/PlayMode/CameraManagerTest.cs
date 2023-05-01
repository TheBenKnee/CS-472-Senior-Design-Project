using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

namespace Tests
{
    public class CameraManagerTest
    {
        [UnitySetUp]
        public IEnumerator SetUp()
        {
            SceneManager.LoadScene("CameraManagerTestScene", LoadSceneMode.Single);
            yield return null;
            GridManager.InitializeGridManager();
            for(int i=0; i<2; i++)
            {
                GridManager.CreateLevel();
	        }
            CameraManager.InitializeCamera();
            yield return new EnterPlayMode();
        }

        [UnityTearDown]
        public IEnumerator TearDown()
        {
            yield return new ExitPlayMode();
	    }

        [UnityTest]
        public IEnumerator CameraManager_MoveCamera_UP_AreEqual_Pass()
        {
            yield return new WaitForSeconds(0.5f);

            Vector3 previousCameraPosition = Camera.main.transform.position;
            CameraManager.MoveCamera(CameraMovement.UP);
            Vector3 updatedCameraPosition = Camera.main.transform.position;

            Assert.AreEqual(previousCameraPosition.x, updatedCameraPosition.x);
        }

        [UnityTest]
        public IEnumerator CameraManager_MoveCamera_UP_YGreater_Pass()
        {
            yield return new WaitForSeconds(0.5f);

            Vector3 previousCameraPosition = Camera.main.transform.position;
            CameraManager.MoveCamera(CameraMovement.UP);
            Vector3 updatedCameraPosition = Camera.main.transform.position;

            Assert.Greater(updatedCameraPosition.y, previousCameraPosition.y);
        }

        [UnityTest]
        public IEnumerator CameraManager_MoveCamera_UP_YGreaterThanMapHeight_AreEqual_Pass()
        {
            yield return new WaitForSeconds(0.5f);

            Vector3 previousCameraPosition = Camera.main.transform.position;

            int mapHeight = GridManager.mapLevels[0].getYMax();
            float cameraOrthographicSize = Camera.main.orthographicSize;
            float distance = mapHeight - previousCameraPosition.y - cameraOrthographicSize;
            float newYValue = previousCameraPosition.y + distance;

            Vector3 moveCamera = new Vector3(
                previousCameraPosition.x,
                newYValue,
                0);

            Camera.main.transform.position = moveCamera;
            previousCameraPosition = Camera.main.transform.position;

            CameraManager.MoveCamera(CameraMovement.UP);

            Vector3 updatedCameraPosition = Camera.main.transform.position;

            Assert.AreEqual(updatedCameraPosition, previousCameraPosition);
	    }

        [UnityTest]
        public IEnumerator CameraManager_MoveCamera_RIGHT_xIncrease_Pass()
        {
            yield return new WaitForSeconds(0.5f);

            Vector3 previousCameraPosition = Camera.main.transform.position;
            CameraManager.MoveCamera(CameraMovement.RIGHT);
            Vector3 updatedCameraPosition = Camera.main.transform.position;

            Assert.Greater(updatedCameraPosition.x, previousCameraPosition.x);
	    }

        [UnityTest]
        public IEnumerator CameraManager_MoveCamera_RIGHT_Y_Pass()
        {
            yield return new WaitForSeconds(0.5f);

            Vector3 previousCameraPosition = Camera.main.transform.position;
            CameraManager.MoveCamera(CameraMovement.RIGHT);
            Vector3 updatedCameraPosition = Camera.main.transform.position;

            Assert.AreEqual(previousCameraPosition.y, updatedCameraPosition.y);
	    }

        [UnityTest]
        public IEnumerator CameraManager_MoveCamera_RIGHT_XGreaterThanMapWidth_Pass()
        {
            yield return new WaitForSeconds(0.5f);

            Vector3 previousCameraPosition = Camera.main.transform.position;

            int mapWidth = GridManager.mapLevels[0].getXMax();
            float cameraOrthographicSize = Camera.main.orthographicSize;
            float distance = (mapWidth) - previousCameraPosition.x - (cameraOrthographicSize * Camera.main.aspect);
            float newXValue = previousCameraPosition.x + distance;

            Vector3 moveCamera = new Vector3(
                newXValue,
                previousCameraPosition.y,
                0);

            Camera.main.transform.position = moveCamera;
            previousCameraPosition = Camera.main.transform.position;

            CameraManager.MoveCamera(CameraMovement.RIGHT);

            Vector3 updatedCameraPosition = Camera.main.transform.position;

            Assert.AreEqual(previousCameraPosition, updatedCameraPosition);
	    }

        [UnityTest]
        public IEnumerator CameraManager_MoveCamera_DOWN_yDecrease_Pass()
        {
            yield return new WaitForSeconds(0.5f);

            Vector3 previousCameraPosition = Camera.main.transform.position;
            CameraManager.MoveCamera(CameraMovement.DOWN);
            Vector3 updatedCameraPosition = Camera.main.transform.position;

            Assert.Less(updatedCameraPosition.y, previousCameraPosition.y);
	    }

        [UnityTest]
        public IEnumerator CameraManager_MoveCamera_DOWN_X_Pass()
        {
            yield return new WaitForSeconds(0.5f);

            Vector3 previousCameraPosition = Camera.main.transform.position;
            CameraManager.MoveCamera(CameraMovement.DOWN);
            Vector3 updatedCameraPosition = Camera.main.transform.position;

            Assert.AreEqual(updatedCameraPosition.x, previousCameraPosition.x);
	    }

        [UnityTest]
        public IEnumerator CameraManager_MoveCamera_DOWN_YLessThanMapYMin_Pass()
        {
            yield return new WaitForSeconds(0.5f);

            Vector3 previousCameraPosition = Camera.main.transform.position;

            float cameraOrthographicSize = Camera.main.orthographicSize;
            float distance = previousCameraPosition.y - cameraOrthographicSize;
            float newYValue = previousCameraPosition.y - distance;

            Vector3 moveCamera = new Vector3(
                previousCameraPosition.x,
                newYValue,
                0);

            Camera.main.transform.position = moveCamera;
            previousCameraPosition = Camera.main.transform.position;

            CameraManager.MoveCamera(CameraMovement.DOWN);

            Vector3 updatedCameraPosition = Camera.main.transform.position;

            Assert.AreEqual(previousCameraPosition, updatedCameraPosition);
	    }

        [UnityTest]
        public IEnumerator CameraManager_MoveCamera_LEFT_xDecrease_Pass()
        {
            yield return new WaitForSeconds(0.5f);

            Vector3 previousCameraPosition = Camera.main.transform.position;
            CameraManager.MoveCamera(CameraMovement.LEFT);
            Vector3 updatedCameraPosition = Camera.main.transform.position;

            Assert.Less(updatedCameraPosition.x, previousCameraPosition.x);
	    }

        [UnityTest]
        public IEnumerator CameraManager_MoveCamera_LEFT_Y_Pass()
        {
            yield return new WaitForSeconds(0.5f);

            Vector3 previousCameraPosition = Camera.main.transform.position;
            CameraManager.MoveCamera(CameraMovement.LEFT);
            Vector3 updatedCameraPosition = Camera.main.transform.position;

            Assert.AreEqual(updatedCameraPosition.y, previousCameraPosition.y);
	    }

        [UnityTest]
        public IEnumerator CameraManager_MoveCamera_LEFT_XLessThanMapXMin_Pass()
        {
            yield return new WaitForSeconds(0.5f);

            Vector3 previousCameraPosition = Camera.main.transform.position;

            float cameraOrthographicSize = Camera.main.orthographicSize;
            float distance = previousCameraPosition.x - (cameraOrthographicSize * Camera.main.aspect);
            float newXValue = previousCameraPosition.x - distance;

            Vector3 moveCamera = new Vector3(
                newXValue,
                previousCameraPosition.y,
                0);

            Camera.main.transform.position = moveCamera;
            previousCameraPosition = Camera.main.transform.position;

            CameraManager.MoveCamera(CameraMovement.LEFT);

            Vector3 updatedCameraPosition = Camera.main.transform.position;

            if (previousCameraPosition.x - updatedCameraPosition.x < 0.0001f)
            {
                Assert.Pass();
            }
            else
	        {
                Assert.Fail();
	        }
	    }

        [UnityTest]
        public IEnumerator CameraManager_MoveCamera_LEVEL_DOWN_AreEqual_Pass()
        {
            yield return new WaitForSeconds(0.5f);

            Vector3 previousCameraPosition = Camera.main.transform.position;
            float expectedXPosition = previousCameraPosition.x + GridManager.LEVEL_WIDTH;
            Vector3 expectedCameraPosition = new Vector3(
                expectedXPosition,
                previousCameraPosition.y,
                0);

            CameraManager.MoveCamera(CameraMovement.LEVEL_DOWN);

            Assert.AreEqual(expectedCameraPosition, Camera.main.transform.position);
	    }

        [UnityTest]
        public IEnumerator CameraManager_MoveCamera_LEVEL_UP_AreEqual_Pass()
        {
            yield return new WaitForSeconds(0.5f);

            Vector3 expectedCameraPosition = Camera.main.transform.position;

            float setCameraXPosition = Camera.main.transform.position.x + GridManager.LEVEL_WIDTH;
            Vector3 setCameraPosition = new Vector3(
                setCameraXPosition,
                Camera.main.transform.position.y,
                0);
            Camera.main.transform.position = setCameraPosition;

            CameraManager.MoveCamera(CameraMovement.LEVEL_UP);

            Assert.AreEqual(expectedCameraPosition, Camera.main.transform.position);
	    }

        [UnityTest]
        public IEnumerator CameraManager_UpdateCameraOrthographicSize_OrthographicSizeIncrease_Pass()
        {
            yield return new WaitForSeconds(0.5f);

            float previousCameraOrthographicSize = Camera.main.orthographicSize;

            float delta = -1.5f;
            CameraManager.UpdateCameraOrthographicSize(delta);

            Assert.Less(Camera.main.orthographicSize, previousCameraOrthographicSize);
	    }

        [UnityTest]
        public IEnumerator CameraManager_UpdateCameraOrthographicSize_OrthographicSizeDecrease_Pass()
        {
            yield return new WaitForSeconds(0.5f);

            float previousCameraOrthographicSize = Camera.main.orthographicSize;

            float delta = 1.5f;
            CameraManager.UpdateCameraOrthographicSize(delta);

            Assert.Greater(Camera.main.orthographicSize, previousCameraOrthographicSize);
	    }

        [UnityTest]
        public IEnumerator CameraManager_UpdateCameraOrthographicSize_CameraLowerLeft_UpdateCameraPositionX_Pass()
        {
            yield return new WaitForSeconds(0.5f);

            Camera camera = Camera.main;

            // Set camera x
            float cameraOrthographicSize = Camera.main.orthographicSize;
            float xDistance = camera.transform.position.x - (cameraOrthographicSize * camera.aspect);
            float newXValue = camera.transform.position.x - xDistance;

            // Set camera y
            float yDistance = camera.transform.position.y - cameraOrthographicSize;
            float newYValue = camera.transform.position.y - yDistance;

            Vector3 moveCamera = new Vector3(newXValue, newYValue, 0);
            camera.transform.position = moveCamera;

            Vector3 previousCameraPosition = Camera.main.transform.position;
            
	        float delta = 2.5f;
            CameraManager.UpdateCameraOrthographicSize(delta);

            Assert.Greater(camera.transform.position.x, previousCameraPosition.x);
	    }

        [UnityTest]
        public IEnumerator CameraManager_UpdateCameraOrthographicSize_CameraLowerLeft_UpdateCameraPositionY_Pass()
        {
            yield return new WaitForSeconds(0.5f);

            Camera camera = Camera.main;

            // Set camera x
            float cameraOrthographicSize = Camera.main.orthographicSize;
            float xDistance = camera.transform.position.x - (cameraOrthographicSize * camera.aspect);
            float newXValue = camera.transform.position.x - xDistance;

            // Set camera y
            float yDistance = camera.transform.position.y - cameraOrthographicSize;
            float newYValue = camera.transform.position.y - yDistance;

            Vector3 moveCamera = new Vector3(newXValue, newYValue, 0);
            camera.transform.position = moveCamera;

            Vector3 previousCameraPosition = Camera.main.transform.position;

            float delta = 2.5f;
            CameraManager.UpdateCameraOrthographicSize(delta);

            Assert.Greater(camera.transform.position.y, previousCameraPosition.y);
        }

        [UnityTest]
        public IEnumerator CameraManager_UpdateCameraOrthographicSize_CameraUpperRight_UpdateCameraPositionY_Pass()
        {
            yield return new WaitForSeconds(0.5f);

            Camera camera = Camera.main;

            // Set camera y
            int mapHeight = GridManager.mapLevels[0].getYMax();
            float cameraOrthographicSize = camera.orthographicSize;
            float yDistance = mapHeight - camera.transform.position.y - cameraOrthographicSize;
            float newYValue = camera.transform.position.y + yDistance;

            // Set camera x
            int mapWidth = GridManager.mapLevels[0].getXMax();
            float xDistance = (mapWidth - 1) - camera.transform.position.x - (cameraOrthographicSize * camera.aspect);
            float newXValue = camera.transform.position.x + xDistance;

            Vector3 moveCamera = new Vector3(newXValue, newYValue, 0);
            camera.transform.position = moveCamera;

            Vector3 previousCameraPosition = camera.transform.position;

            float delta = 2.5f;
            CameraManager.UpdateCameraOrthographicSize(delta);

            Assert.Less(camera.transform.position.y, previousCameraPosition.y);
	    }

        [UnityTest]
        public IEnumerator CameraManager_UpdateCameraOrthographicSize_CameraUpperRight_UpdateCameraPositionX_Pass()
        {
            yield return new WaitForSeconds(0.5f);

            Camera camera = Camera.main;

            // Set camera y
            int mapHeight = GridManager.mapLevels[0].getYMax();
            float cameraOrthographicSize = camera.orthographicSize;
            float yDistance = mapHeight - camera.transform.position.y - cameraOrthographicSize;
            float newYValue = camera.transform.position.y + yDistance;

            // Set camera x
            int mapWidth = GridManager.mapLevels[0].getXMax();
            float xDistance = (mapWidth - 1) - camera.transform.position.x - (cameraOrthographicSize * camera.aspect);
            float newXValue = camera.transform.position.x + xDistance;

            Vector3 moveCamera = new Vector3(newXValue, newYValue, 0);
            camera.transform.position = moveCamera;

            Vector3 previousCameraPosition = camera.transform.position;

            float delta = 2.5f;
            CameraManager.UpdateCameraOrthographicSize(delta);

            Assert.Less(camera.transform.position.x, previousCameraPosition.x);
        }
    }
}
