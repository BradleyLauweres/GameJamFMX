using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject PlayerObject;
    [SerializeField] private GameObject SpawnPoint;

    private void Awake()
    {
        Instantiate(PlayerObject , SpawnPoint.transform.position, transform.rotation);
    }

    private void Start()
    {
        Application.targetFrameRate = Settings.FPSLimit;
    }

}
