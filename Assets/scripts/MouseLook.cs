using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    //define enum data structure to associate names with settings 
    public enum RotationAxes
    {
        mouseXandY = 0, 
        mouseX = 1, 
        mouseY = 2
    }

    public RotationAxes axes = RotationAxes.mouseXandY; 
    public float sensitivityHor = 9.0f; 
    public float sensitivityVert = 9.0f; 

    public float minimumVert = -45.0f; 
    public float maximumVert = 45.0f; 

    private float verticalRot = 0; 

    void Start()
    {
        Rigidbody body = GetComponent<Rigidbody>(); 
        if (body != null)
        {
            body.freezeRotation = true; 
        }
    }

    void Update()
    {
        if (axes == RotationAxes.mouseX)
        {
            transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityHor, 0); 
        }
        else if (axes == RotationAxes.mouseY)
        {
            verticalRot -= Input.GetAxis("Mouse Y") * sensitivityVert; 
            //clamps vertical angle between min and max limits 
            verticalRot = Mathf.Clamp(verticalRot, minimumVert, maximumVert); 
            //keep the same y angle
            float horizontalRot = transform.localEulerAngles.y; 
            //create a new vector from the rotation values 
            transform.localEulerAngles = new Vector3(verticalRot, horizontalRot, 0); 
        }
        else 
        {
            verticalRot -= Input.GetAxis("Mouse Y") * sensitivityVert; 
            verticalRot = Mathf.Clamp(verticalRot, minimumVert, maximumVert); 

            float delta = Input.GetAxis("Mouse X") * sensitivityHor; 
            float horizontalRot = transform.localEulerAngles.y + delta; 

            transform.localEulerAngles = new Vector3(verticalRot, horizontalRot, 0); 
        }
    }
}
