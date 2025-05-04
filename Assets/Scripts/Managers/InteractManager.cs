using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;

public class InteractManager : MonoBehaviour
{
    [Header("Player")]
    public PlayerController _pc;

    [Header("Camera Moving Interaction")]
    [SerializeField] Camera _animationPcCam;
    [SerializeField] Camera _animationWhiteBoardCam;
    [SerializeField] GameObject _playerCam;

    [SerializeField] private Transform _animCamPos;
    [SerializeField] private Transform targetPcPosition;
    [SerializeField] private Transform targetBoardPosition;
    [SerializeField] public float speed = 5f;

    private Vector3 _currentPosition;
    private Vector3 _oldPosition;

    private string _ItemToInteractWith;

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
        _currentPosition = _animationPcCam.transform.position;
        _oldPosition = _animationPcCam.transform.position;
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

            if (_ItemToInteractWith == "Pc")
            {
                _animationPcCam.gameObject.SetActive(true);
                _playerCam.SetActive(false);
                MoveCamera(targetPcPosition.position, _animationPcCam.transform.position , _animationPcCam);
            }

            if(_ItemToInteractWith == "Board")
            {
                _animationWhiteBoardCam.gameObject.SetActive(true);
                _playerCam.SetActive(false);
                MoveCamera(targetBoardPosition.position, _animationWhiteBoardCam.transform.position, _animationWhiteBoardCam);
            }
           
        }

        StopInteracting();

    }

    public void Interact(GameObject gameObject)
    {
        _pc.ToggleInteractableUI(true);

        if (Input.GetKeyDown(KeyCode.E))
        {
            _ItemToInteractWith = gameObject.name;
            GameManager.state = GameState.Interacting;
        }

    }

    public void StopInteracting()
    {
        if (GameManager.state == GameState.Interacting && Input.GetKeyDown(KeyCode.E))
        {
            _playerCam.SetActive(true);
            _animationPcCam.gameObject.SetActive(false);
            _animationWhiteBoardCam.gameObject.SetActive(false);
            GameManager.state = GameState.Playing;
        }
    }

    void MoveCamera(Vector3 from, Vector3 to , Camera cam)
    {
        float step = speed * Time.deltaTime;
        _currentPosition = Vector3.Lerp(from, to, step);
        cam.transform.position = _currentPosition;
    }

}
