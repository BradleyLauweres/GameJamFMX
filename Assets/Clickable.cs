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
    private bool _onPlace;

    private PostnoteType _myType;

    private void Awake()
    {
        originalPlace = transform;
        
    }

    private void OnMouseDown()
    {
        _clicked = true;
        _myType = GetComponent<Postnote>()._type;
        Debug.Log("clicked on" + gameObject.name);
    }

    private void Update()
    {
      
        if (!GameService.Instance.isMurdererSelected && _myType == PostnoteType.Killer)
        {
            if (_clicked & !_onPlace)
            {
                MoveTo();
            }
        }
        else if(GameService.Instance.isMurdererSelected && _myType == PostnoteType.Killer)
        {
            if (_clicked && _onPlace)
            {
                MoveFrom();
            }
        }

        if (!GameService.Instance.isMurderWeaponSelected && _myType == PostnoteType.Weapon)
        {

            if (_clicked & !_onPlace)
            {
                MoveTo();
            }
        }
        else if(GameService.Instance.isMurderWeaponSelected && _myType == PostnoteType.Weapon)
        {
            if (_clicked && _onPlace)
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
            }
            else
            {
                GameService.Instance.SelectedMurderWeapon = gameObject;
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
            }
            else
            {
                GameService.Instance.SelectedMurderWeapon = null;
            }

            _onPlace = false;
            _clicked = false;
        }
    }
}
