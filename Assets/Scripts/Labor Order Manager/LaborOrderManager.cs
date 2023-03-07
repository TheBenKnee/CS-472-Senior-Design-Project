using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LaborOrderManager : MonoBehaviour
{
    [SerializeField] public GameObject pawn_prefab;
    public static Queue<Pawn> pawns;
    public static List<Queue<LaborOrder>> laborQueues = new List<Queue<LaborOrder>>();
    public static int totalLaborOrders;
    public static int pawnCount;

    public static int getPawnCount() {
        return pawnCount;
    }

    public static void incrementPawnCount() {
        pawnCount++;
    }

    public static void decrementPawnCount() {
        pawnCount--;
    }

    // get the total number of labor orders in the list of labor queues
    public int getTotalLaborOrders() {
        int total = 0;
        for (int i = 0; i < 20; i++) {
            total += laborQueues[i].Count;
        }
        return total;
    }

    public static void enqueueLaborOrder(LaborOrder order){
        // add the labor order to th
        laborQueues[(int)order.laborType].Enqueue(order);
        totalLaborOrders++;
    }

    void assignPawn(){

        // print the number of pawns and labor orders
        //Debug.Log("Pawns: " + pawns.Count + " Labor Orders: " + laborQueues.Sum(x => x.Count));

        try
        {
            // dequeue a pawn
            Pawn pawn = pawns.Dequeue();

            // Dequeue a pawn.
            // Iterate through the list of labor queues and find the first labor order that matches a priority in the pawn's queueAnswerPriority array (start at index 0 and increment by 1 each iteration)
            // If a match is found, assign the pawn to the labor order and start the coroutine to complete the labor order
            // If no match is found, add the pawn back to the queue
            for (int i = 0; i < 4; i++) {
                for (int j = 0; j < 20; j++) {
                    if (laborQueues[j].Count > 0 && pawn.queueAnswerPriority[i].Contains(laborQueues[j].Peek().getLaborType())) {
                        pawn.currentLaborOrder = laborQueues[j].Dequeue();
                        StartCoroutine(pawn.completeCurrentLaborOrder());
                        return;
                    }
                }
            }
        }
        catch (System.Exception)
        { 
            //throw;
        }

        // print the number of pawns and labor orders
        //Debug.Log("Pawns: " + pawns.Count + " Labor Orders: " + laborQueues.Sum(x => x.Count));
    }

    // Start is called before the first frame update
    void Awake()
    {
        // initialize and populate pawn queue (Instantiate them as the children of this object)
        pawns = new Queue<Pawn>();
        for (int i = 0; i < 10; i++) {
            pawns.Enqueue(Instantiate(pawn_prefab, transform).GetComponent<Pawn>());
        }

        // iterate through the list of labor queues and initialize them
        for (int i = 0; i < 20; i++) {
            laborQueues.Add(new Queue<LaborOrder>());
        }

        // iterate through the list of labor queues and populate them with random labor orders that match the labor type
        int laborOrderToAdd = 5;
        for (int i = 0; i < 20; i++) {
            for (int j = 0; j < laborOrderToAdd; j++) {
                laborQueues[i].Enqueue(new LaborOrder((LaborType)i, UnityEngine.Random.Range(3, 10)));
            }
        }

        // initialize totalLaborOrders to 0
        totalLaborOrders = 0;

        // initialize pawnCount to 0
        pawnCount = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // assure there are pawns and labor orders to assign
        if (getTotalLaborOrders() > 0) {
            // assign a pawn to a labor order
            assignPawn();
        }
    }
}