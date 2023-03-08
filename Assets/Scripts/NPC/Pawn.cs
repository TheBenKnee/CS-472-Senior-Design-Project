using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : BaseNPC
{
	[SerializeField]
    private List<LaborType>[] LaborTypePriority;
    private LaborOrder currentLaborOrder;
    private string pawnName;
    private bool isAssigned;
    
    private const int NUM_OF_PRIORITY_LEVELS = 4;

    // set the current labor order of the pawn and indicate that the pawn is assigned to a labor order
    public void setCurrentLaborOrder(LaborOrder laborOrder) {
        currentLaborOrder = laborOrder;
        isAssigned = true;
    }

    // get the current labor type priorities of the pawn
    public List<LaborType>[] getLaborTypePriority() {
        return LaborTypePriority;
    }

    // coroutine to complete labor order by waiting for the time to complete
    public IEnumerator completeCurrentLaborOrder() {
		// wait the time to complete the labor order
        yield return new WaitForSeconds(currentLaborOrder.getTimeToComplete());

		// debug print the pawn name and the labor type and the labor number and time to complete
        Debug.Log($"{pawnName,-10} completed {currentLaborOrder.getLaborType(),-10} {currentLaborOrder.getOrderNumber(),-10} in {currentLaborOrder.getTimeToComplete(),-5:F2} seconds");

		// set the current labor order to null
        isAssigned = false;

        // add the pawn back to the queue of available pawns
        LaborOrderManager.addPawn(this);

        // stop the coroutine
        StopCoroutine(completeCurrentLaborOrder());
    }

    // Start is called before the first frame update
    void Awake()
    {   
        // initialize the array of lists
        LaborTypePriority = new List<LaborType>[NUM_OF_PRIORITY_LEVELS];

        // iterate through the labor types and add them to the list at random priority levels
        foreach(LaborType laborType in System.Enum.GetValues(typeof(LaborType))) {
            int randomPriorityLevel = Random.Range(0, NUM_OF_PRIORITY_LEVELS);
            if(LaborTypePriority[randomPriorityLevel] == null) {
                LaborTypePriority[randomPriorityLevel] = new List<LaborType>();
            }
            LaborTypePriority[randomPriorityLevel].Add(laborType);
        }

        // initialize the default current labor order
        currentLaborOrder = new LaborOrder();

        // initialize the default pawn name to null
        pawnName = "Pawn" + GetInstanceID();
        name = pawnName;

        // initialize the default isWorking to false
        isAssigned = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isAssigned){
            StartCoroutine(completeCurrentLaborOrder());
        }else{
            // no order to complete, do nothing
        }
    }
}
