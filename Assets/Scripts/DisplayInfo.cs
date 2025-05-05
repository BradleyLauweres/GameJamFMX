using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayInfo : MonoBehaviour
{
    private static DisplayInfo _instance;

    public static DisplayInfo Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.Log("DisplayInfo is NULL");
            }
            return _instance;
        }
    }


    [SerializeField] private GameObject UI;
    [SerializeField] private TMP_Text displayText;
    [SerializeField] private string Text;
    [SerializeField] GameObject CrossHair;
    [SerializeField] GameObject ThisObject;

    public bool isOpen = false;

    private void Awake()
    {
        _instance = this;
        displayText.text = Text;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0) && GameManager.Instance.state == GameState.Playing && ThisObject.activeSelf)
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
