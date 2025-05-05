using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

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

    [SerializeField] private Texture[] images;

    [SerializeField] private GameObject UI;
    [SerializeField] private GameObject ImageUI;
    [SerializeField] private TMP_Text displayText;
    [SerializeField] private string Text;
    [SerializeField] GameObject CrossHair;
    [SerializeField] GameObject ThisObject;

    public bool isOpen = false;
    public bool isImage;

    private int currentIndex = 0;

    private void Awake()
    {
        _instance = this;
        displayText.text = Text;
    }

    private void Start()
    {
        if (isImage)
        {
            ImageUI.GetComponent<RawImage>().texture = images[currentIndex];
        }
    }

    void Update()
    {
        if (isImage)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && GameManager.Instance.state == GameState.Playing && ThisObject.activeSelf)
            {
                OpenImageDisplay();
            }

            if (isOpen)
            {
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    ImageUI.GetComponent<RawImage>().texture = GetPreviousItem();
                }

                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    ImageUI.GetComponent<RawImage>().texture = GetNextItem();
                }
            }
        }

        if (!isImage)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && GameManager.Instance.state == GameState.Playing && ThisObject.activeSelf)
            {
                OpenTextDisplay();
            }
        }
       
    }

    public void OpenTextDisplay()
    {
        isOpen = !isOpen;
        UI.SetActive(isOpen);
        CrossHair.SetActive(!isOpen);
        
    }

    public void OpenImageDisplay()
    {
        isOpen = !isOpen;
        ImageUI.SetActive(isOpen);
        CrossHair.SetActive(!isOpen);
    }

    public Texture GetCurrentItem()
    {
        return images[currentIndex];
    }

    public Texture GetNextItem()
    {
        currentIndex = (currentIndex + 1) % images.Length;
        return images[currentIndex];
    }

    public Texture GetPreviousItem()
    {
        currentIndex = (currentIndex - 1 + images.Length) % images.Length;
        return images[currentIndex];
    }

    public void SetIndex(int index)
    {
        currentIndex = ((index % images.Length) + images.Length) % images.Length;
    }
}
