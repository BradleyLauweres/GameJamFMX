using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [Header("Pickup Function")]
    [SerializeField] private GameObject PickableText;
    [SerializeField] private GameObject InteractText;
    private GameObject mainObject;
    private PickupManager pickUpManager;


    [Header("Interaction")]
    bool _isInteracting = false;
    [SerializeField] private GameObject _camera;
    [SerializeField] private GameObject _mainCamera;
    [SerializeField] private KeyCode _key = KeyCode.V;
    private bool isMainCamActive = true;

    public Transform targetPosition;
    public float speed = 5f;
    private Vector3 _currentPosition;
    private Vector3 _oldPosition;


    private RaycastHit hit;

    private void Awake()
    {
        mainObject = GameObject.Find("Main");
        pickUpManager = mainObject.GetComponent<PickupManager>();
    }

    private void Start()
    {
        _currentPosition = _camera.transform.position;
        _oldPosition = _camera.transform.position;
    }

    void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            if (pickUpManager == null)
                pickUpManager = GameObject.Find("Main").GetComponent<PickupManager>();


            GameObject hitObject = hit.collider.gameObject;

            if (hitObject.tag == "Pickable" && hitObject.GetComponent<Item>().IsInRange)
            {
                PickableText.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    pickUpManager.PickUp(hitObject);
                }
            }
            else
                PickableText.SetActive(false);

            if (hitObject.tag == "Useable" && hitObject.GetComponent<Item>().IsInRange)
            {
                InteractText.SetActive(true);

                targetPosition = GameObject.Find("interactionpoint").transform;

                if (Input.GetKeyDown(KeyCode.E))
                {
                    _isInteracting = !_isInteracting;

                    if (_isInteracting)
                    {
                        MoveCamera(_camera.transform.position, targetPosition.position);
                    }
                    else
                    {
                        isMainCamActive = !isMainCamActive;
                    }

                    _mainCamera.gameObject.SetActive(!isMainCamActive);
                    _camera.gameObject.SetActive(isMainCamActive);

                }


            }
            else
            {
                _mainCamera.gameObject.SetActive(isMainCamActive);
                _camera.gameObject.SetActive(!isMainCamActive);
                InteractText.SetActive(false);
            }
                

        }

    }

    void MoveCamera(Vector3 from, Vector3 to)
    {
        float step = speed * Time.deltaTime;
        _currentPosition = Vector3.Lerp(from, to, step);
        _camera.transform.position = _currentPosition;
    }
}
