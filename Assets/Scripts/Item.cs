using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Item : MonoBehaviour
{
    private Transform _playerPos;

    public bool IsInRange;

    void Update()
    {
        _playerPos = GameObject.FindGameObjectWithTag("Player").transform;

        if (IsPlayerClose(_playerPos, 1.5f))
        {
            IsInRange = true;
        }
        else
        {
            IsInRange = false;
        }
    }


    public bool IsPlayerClose(Transform playerPos, float maxDistance)
    {
        float distance = Vector3.Distance(transform.position, playerPos.transform.position);
        Debug.Log(distance);
        return distance <= maxDistance;

    }
}
