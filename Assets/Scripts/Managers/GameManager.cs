using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject PlayerObject;
    [SerializeField] private GameObject SpawnPoint;
    [SerializeField] private GameObject EscapeScreen;


    private void Awake()
    {
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
