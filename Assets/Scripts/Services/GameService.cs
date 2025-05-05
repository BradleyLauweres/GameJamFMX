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
    private string murderWeaponAnswer = "Belladonna";

    [SerializeField] private GameObject EndGameScreenWin;
    [SerializeField] private GameObject EndGameScreenLose;

    public GameObject SelectedMurderWeapon;
    public GameObject SelectedMurderer;

    public bool isMurderWeaponSelected;
    public bool isMurdererSelected;

    public bool rightAnswersSelected = false;

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
        if(SelectedMurderer == null || SelectedMurderWeapon == null)
        {
            Cursor.lockState = CursorLockMode.None;
            InteractManager.Instance._pc.ToggleEndGameText(false);
            InteractManager.Instance._pc.ToggleEndGamePlaceholder(false);
            EndGameScreenLose.SetActive(true);
            return;
        }

        if(SelectedMurderer.GetComponent<Postnote>()._name == killerAnswer && SelectedMurderWeapon.GetComponent<Postnote>()._name == murderWeaponAnswer)
        {
            Cursor.lockState = CursorLockMode.None;
            InteractManager.Instance._pc.ToggleEndGameText(false);
            InteractManager.Instance._pc.ToggleEndGamePlaceholder(false);
            EndGameScreenWin.SetActive(true);
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            InteractManager.Instance._pc.ToggleEndGameText(false);
            InteractManager.Instance._pc.ToggleEndGamePlaceholder(false);
            EndGameScreenLose.SetActive(true);
        }

    }


}
