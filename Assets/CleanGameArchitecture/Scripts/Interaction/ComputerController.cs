using Assets.CleanGameArchitecture.Scripts.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerController : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform viewPosition;
    [SerializeField] private float transitionSpeed = 5f;
    [SerializeField] private Canvas computerScreenCanvas;

    private Transform playerCamera;
    private bool isBeingUsed = false;

    private void Start()
    {
        if (computerScreenCanvas != null)
            computerScreenCanvas.enabled = false;
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

        if (computerScreenCanvas != null)
            computerScreenCanvas.enabled = true;
    }

    public void EndInteraction()
    {
        isBeingUsed = false;
        Cursor.lockState = CursorLockMode.Locked;

        if (computerScreenCanvas != null)
            computerScreenCanvas.enabled = false;
    }

    public Transform GetViewPosition()
    {
        return viewPosition;
    }
}
