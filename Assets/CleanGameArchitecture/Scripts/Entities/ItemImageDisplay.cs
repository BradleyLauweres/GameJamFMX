using Assets.CleanGameArchitecture.Scripts.Entities;
using Assets.CleanGameArchitecture.Scripts.Interfaces;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemImageDisplay : MonoBehaviour
{
    [Header("UI References")]
    public Canvas imageDisplayCanvas;
    public Canvas newsImageDisplayCanvas;
    public Image displayImage;
    public Image newsDisplayImage;
    public TMP_Text pageIndicator;
    public TMP_Text newsPageIndicator;

    [Header("Settings")]
    public KeyCode showImageKey = KeyCode.Mouse0;
    public KeyCode nextImageKey = KeyCode.RightArrow;
    public KeyCode prevImageKey = KeyCode.LeftArrow; 
    public KeyCode closeImageKey = KeyCode.Mouse1; 

    private ItemHolder itemHolder;

    private IItemWithImages currentItemWithImages;
    private int currentImageIndex = 0;

    private bool isDisplayActive = false;

    void Start()
    {
        itemHolder = GetComponent<ItemHolder>();
        if (itemHolder == null)
        {
            Debug.LogError("ItemImageDisplay requires an ItemHolder component on the same GameObject");
            enabled = false;
            return;
        }

        if (imageDisplayCanvas == null || displayImage == null)
        {
            Debug.LogError("ImageDisplayCanvas or DisplayImage not assigned in ItemImageDisplay");
            enabled = false;
            return;
        } 
        if (newsImageDisplayCanvas == null || displayImage == null)
        {
            Debug.LogError("ImageDisplayCanvas or DisplayImage not assigned in ItemImageDisplay");
            enabled = false;
            return;
        }

        imageDisplayCanvas.gameObject.SetActive(false);
        newsImageDisplayCanvas.gameObject.SetActive(false);
    }

    void Update()
    {
        if (itemHolder.IsHoldingItem())
        {
            if (currentItemWithImages == null)
            {
                GameObject heldItem = itemHolder.GetHeldItem();
                if (heldItem != null)
                {
                    currentItemWithImages = heldItem.GetComponent<IItemWithImages>();
                }
            }

            if (currentItemWithImages != null)
            {
                if (Input.GetKeyDown(showImageKey) && !isDisplayActive)
                {
                    if(itemHolder.GetHeldItem().name == "NewspaperInteractable")
                    {
                        ShowImageNewsPaper();
                    }
                    else
                    {
                        ShowImage();
                    }
                        
                }

                if (isDisplayActive)
                {
                    if (Input.GetKeyDown(nextImageKey) || Input.GetKeyDown(KeyCode.Mouse0))
                    {
                        ShowNextImage();
                    }

                    if (Input.GetKeyDown(prevImageKey))
                    {
                        ShowPreviousImage();
                    }

                    if (Input.GetKeyDown(KeyCode.Mouse1))
                    {
                        HideDisplay();
                    }
                }
            }
        }
        else
        {
            if (currentItemWithImages != null)
            {
                currentItemWithImages = null;

                if (isDisplayActive)
                {
                    HideDisplay();
                }
            }
        }
    }

    void ShowImage()
    {
        if (currentItemWithImages != null)
        {
            Sprite[] images = currentItemWithImages.GetImages();

            if (images != null && images.Length > 0)
            {
                currentImageIndex = 0;

                displayImage.sprite = images[currentImageIndex];

                if (pageIndicator != null)
                {
                    pageIndicator.text = (currentImageIndex + 1) + " / " + images.Length;
                }

                imageDisplayCanvas.gameObject.SetActive(true);
                isDisplayActive = true;

            }
        }
    }

    void ShowImageNewsPaper()
    {
        if (currentItemWithImages != null)
        {
            Sprite[] images = currentItemWithImages.GetImages();

            if (images != null && images.Length > 0)
            {
                currentImageIndex = 0;

                newsDisplayImage.sprite = images[currentImageIndex];

                if (pageIndicator != null)
                {
                    pageIndicator.text = (currentImageIndex + 1) + " / " + images.Length;
                }

                newsImageDisplayCanvas.gameObject.SetActive(true);
                isDisplayActive = true;

            }
        }
    }

    void ShowNextImage()
    {
        if (currentItemWithImages != null)
        {
            Sprite[] images = currentItemWithImages.GetImages();

            if (images != null && images.Length > 0)
            {
                currentImageIndex = (currentImageIndex + 1) % images.Length;

                displayImage.sprite = images[currentImageIndex];

                if (pageIndicator != null)
                {
                    pageIndicator.text = (currentImageIndex + 1) + " / " + images.Length;
                }
            }
        }
    }

    void ShowPreviousImage()
    {
        if (currentItemWithImages != null)
        {
            Sprite[] images = currentItemWithImages.GetImages();

            if (images != null && images.Length > 0)
            {
                currentImageIndex--;
                if (currentImageIndex < 0)
                    currentImageIndex = images.Length - 1;

                displayImage.sprite = images[currentImageIndex];

                if (pageIndicator != null)
                {
                    pageIndicator.text = (currentImageIndex + 1) + " / " + images.Length;
                }
            }
        }
    }

    void HideDisplay()
    {
        imageDisplayCanvas.gameObject.SetActive(false);
        newsImageDisplayCanvas.gameObject.SetActive(false);
        isDisplayActive = false;
    }
}


