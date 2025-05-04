using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [Header("Pickup Function")]
    [SerializeField] private GameObject InteractText;
    private GameObject mainObject;
    private PickupManager pickUpManager;
    private GameManager gm;


    private RaycastHit hit;

    private void Awake()
    {
        mainObject = GameObject.Find("Main");
        pickUpManager = mainObject.GetComponent<PickupManager>();
        gm = mainObject.GetComponent<GameManager>();
    }

    void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            if(pickUpManager == null)
                pickUpManager = GameObject.Find("Main").GetComponent<PickupManager>();


            GameObject hitObject = hit.collider.gameObject;

            if(hitObject.tag == "Pickable" && gm.IsInRange)
            {
                InteractText.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    pickUpManager.PickUp(hitObject);
                }
            }
            else
                InteractText.SetActive(false);

            if (hitObject.tag == "Useable" && gm.IsInRange)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    pickUpManager.Interact();
                }
               
            }
          
            Debug.Log("Looking at: " + hitObject.name);
        
        }
        
    }
}
