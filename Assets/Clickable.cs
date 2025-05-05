using Assets.Scripts.Enums;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Clickable : MonoBehaviour
{
    [SerializeField] Transform postnotePlaceholder;
    [SerializeField] Transform startPositionPlaceholder;
    private Transform originalPlace;

    private Vector3 _currentPos;
    private bool _clicked;
    private bool _onPlace = false;

    private PostnoteType _myType;

    private void Awake()
    {
        originalPlace = transform;
        
    }

    private void OnMouseDown()
    {
        if(GameManager.Instance.state != GameState.Endgame)
        {
            _clicked = true;
            _myType = GetComponent<Postnote>()._type;
            Debug.Log("clicked on" + gameObject.name);
        }
        
    }

    private void Update()
    {
      
        if (!GameService.Instance.isMurdererSelected && _myType == PostnoteType.Killer && !_onPlace)
        {
            if (_clicked )
            {
                MoveTo();
            }
        }
        
        if(GameService.Instance.isMurdererSelected && _myType == PostnoteType.Killer && _onPlace)
        {
            if (_clicked  )
            {
                MoveFrom();
            }
        }

        if (!GameService.Instance.isMurderWeaponSelected && _myType == PostnoteType.Weapon && !_onPlace)
        {

            if (_clicked )
            {
                MoveTo();
            }
        }
        
        if(GameService.Instance.isMurderWeaponSelected && _myType == PostnoteType.Weapon && _onPlace)
        {
            if (_clicked )
            {
                MoveFrom();
            }
        }


       
    }

    private void MoveTo()
    {
        float step = 20f * Time.deltaTime;
        _currentPos = Vector3.Lerp(gameObject.transform.position, postnotePlaceholder.transform.position, step);
        gameObject.transform.position = _currentPos;
        if (gameObject.transform.position == postnotePlaceholder.transform.position)
        {
            if(gameObject.GetComponent<Postnote>()._type == Assets.Scripts.Enums.PostnoteType.Killer)
            {
                GameService.Instance.SelectedMurderer = gameObject;
                GameService.Instance.isMurdererSelected = true;
            }
            else
            {
                GameService.Instance.SelectedMurderWeapon = gameObject;
                GameService.Instance.isMurderWeaponSelected = true;
            }
                _onPlace = true;
            _clicked = false;
        }
    }

    private void MoveFrom()
    {
        float step = 20f * Time.deltaTime;
        _currentPos = Vector3.Lerp(gameObject.transform.position, startPositionPlaceholder.transform.position, step);
        gameObject.transform.position = _currentPos;
        if (gameObject.transform.position == startPositionPlaceholder.transform.position)
        {
            if (gameObject.GetComponent<Postnote>()._type == Assets.Scripts.Enums.PostnoteType.Killer)
            {
                GameService.Instance.SelectedMurderer = null;
                GameService.Instance.isMurdererSelected= false;
            }
            else
            {
                GameService.Instance.SelectedMurderWeapon = null;
                GameService.Instance.isMurderWeaponSelected = false;
            }

            _onPlace = false;
            _clicked = false;
        }
    }
}
