using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayInfo : MonoBehaviour
{

    [SerializeField] private GameObject UI;
    [SerializeField] private TMP_Text displayText;
    [SerializeField] private string Text;
    [SerializeField] GameObject CrossHair;

    private bool isOpen = false;

    private void Awake()
    {
        displayText.text = Text;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0) && GameManager.Instance.state == GameState.Playing)
        {
            OpenTextDisplay();
        }
       
    }

    public void OpenTextDisplay()
    {
        isOpen = !isOpen;
        UI.SetActive(isOpen);
        CrossHair.SetActive(!isOpen);
        
    }
}
