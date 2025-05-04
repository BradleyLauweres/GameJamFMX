using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField] private Camera _camera;
    private GameObject _PlayerCamera;
     
    public Transform targetPosition; 
    public float speed = 5f; 
    private Vector3 _currentPosition;
    private Vector3 _oldPosition;

    bool _isInteracting = false;


    void Start()
    {
        _currentPosition = _camera.transform.position;
        _oldPosition = _camera.transform.position;
    }

    void Update()
    {
        if (_PlayerCamera == null)
            _PlayerCamera = GameObject.FindGameObjectWithTag("MainCamera");

        if(GameManager.state == GameState.Interacting)
        {
            _camera.gameObject.SetActive(true);
            _PlayerCamera.SetActive(false);
            _isInteracting = !_isInteracting;
            MoveCamera(_camera.transform.position,targetPosition.position);

        }
        else
        {

            MoveCamera(_oldPosition, _camera.transform.position);
            _camera.gameObject.SetActive(false);
            _PlayerCamera.SetActive(true);


        }

    }

    void MoveCamera(Vector3 from, Vector3 to)
    {
        float step = speed * Time.deltaTime;
        _currentPosition = Vector3.Lerp(from, to, step);
        _camera.transform.position = _currentPosition;
    }
}
