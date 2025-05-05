using Assets.Scripts.Enums;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PickupManager : MonoBehaviour
{
    [SerializeField] private GameObject[] Items;
    private GameObject _player;
    public PlayerController _pc;

    private static PickupManager _instance;

    public static PickupManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.Log("PickupManager is NULL");
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
    }

    private void Update()
    {
        if(_player == null)
        {
            _player = GameObject.FindGameObjectWithTag("Player");
            _pc = _player.GetComponent<PlayerController>();

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
        _pc.TogglePickableUI(true);

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (other.name.Contains("(Clone)"))
            {
                other.name = other.name.Replace("(Clone)", "");
            }

            var item = Items.FirstOrDefault(x => x.name == other.name);
            item.SetActive(true);

            Destroy(other);
        }

    }

}
