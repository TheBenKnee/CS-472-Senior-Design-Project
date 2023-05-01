using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Tests
{
    public class LaborOrderPanelManagerTests
    {
        LaborOrderPanelManager manager;

		[UnitySetUp]
        public IEnumerator SetUp()
        {
            SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
            yield return null;
            yield return new EnterPlayMode();
            GameObject.Find("Canvas").GetComponent<PanelManager>().ToggleLaborOrderPanel();
            manager = GameObject.Find("Canvas/Pawn Priorities/LaborOrderPanel").GetComponent<LaborOrderPanelManager>();
        }

        [UnityTearDown]
        public IEnumerator TearDown()
        {
            yield return new ExitPlayMode();
        }

        [UnityTest]
        public IEnumerator LaborOrderPanel_InitializeLaborOrderPanel()
        {
            //Arrange
			yield return new WaitForSeconds(0.5f);

            // Assert Initialization
            Assert.IsNotNull(manager.buttonContainer);
            Assert.IsNotNull(manager.pawnNameContainer);
            Assert.IsNotNull(manager.LaborNameContainer);
            Assert.IsNotNull(manager.pawnContainer);

            Assert.IsNotNull(manager.button_prefab);
            Assert.IsNotNull(manager.pawnText_prefab);
            Assert.IsNotNull(manager.LaborText_prefab);

            Assert.AreEqual(manager.laborTypeNames.Length, LaborOrderManager.GetLaborTypesCount());
            Assert.AreEqual(manager.buttonContainer.transform.childCount, manager.pawnContainer.transform.childCount * LaborOrderManager.GetLaborTypesCount());

        }

        [UnityTest]
        public IEnumerator LaborOrderPanel_AddAndRemovePawnButtons()
        {

            ////////////////////////////////////////////
            //            AddPawnButtons()            //
            ////////////////////////////////////////////

            //Arrange
			yield return new WaitForSeconds(0.5f);
            GameObject pawn_prefab = Resources.Load<GameObject>("prefabs/npc/Pawn");
            GameObject pawn_instance = UnityEngine.Object.Instantiate(pawn_prefab, GridManager.tileMap.GetCellCenterWorld(Vector3Int.FloorToInt(new Vector3(GridManager.LEVEL_WIDTH / 2, GridManager.LEVEL_HEIGHT / 2, 0))), Quaternion.identity);

            pawn_instance.transform.SetParent(GameObject.Find("Pawns").transform);
            LaborOrderManager.AddAvailablePawn(pawn_instance.GetComponent<Pawn>());
            Pawn newPawn = pawn_instance.GetComponent<Pawn>();

            // Act create pawn buttons.
            manager.AddPawnButtons(pawn_instance);

            Assert.IsNotNull(GameObject.Find(newPawn.GetPawnName() + " (Text)"));
            // Assert that buttons were created with appropriate number of buttons / labor type.
            for (int i = 0; i < LaborOrderManager.GetLaborTypesCount(); i++)
            {
                Assert.IsNotNull(GameObject.Find(newPawn.GetPawnName() + ": " + LaborOrderManager.GetLaborTypeName(i)));
            }

            ////////////////////////////////////////////
            //          RemovePawnButtons()           //
            ////////////////////////////////////////////

            // Act remove pawn buttons.
            manager.RemovePawnButtons(pawn_instance);
            yield return new WaitForSeconds(0.5f);

            Assert.IsNull(GameObject.Find(newPawn.GetPawnName() + " (Text)"));
            // Assert that buttons were created with appropriate number of buttons / labor type.
            for (int i = 0; i < LaborOrderManager.GetLaborTypesCount(); i++)
            {
                Assert.IsNull(GameObject.Find(newPawn.GetPawnName() + ": " + LaborOrderManager.GetLaborTypeName(i)));
            }
            manager.AddPawnButtons(pawn_instance);
        }

        [UnityTest]
        public IEnumerator LaborOrderPanel_AdjustGridLayout()
        {
            //Arrange
			yield return new WaitForSeconds(0.5f);

            // Act
            GridLayoutGroup buttonGrid = manager.buttonContainer.GetComponent<GridLayoutGroup>();
            GridLayoutGroup laborNameGrid = manager.LaborNameContainer.GetComponent<GridLayoutGroup>();
            manager.AdjustGridLayout();

            //Assert
            Assert.AreEqual(buttonGrid.constraintCount, LaborOrderManager.GetLaborTypesCount());
            Assert.AreEqual(laborNameGrid.constraintCount, LaborOrderManager.GetLaborTypesCount());
            
        }
    }
}
