using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Tests
{
    public class InputManagerTest
    {
        private GameObject obj;
        private InputManager im;

        [SetUp]
        public void Setup()
        {
            obj = new GameObject();
            obj.AddComponent<InputManager>();
            im = obj.GetComponent<InputManager>();
        }

        [UnityTest]
        public void InputManager_CheckForInput()
        {
            im.CheckForInput();
            Assert.Pass();
        }
    }
}