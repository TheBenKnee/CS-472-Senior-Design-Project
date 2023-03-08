using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public enum LaborType { FireFight, Patient, Doctor, Sleep, Basic, Warden, Handle, Cook, Hunt, Construct, Grow, Mine, Farm, Woodcut, Smith, Tailor, Art, Craft, Haul, Clean, Research };

public class LaborOrderManager : MonoBehaviour
{
    [SerializeField]
    private GameObject pawn_prefab;
    private static Queue<Pawn> availablePawns;
    private static Queue<LaborOrder>[] laborQueues;
    private static int laborOrderTotal;
    public static int numOfLaborTypes;

    public const int NUM_OF_PAWNS_TO_SPAWN = 10;

    // iterate through the array of labor queues and sum the total number of labor orders at each queue
    public static int getNumOfLaborOrders()
    {
        int total = 0;
        for(int i = 0; i < numOfLaborTypes; i++){
            total += laborQueues[i].Count;
        }
        return total;
    }

    public static int getLaborOrderTotal()
    {
        return laborOrderTotal;
    }

    public static void addPawn(Pawn pawn)
    {
        // add pawn to the queue
        availablePawns.Enqueue(pawn);
    }

    public static Pawn getAvailablePawn()
    {
        // return pawn from the queue
        return availablePawns.Dequeue();
    }

    public static void addLaborOrder(LaborOrder laborOrder)
    {
        // add labor order to the queue
        laborQueues[(int)laborOrder.getLaborType()].Enqueue(laborOrder);
        laborOrderTotal++;
    }

    public static int getPawnCount(){
        return availablePawns.Count;
    }

    private void assignPawn()
    {
        try{

            if(availablePawns.Count > 0 && getNumOfLaborOrders() > 0){

                Pawn pawn = getAvailablePawn();
                List<LaborType>[] laborTypePriority = pawn.getLaborTypePriority();

                for(int i = 0; i < laborTypePriority.Length; i++){
                    if(laborTypePriority[i] != null) {
                        for(int j = 0; j < laborTypePriority[i].Count; j++){
                            if(laborQueues[(int)laborTypePriority[i][j]] != null && laborQueues[(int)laborTypePriority[i][j]].Count > 0){
                                pawn.setCurrentLaborOrder(laborQueues[(int)laborTypePriority[i][j]].Dequeue());
                                return;
                            }
                        }
                    }
                }

            }

        }catch(Exception e){
            Debug.Log(e);
            // set play mode to false
            //UnityEditor.EditorApplication.isPlaying = false;
        }

    }

    // Start is called before the first frame update
    void Awake()
    {
        laborOrderTotal = 0;
        numOfLaborTypes = Enum.GetNames(typeof(LaborType)).Length;

        // initialize and populate pawn queue (Instantiate them as the children of this object)
        availablePawns = new Queue<Pawn>();
        for (int i = 0; i < NUM_OF_PAWNS_TO_SPAWN; i++) {
            addPawn(Instantiate(pawn_prefab, transform).GetComponent<Pawn>());
        }

        // initialize the array of labor order queues
        laborQueues = new Queue<LaborOrder>[numOfLaborTypes];

        // iterate through the array of labor order queues and initialize each queue
        for(int i = 0; i < numOfLaborTypes; i++){
            laborQueues[i] = new Queue<LaborOrder>();
        }

        // iterate through the array of labor order queues and populate each with 3 random labor orders
        for(int i = 0; i < numOfLaborTypes; i++){
            for(int j = 0; j < 3; j++){
                addLaborOrder(new LaborOrder(true));
            }
        }
    }

    void Start(){
        // print the total number of pawns and the total number of labor orders 
        try{
            Debug.Log("Number of Pawns: " + getPawnCount() + " Number of Labor Orders: " + getNumOfLaborOrders());
        }catch{
            //Debug.Log("Error: Number of Pawns: " + getPawnCount() + " Number of Labor Orders: " + getNumOfLaborOrders());
        }
    }

    // Update is called once per frame
    void Update()
    {
        assignPawn();
    }
}