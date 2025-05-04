using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameService : MonoBehaviour
{
    private static GameService _instance;

    public static GameService Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.Log("GameService is NULL");
            }
            return _instance;
        }
    }

    private string killerAnswer = "Clara Blair";
    private string murderWeaponAnswer = "Lily of the valley";

    public GameObject SelectedMurderWeapon;
    public GameObject SelectedMurderer;

    public bool isMurderWeaponSelected;
    public bool isMurdererSelected;

    private void Awake()
    {
        _instance = this;
    }

    private void Update()
    {
        CheckIfSelected();
    }

    public void CheckIfSelected()
    {
        if(SelectedMurderer != null)
            isMurdererSelected = true;
        else
            isMurdererSelected = false;

        if (SelectedMurderWeapon != null)
            isMurderWeaponSelected = true;
        else
            isMurderWeaponSelected = false;
    }

    public void CheckEndResult()
    {


        if(SelectedMurderer.GetComponent<Postnote>().name == killerAnswer)
        {

        }

        if(SelectedMurderWeapon.GetComponent<Postnote>().name == murderWeaponAnswer)
        {

        }
    }


}
