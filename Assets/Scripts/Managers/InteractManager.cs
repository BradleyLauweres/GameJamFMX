using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class InteractManager : MonoBehaviour
{
    [Header("Player")]
    public PlayerController _pc;

    [Header("Camera Moving Interaction")]
    [SerializeField] Camera _animationCam;
    [SerializeField] GameObject _playerCam;

    [SerializeField] private Transform _animCamPos;
    [SerializeField] private Transform targetPosition;
    [SerializeField] public float speed = 5f;

    private Vector3 _currentPosition;
    private Vector3 _oldPosition;

    [Header("Computer Controls")]
    [SerializeField] private float mouseSensitivity = 100f;
    private float xRotation = 0f;


    private static InteractManager _instance;

    public static InteractManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.Log("InteractManager is NULL");
            }
            return _instance;
        }
    }

    void Start()
    {
        _currentPosition = _animationCam.transform.position;
        _oldPosition = _animationCam.transform.position;
    }

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
    }

    private void Update()
    {

        if (_pc == null)
            _pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        if (_playerCam == null)
            _playerCam = GameObject.FindGameObjectWithTag("MainCamera");

        if (_animCamPos == null)
            _animCamPos = _playerCam.transform;

        if(GameManager.state == GameState.Interacting)
        {
            Cursor.lockState = CursorLockMode.None;
        }

        StopInteracting();

    }

    public void Interact(GameObject gameObject)
    {
        _pc.ToggleInteractableUI(true);

        if (Input.GetKeyDown(KeyCode.E))
        {
            GameManager.state = GameState.Interacting;

            _animationCam.gameObject.SetActive(true);
            _playerCam.SetActive(false);

            MoveCamera(targetPosition.position, _animationCam.transform.position);

        }
    }

    public void StopInteracting()
    {
        if (GameManager.state == GameState.Interacting && Input.GetKeyDown(KeyCode.E))
        {
            _playerCam.SetActive(true);
            _animationCam.gameObject.SetActive(false);
            GameManager.state = GameState.Playing;
        }
    }

    void MoveCamera(Vector3 from, Vector3 to)
    {
        float step = speed * Time.deltaTime;
        _currentPosition = Vector3.Lerp(from, to, step);
        _animationCam.transform.position = _currentPosition;
    }

}
