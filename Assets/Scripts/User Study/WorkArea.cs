using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkArea : MonoBehaviour
{
    public enum WorkType
    {
        Mining,
        Fishing,
        Farming,
        Selling
    }

    public WorkType myType;
    public ExampleWalk worker;

    public void QuitWork()
    {
        worker = null;
    }
}
