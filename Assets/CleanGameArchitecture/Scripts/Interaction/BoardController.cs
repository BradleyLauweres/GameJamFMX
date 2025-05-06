using Assets.CleanGameArchitecture.Scripts.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardController : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform viewPosition;
    [SerializeField] private float transitionSpeed = 5f;
    [SerializeField] private GameObject boardContent;

    private Transform playerCamera;
    private bool isBeingUsed = false;

    private void Start()
    {
        if (boardContent != null)
            boardContent.SetActive(false);
    }

    private void Update()
    {
        if (isBeingUsed && playerCamera != null)
        {
            playerCamera.position = Vector3.Lerp(playerCamera.position, viewPosition.position, Time.deltaTime * transitionSpeed);
            playerCamera.rotation = Quaternion.Lerp(playerCamera.rotation, viewPosition.rotation, Time.deltaTime * transitionSpeed);
        }
    }

    public void StartInteraction(Transform camera)
    {
        playerCamera = camera;
        isBeingUsed = true;
        Cursor.lockState = CursorLockMode.None;

        if (boardContent != null)
            boardContent.SetActive(true);
    }

    public void EndInteraction()
    {
        isBeingUsed = false;
        Cursor.lockState = CursorLockMode.Locked;

        if (boardContent != null)
            boardContent.SetActive(false);
    }

    public Transform GetViewPosition()
    {
        return viewPosition;
    }
}
