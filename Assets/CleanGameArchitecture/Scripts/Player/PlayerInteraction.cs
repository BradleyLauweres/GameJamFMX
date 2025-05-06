using Assets.CleanGameArchitecture.Scripts.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Interaction Settings")]
    [SerializeField] private float interactionDistance = 3f;
    [SerializeField] private LayerMask interactionLayer;
    [SerializeField] private KeyCode interactionKey = KeyCode.E;
    [SerializeField] private Transform cameraTransform;

    [Header("UI Feedback")]
    [SerializeField] private GameObject interactionPrompt;
    [SerializeField] private TMPro.TextMeshProUGUI promptText;

    [SerializeField] private GameObject crossHair;

    private GameObject currentUseableObject;
    private IInteractable currentInteractable;

    private Vector3 originalCameraPosition;
    private Quaternion originalCameraRotation;
    private bool isInteracting = false;
    private CharacterController characterController;

    private void Start()
    {
        if (cameraTransform == null)
            cameraTransform = Camera.main.transform;

        characterController = GetComponent<CharacterController>();

        if (interactionPrompt != null)
            interactionPrompt.SetActive(false);
    }

    private void Update()
    {
        if (isInteracting)
        {
            crossHair.SetActive(false);

            if (Input.GetKeyDown(KeyCode.E))
            {
                StopInteraction();
            }
            return;
        }

        RaycastHit hit;
        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, interactionDistance, interactionLayer))
        {

            if (hit.collider.CompareTag("Useable"))
            {
                GameObject hitObject = hit.collider.gameObject;

                IInteractable interactable = hitObject.GetComponent<IInteractable>();

                if (interactable != null)
                {
                    currentUseableObject = hitObject;
                    currentInteractable = interactable;

                    if (interactionPrompt != null)
                    {
                        interactionPrompt.SetActive(true);

                        if (promptText != null)
                        {
                            string objectType = DetermineObjectType(hitObject);
                            promptText.text = $"Press E to use {objectType}";
                        }
                    }

                    if (Input.GetKeyDown(interactionKey))
                    {
                        StartInteraction();
                    }
                }
                else
                {
                    ClearCurrentUseable();
                }
            }
            else
            {
                ClearCurrentUseable();
            }
        }
        else
        {
            ClearCurrentUseable();
        }
    }

    private string DetermineObjectType(GameObject obj)
    {
        if (obj.GetComponent<ComputerController>() != null)
            return "Computer";
        else if (obj.GetComponent<BoardController>() != null)
            return "Board";
        else
            return "Object";
    }

    private void ClearCurrentUseable()
    {
        currentUseableObject = null;
        currentInteractable = null;

        if (interactionPrompt != null)
            interactionPrompt.SetActive(false);
    }

    private void StartInteraction()
    {
        if (currentInteractable != null)
        {
            originalCameraPosition = cameraTransform.position;
            originalCameraRotation = cameraTransform.rotation;

            if (characterController != null)
                characterController.enabled = false;

            isInteracting = true;

            currentInteractable.StartInteraction(cameraTransform);


            if (interactionPrompt != null)
                interactionPrompt.SetActive(false);
        }
    }

    public void StopInteraction()
    {
        if (currentInteractable != null)
        {
            currentInteractable.EndInteraction();

            StartCoroutine(ReturnToOriginalPosition());
        }
    }

    private IEnumerator ReturnToOriginalPosition()
    {
        float duration = 0.5f;
        float elapsedTime = 0f;

        Vector3 startPos = cameraTransform.position;
        Quaternion startRot = cameraTransform.rotation;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            cameraTransform.position = Vector3.Lerp(startPos, originalCameraPosition, t);
            cameraTransform.rotation = Quaternion.Lerp(startRot, originalCameraRotation, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        cameraTransform.position = originalCameraPosition;
        cameraTransform.rotation = originalCameraRotation;

        if (characterController != null)
            characterController.enabled = true;

        crossHair.SetActive(true);
        isInteracting = false;
    }
}