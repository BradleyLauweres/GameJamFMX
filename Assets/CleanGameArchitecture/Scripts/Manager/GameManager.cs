using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] private GameObject WinScreen;
    [SerializeField] private GameObject LoseScreen;

    private void Start()
    {
        if (WinScreen != null)
            WinScreen.SetActive(false);

        if(LoseScreen != null)
            LoseScreen.SetActive(false);
    }

    public void CheckGameResult()
    {
        if (PostitNoteManager.Instance.placeholders[0].currentNote == null || PostitNoteManager.Instance.placeholders[1].currentNote == null)
        {
            Lose();
        }
        else if (PostitNoteManager.Instance.placeholders[0].currentNote.name != "Wolfbane" || PostitNoteManager.Instance.placeholders[1].currentNote.name != "ClaraBlair")
        {
            Lose();
        }
        else if (PostitNoteManager.Instance.placeholders[0].currentNote.name == "Wolfbane" || PostitNoteManager.Instance.placeholders[1].currentNote.name == "ClaraBlair")
        {
            Win();
        }
    }

    public void Lose()
    {
        LoseScreen.SetActive (true);
    }

    public void Win()
    {
        WinScreen.SetActive (true);
    }
}
