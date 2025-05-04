using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Useable : MonoBehaviour
{
    private RaycastHit hit;

    void Update()
    {
        // Cast a ray from the camera
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            // Get the GameObject that was hit
            GameObject hitObject = hit.collider.gameObject;

            // Do something with the hit object
            Debug.Log("Looking at: " + hitObject.name);
            // Example: Highlight the object (assuming it has a Renderer component)
            // hitObject.GetComponent<Renderer>().material.color = Color.red;
        }
        else
        {
            // Optional: Handle the case where the raycast didn't hit anything
            // Debug.Log("Not looking at anything");
            Debug.Log("Not Looking at anything");
        }
    }
}
