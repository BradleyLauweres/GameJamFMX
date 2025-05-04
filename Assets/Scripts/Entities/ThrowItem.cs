using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ThrowItem : MonoBehaviour
{
    [SerializeField] GameObject[] items;
    [SerializeField] GameObject[] prefabs;
    public GameObject itemInHand;  
    public GameObject itemPrefab;
    public float throwForce = 1f;    
    public Transform throwPoint;

    void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.Mouse0) )
        {
            try
            {
                itemInHand = items.FirstOrDefault(x => x.activeSelf == true);
                itemPrefab = items.FirstOrDefault(x => x.name == itemInHand.name);
                Throw();
            }
            catch {
                Debug.Log("Non pickable item");
            }
          
           
        }
    }

    void Throw()
    {
        GameObject thrownItem = Instantiate(itemPrefab, throwPoint.position, throwPoint.rotation);
        Rigidbody rb = thrownItem.GetComponent<Rigidbody>();
        Collider collider = thrownItem.GetComponent<Collider>();

        if (rb == null) rb = thrownItem.AddComponent<Rigidbody>();
        if(collider == null) collider = thrownItem.AddComponent<BoxCollider>();

        rb.AddForce(throwPoint.forward * throwForce);

        itemPrefab.SetActive(false);
    }
}
