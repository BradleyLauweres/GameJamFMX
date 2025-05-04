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
    [SerializeField] private GameObject EscapeScreen;

    public bool IsInRange = false;


    private void Awake()
    {
        _instance = this;
        Instantiate(PlayerObject , SpawnPoint.transform.position, transform.rotation);
    }

    private void Start()
    {
        Application.targetFrameRate = Settings.FPSLimit;
    }

    private void Update()
    {
        OpenEscapeMenu();
    }

    private void OpenEscapeMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            bool IsActive = !EscapeScreen.activeSelf;
            EscapeScreen.SetActive(IsActive);   
        }
    }

   

}
