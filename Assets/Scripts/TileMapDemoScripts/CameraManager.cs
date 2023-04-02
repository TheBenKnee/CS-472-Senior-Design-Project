using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager
{
    private Camera mainCamera;
    private const int CAMERA_BOUNDS = 1000;
    private const int CAMERA_Z_POSITION = -10;
    private const float SPEED = 20.0f;

    public CameraManager(Camera mainCamera)
    {
        this.mainCamera = mainCamera;
    }

    public void UpdateCamera()
    {
        if (Input.GetKey(KeyCode.W)) {
            Vector3 target = new Vector3(mainCamera.transform.position.x, CAMERA_BOUNDS, CAMERA_Z_POSITION);
            mainCamera.transform.position = Vector3.MoveTowards(mainCamera.transform.position, target, SPEED * Time.deltaTime);
	    } else if (Input.GetKey(KeyCode.D)) {
            Vector3 target = new Vector3(CAMERA_BOUNDS, mainCamera.transform.position.y, CAMERA_Z_POSITION);
            mainCamera.transform.position = Vector3.MoveTowards(mainCamera.transform.position, target, SPEED * Time.deltaTime);
	    } else if (Input.GetKey(KeyCode.S)) {
            Vector3 target = new Vector3(mainCamera.transform.position.x, CAMERA_BOUNDS * -1, CAMERA_Z_POSITION);
            mainCamera.transform.position = Vector3.MoveTowards(mainCamera.transform.position, target, SPEED * Time.deltaTime);
	    } else if (Input.GetKey(KeyCode.A)) {
            Vector3 target = new Vector3(CAMERA_BOUNDS * -1, mainCamera.transform.position.y, CAMERA_Z_POSITION);
            mainCamera.transform.position = Vector3.MoveTowards(mainCamera.transform.position, target, SPEED * Time.deltaTime);
	    }
    }
}
