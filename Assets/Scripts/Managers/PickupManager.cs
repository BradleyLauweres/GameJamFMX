using Assets.Scripts.Enums;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PickupManager : MonoBehaviour
{
    [SerializeField] private GameObject[] Items;
    private GameObject _player;

    private void Update()
    {
        if(_player == null)
        {
            _player = GameObject.FindGameObjectWithTag("Player");

            GameObject itemsParent = GameObject.Find("Items");

            if (itemsParent != null)
            {
                int childCount = itemsParent.transform.childCount;
                Items = new GameObject[childCount];

                for (int i = 0; i < childCount; i++)
                {
                    Items[i] = itemsParent.transform.GetChild(i).gameObject;
                }
            }
            else
            {
                Debug.LogWarning("Items GameObject not found in the scene.");
            }
        }
    }

    public void PickUp(GameObject other)
    {

        if (other.name.Contains("(Clone)"))
        {
            other.name = other.name.Replace("(Clone)", "");
        }

        var item = Items.FirstOrDefault(x => x.name == other.name);
        item.SetActive(true);
        Destroy(other);
    }

    public void Interact()
    {
        Debug.Log("Interact");
    }
}
