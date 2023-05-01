using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Tests 
{
	public class MainMenuManagerTests
	{

        MainMenuManager mainManager;

		[UnitySetUp]
        public IEnumerator SetUp()
        {
            SceneManager.LoadScene("Main Menu", LoadSceneMode.Single);
            yield return null;
            yield return new EnterPlayMode();
            mainManager = GameObject.Find("Main Menu Test").GetComponent<MainMenuManager>();
        }

        [UnityTearDown]
        public IEnumerator TearDown()
        {
            yield return new ExitPlayMode();
        }

		[UnityTest]
		public IEnumerator MainMenu_Awake()
		{
            //Arrange
			yield return new WaitForSeconds(0.5f);
            
            //Act
            mainManager.Awake();

            //Assert
            Assert.AreEqual(mainManager.homeScreen.activeSelf && !mainManager.newMenu.activeSelf
                && !mainManager.loadMenu.activeSelf && !mainManager.settingsMenu.activeSelf
                && !mainManager.exitMenu.activeSelf, true);
        }

		[UnityTest]
		public IEnumerator MainMenu_DisableMenus()
		{
            //Arrange
			yield return new WaitForSeconds(0.5f);
            
            //Act
            mainManager.DisableMenus(); // "Esc" button from inputManager not made (temporary).

            //Assert
            Assert.AreEqual(!mainManager.homeScreen.activeSelf && !mainManager.newMenu.activeSelf
            && !mainManager.loadMenu.activeSelf && !mainManager.settingsMenu.activeSelf
            && !mainManager.exitMenu.activeSelf, true);
        }

        [UnityTest]
        public IEnumerator  MainMenu_ReturnHome()
        {
            //Arrange
			yield return new WaitForSeconds(0.5f);

            //Act
            mainManager.ReturnHome();

            //Assert
            Assert.AreEqual(mainManager.homeScreen.activeSelf && !mainManager.newMenu.activeSelf
            && !mainManager.loadMenu.activeSelf && !mainManager.settingsMenu.activeSelf
            && !mainManager.exitMenu.activeSelf, true);
        }

        [UnityTest]
        public IEnumerator  MainMenu_SelectNewOption()
        {
            //Arrange
			yield return new WaitForSeconds(0.5f);

            // Act
            mainManager.ReturnHome();
            mainManager.selectOption("new");
            
            //Assert
            Assert.AreEqual(!mainManager.homeScreen.activeSelf && mainManager.newMenu.activeSelf
                && !mainManager.loadMenu.activeSelf && !mainManager.settingsMenu.activeSelf
                && !mainManager.exitMenu.activeSelf, true);

            //Arrange
			yield return new WaitForSeconds(0.5f);

            //Act
            mainManager.ReturnHome();
            mainManager.selectOption("load");

            //Assert
            Assert.AreEqual(!mainManager.homeScreen.activeSelf && !mainManager.newMenu.activeSelf
                && mainManager.loadMenu.activeSelf && !mainManager.settingsMenu.activeSelf
                && !mainManager.exitMenu.activeSelf, true);

            //Arrange
			yield return new WaitForSeconds(0.5f);

            //Act
            mainManager.ReturnHome();
            mainManager.selectOption("settings");

            //Assert
            Assert.AreEqual(!mainManager.homeScreen.activeSelf && !mainManager.newMenu.activeSelf
                && !mainManager.loadMenu.activeSelf && mainManager.settingsMenu.activeSelf
                && !mainManager.exitMenu.activeSelf, true);

            //Arrange
			yield return new WaitForSeconds(0.5f);

            //Act
            mainManager.ReturnHome();
            mainManager.selectOption("exit");

            //Assert
            Assert.AreEqual(!mainManager.homeScreen.activeSelf && !mainManager.newMenu.activeSelf
                && !mainManager.loadMenu.activeSelf && !mainManager.settingsMenu.activeSelf
                && mainManager.exitMenu.activeSelf, true);

            //Arrange
			yield return new WaitForSeconds(0.5f);

            //Act (Default Option)
            mainManager.ReturnHome();
            mainManager.selectOption("default");

            //Assert
            Assert.AreEqual(mainManager.homeScreen.activeSelf && !mainManager.newMenu.activeSelf
                && !mainManager.loadMenu.activeSelf && !mainManager.settingsMenu.activeSelf
                && !mainManager.exitMenu.activeSelf, true);
        }

        [UnityTest]
        public IEnumerator  MainMenu_PlayGame()
        {
            //Arrange
			yield return new WaitForSeconds(0.5f);
            var targetScene = SceneManager.GetActiveScene().buildIndex + 1;

            //Act
            mainManager.PlayGame();
            yield return new WaitForSeconds(0.5f);

            //Assert
            Assert.AreEqual(SceneManager.GetActiveScene().buildIndex, targetScene);
        }

        [UnityTest]
        public IEnumerator  MainMenu_LoadSaveGame()
        {
            //Arrange
			yield return new WaitForSeconds(0.5f);
            var targetScene = SceneManager.GetActiveScene().buildIndex + 2;

            //Act
            mainManager.LoadSaveGame();
            yield return new WaitForSeconds(0.5f);

            //Assert
            Assert.AreEqual(SceneManager.GetActiveScene().buildIndex, targetScene);
        }

        [UnityTest]
        public IEnumerator  MainMenu_ExitGame()
        {
            //Arrange
			yield return new WaitForSeconds(0.5f);

            //Act
            mainManager.ReturnHome();
            mainManager.ExitGame(); // Exit game button not made (temporary).
        }
    }
}
