using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class CameraManager : MonoBehaviour
{


    private RaycastHit hit;

    private void Awake()
    {

    }

    void Update()
    {
        SendObject();
    }

    private void SendObject()
    {
        if(GameManager.state == GameState.Playing)
        {

            if(Physics.Raycast(transform.position, transform.forward, out hit))
            {
                GameObject hitObject = hit.collider.gameObject;

                if(hitObject.tag == "Pickable" && hitObject.GetComponent<Item>().IsInRange)
                {
                    PickupManager.Instance.PickUp(hitObject);
                }
                else
                {

                }


                if (hitObject.tag == "Useable" && hitObject.GetComponent<Item>().IsInRange)
                {
                    InteractManager.Instance.Interact(hitObject);
                }
                else
                {
                    InteractManager.Instance._pc.ToggleInteractableUI(false);
                }
            }

          


        }
    }

}
