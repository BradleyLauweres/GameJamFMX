using Assets.Scripts.Enums;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PickupManager : MonoBehaviour
{
    [SerializeField] private GameObject[] Items;
    [SerializeField] private GameObject UIText;


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        UIText.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log(other.name);
        UIText.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKey(KeyCode.E))
        {
            if (other.GetComponent<Item>().Action == ActionEnum.Pickable)
            {
                PickUp(other);
            }
        }
    }

    private void PickUp(Collider other)
    {
        var item = Items.FirstOrDefault(x => x.name == other.name);
        item.SetActive(true);
        other.gameObject.SetActive(false);
        UIText.SetActive(false);
    }

    private void Interact()
    {
        Debug.Log("Interact");
    }
}
