using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clicker : MonoBehaviour
{
    public GameObject clicked;
    public GameObject rightClicked;
    public List<ExampleWalk> pawns = new List<ExampleWalk>(); 
    public bool done = false;

    // Update is called once per frame
    public void Update()
    {
        //Check for mouse click 
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast (ray.origin, ray.direction, 100f);
            if (hit.collider != null) 
            {
                CurrentClickedGameObject(hit.transform.gameObject);
            }
            else
            {
                CurrentClickedGameObject(null);
            }
        }

        //Check for right mouse click 
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast (ray.origin, ray.direction, 100f);
            if (hit.collider != null) 
            {
                CurrentRightClickedObject(hit.transform.gameObject);
            }
            else
            {
                if(clicked != null && clicked.GetComponent<ExampleWalk>() != null)
                {
                    clicked.GetComponent<ExampleWalk>().MoveToPosition(new Vector3(ray.origin.x, ray.origin.y, 0f));
                }
            }
        }
    }

    public void CurrentClickedGameObject(GameObject newClicked)
    {
        if(clicked != null && clicked.GetComponent<ExampleWalk>() != null)
        {
            clicked.GetComponent<ExampleWalk>().Deselected();
        }
        clicked = newClicked;
        if(clicked != null && clicked.GetComponent<ExampleWalk>() != null)
        {
            clicked.GetComponent<ExampleWalk>().Selected();
        }
    }

    public void CurrentRightClickedObject(GameObject newClicked)
    {
        rightClicked = newClicked;
        WorkArea wA = rightClicked.GetComponent<WorkArea>();
        if(rightClicked != null && wA != null)
        {
            if(clicked != null && clicked.GetComponent<ExampleWalk>() != null)
            {
                //Perform action
                if(clicked.GetComponent<ExampleWalk>() != null)
                {
                    if(wA.worker != null)
                    {
                        wA.worker.StopWork();
                    }
                    clicked.GetComponent<ExampleWalk>().WorkAt(rightClicked.GetComponent<WorkArea>());
                    wA.worker = clicked.GetComponent<ExampleWalk>();
                }
            }
            else
            {
                foreach(ExampleWalk pawn in pawns)
                {
                    if(pawn.myWorkerType == wA.myType)
                    {
                        if(wA.worker != null)
                        {
                            wA.worker.StopWork();
                        }
                        pawn.WorkAt(wA);
                        wA.worker = pawn;
                        break;
                    }
                }
            }
        }
    }
}
