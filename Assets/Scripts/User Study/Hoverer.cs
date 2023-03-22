using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hoverer : MonoBehaviour
{
    public Vector3 offset;

    // Update is called once per frame
    void Update()
    {
        transform.position = Input.mousePosition + offset;   
    }
}
