using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

namespace Test
{
    public class MainMenuUIObjectTests
    {

        GameObject obj;

        [SetUp]
        public void Setup()
        {
            obj = new GameObject();
            obj.AddComponent<MainMenuUIObject>();
            obj.AddComponent<Image>();
        }

        [Test]
        public void MainMenuUIObject_MouseOver()
        {
            obj.GetComponent<MainMenuUIObject>().MouseOver();
            Assert.AreEqual(obj.GetComponent<Image>().enabled, true);
        }

        [Test]
        public void MainMenuUIObject_MouseExit()
        {
            obj.GetComponent<MainMenuUIObject>().MouseExit();
            Assert.AreEqual(obj.GetComponent<Image>().enabled, false);
        }
    } 
}
