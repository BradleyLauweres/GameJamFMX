using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField] private Camera _camera;
    [SerializeField] private KeyCode _key = KeyCode.V;
     
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
        if (Input.GetKeyDown(KeyCode.V))
        {
            _isInteracting = !_isInteracting;

            
        }

        if (_isInteracting)
        {
            MoveCamera(_camera.transform.position, targetPosition.position);
        }
        else
        {
            if (_camera.transform.position != _oldPosition)
                MoveCamera(_oldPosition, _camera.transform.position);
        }



    }

    void MoveCamera(Vector3 from, Vector3 to)
    {
        float step = speed * Time.deltaTime;
        _currentPosition = Vector3.Lerp(from, to, step);
        _camera.transform.position = _currentPosition;
    }
}
