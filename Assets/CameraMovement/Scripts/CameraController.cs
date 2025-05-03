using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField] Camera _camera;
     
    public Transform targetPosition; 
    public float speed = 5f; 
    private Vector3 _currentPosition; 

    bool _isInteracting = false;

    void Start()
    {
        _currentPosition = transform.position; 
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            _isInteracting = true;
        }

        if (_isInteracting)
        {
            float step = speed * Time.deltaTime;
            _currentPosition = Vector3.Lerp(_camera.transform.position, targetPosition.position, step);
            _camera.transform.position = _currentPosition;
        }
    }
}
