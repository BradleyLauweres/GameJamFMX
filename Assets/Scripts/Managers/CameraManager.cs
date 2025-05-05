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
        if(GameManager.Instance.state == GameState.Playing)
        {

            if(Physics.Raycast(transform.position, transform.forward, out hit))
            {
                GameObject hitObject = hit.collider.gameObject;

                if(hitObject.tag == "Door" && hitObject.GetComponent<Item>().IsInRange)
                {
                    InteractManager.Instance.DoorLogic();
                }
                else
                {
                    if (InteractManager.Instance._pc != null)
                        InteractManager.Instance._pc.ToggleEndGamePlaceholder(false);
                }

                if (hitObject.tag == "Pickable" && hitObject.GetComponent<Item>().IsInRange)
                {
                    PickupManager.Instance.PickUp(hitObject);
                }
                else
                {
                    if (PickupManager.Instance._pc != null)
                        PickupManager.Instance._pc.TogglePickableUI(false);
                }

                if (hitObject.tag == "Useable" && hitObject.GetComponent<Item>().IsInRange)
                {
                    InteractManager.Instance._ItemToInteractWith = hitObject.name;
                    InteractManager.Instance.Interact(hitObject);
                }
                else
                {
                    if (InteractManager.Instance._pc != null)
                    {
                        InteractManager.Instance._pc.ToggleInteractableUI(false);
                    }

                }
            }

          


        }
    }

}
