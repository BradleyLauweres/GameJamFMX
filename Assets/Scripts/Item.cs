using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private Transform _playerPos;
    private GameManager gm;

    void Update()
    {
        gm = GameObject.Find("Main").GetComponent<GameManager>();
        _playerPos = GameObject.FindGameObjectWithTag("Player").transform;

        if (IsPlayerClose(_playerPos, 2f))
        {
            gm.IsInRange = true;
            Debug.Log("Player is close to the item!");
        }
        else
        {
            gm.IsInRange = false;
        }
    }


    public bool IsPlayerClose(Transform playerPos, float maxDistance)
    {
        float distance = Vector3.Distance(transform.position, playerPos.transform.position);
        Debug.Log(distance);
        return distance <= maxDistance;

    }
}
