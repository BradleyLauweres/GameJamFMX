using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if(_instance == null)
            {
                Debug.Log("GameManager is NULL");
            }
            return _instance;
        }
    }

    [SerializeField] private GameObject PlayerObject;
    [SerializeField] private GameObject SpawnPoint;

    public GameState state = GameState.Playing;
    public CursorLockMode lockMode = CursorLockMode.Locked;

    public bool IsInRange = false;


    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
    }

    private void Update()
    {   
        if (state == GameState.Playing)
            Cursor.lockState = CursorLockMode.Locked;
        else
            Cursor.lockState = CursorLockMode.None;
    }

   

   

}
