using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitInteract : MonoBehaviour
{

    [SerializeField] Camera _animationPcCam;
    [SerializeField] Camera _animationWhiteBoardCam;
    [SerializeField] Camera _playerCam;

    void Update()
    {
        if (_playerCam == null)
            _playerCam = Camera.main;

        if (GameManager.Instance.state == GameState.Interacting && Input.GetKeyDown(KeyCode.E))
        {
            _playerCam.gameObject.SetActive(true);
            _animationPcCam.gameObject.SetActive(false);
            _animationWhiteBoardCam.gameObject.SetActive(false);
            GameManager.Instance.state = GameState.Playing;
        }
    }
}
