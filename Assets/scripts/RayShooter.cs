using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayShooter : MonoBehaviour
{
    private Camera cam; 

    void Start()
    {
        cam = GetComponent<Camera>(); 
        //hides mouse cursor at the center of the screen 
        Cursor.lockState = CursorLockMode.Locked; 
        Cursor.visible = false; 
    }

    

    void Update()
    {
        //respond to first mouse button
        if (Input.GetMouseButtonDown(0))
        {
            //middle of screen is half width and height 
            Vector3 point = new Vector3(cam.pixelWidth/2, cam.pixelHeight/2, 0); 
            //create a ray at that position 
            Ray ray = cam.ScreenPointToRay(point); 
            RaycastHit hit; 
            //raycast fills referenced variable with this info 
            if (Physics.Raycast(ray, out hit))
            {
                //retrieve object the ray hit 
                GameObject hitObject = hit.transform.gameObject; 
                ReactiveTarget target = hitObject.GetComponent<ReactiveTarget>(); 
                //check for reactive target component on object
                if (target != null)
                {
                    //Debug.Log("Target Hit"); 
                    target.ReactToHit(); 
                }
                else 
                {
                    //launch coroutine in response to a hit
                    StartCoroutine(SphereIndicator(hit.point));   
                }
            }
        }
    }
    
    void OnGUI()
    {
        int size = 12; 
        float posX = cam.pixelWidth/2 - size/2; 
        float posY = cam.pixelHeight/2 - size; 
        //displays text for * on screen 
        GUI.Label(new Rect(posX, posY, size, size), "*"); 
        //GUI.Label(new Rect(cam.pixelWidth, cam.pixelHeight, 50, 50), "Hit " + hit.point); 
    }

    private IEnumerator SphereIndicator(Vector3 pos)
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere); 
        sphere.transform.position = pos; 
        //tells the coroutine where to pause 
        yield return new WaitForSeconds(1); 
        
        Destroy(sphere); 
    }
}
