using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : BaseNPC
{
	[SerializeField]
	//public GameObject pawnObject;
    public LaborOrder currentLaborOrder;
    public LaborType? currentLaborOrderType;
    public string pawnName;
    public List<LaborType>[] queueAnswerPriority;

    // coroutine to complete labor order by waiting for the time to complete
    public IEnumerator completeCurrentLaborOrder() {

		// wait the time to complete the labor order
        yield return new WaitForSeconds(currentLaborOrder.timeToComplete);

		// debug print the pawn name and the labor type
		Debug.Log(pawnName + " has completed work order type " + currentLaborOrder.laborType);

		// set the current labor order to null
        currentLaborOrder.laborType = null;

        // add the pawn back to the queue of available pawns
        LaborOrderManager.pawns.Enqueue(this);

    }

	public void rename(string newName)
    {
		this.pawnName = newName;
    }

    // Start is called before the first frame update
    void Awake()
    {   
        // initialize pawn name and assign to pawn while incrementing pawnCount
        LaborOrderManager.incrementPawnCount();
        name = "Pawn" + LaborOrderManager.getPawnCount();
        pawnName = "Pawn" + LaborOrderManager.getPawnCount();


        // initialize the queueAnswerPriority array
        queueAnswerPriority = new List<LaborType>[4];

        // randomly choose a valid index of queueAnswerPriority array and add the labor type to the list at that index
        for (int i = 0; i < 20; i++) {
            int randomIndex = UnityEngine.Random.Range(0, 4);
            if (queueAnswerPriority[randomIndex] == null) {
                queueAnswerPriority[randomIndex] = new List<LaborType>();
            }
            queueAnswerPriority[randomIndex].Add((LaborType)i);
        }


        // debug print the pawn name and the labor types in the list at each index of the queueAnswerPriority array
        //Debug.Log(pawnName + " has the following labor types in their queueAnswerPriority array:");
        //for (int i = 0; i < 4; i++) {
        //    Debug.Log("Index " + i + ":");
        //    for (int j = 0; j < queueAnswerPriority[i].Count; j++) {
        //        Debug.Log(queueAnswerPriority[i][j]);
        //    }
        //}
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        currentLaborOrderType = currentLaborOrder.laborType;
    }
}
