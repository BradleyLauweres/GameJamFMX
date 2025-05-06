using Assets.CleanGameArchitecture.Scripts.Entities;
using Assets.CleanGameArchitecture.Scripts.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewsPaper : MonoBehaviour, IHoldable, IItemWithImages
{
    public string itemName = "NewsPaper";

    [Header("Images")]
    public Sprite[] documentPages;
    public string[] pageDescriptions;

    private Renderer itemRenderer;
    private Color originalColor;

    void Start()
    {
        itemRenderer = GetComponent<Renderer>();
        if (itemRenderer != null)
        {
            originalColor = itemRenderer.material.color;
        }

        if (pageDescriptions == null || pageDescriptions.Length != documentPages.Length)
        {
            pageDescriptions = new string[documentPages.Length];
            for (int i = 0; i < documentPages.Length; i++)
            {
                pageDescriptions[i] = "Page " + (i + 1);
            }
        }
    }

    public void OnPickup(ItemHolder holder)
    {
       
    }

    public void OnDrop()
    {
        Debug.Log("Document dropped.");

        if (itemRenderer != null)
        {
            itemRenderer.material.color = originalColor;
        }
    }

    public string GetItemName()
    {
        return itemName;
    }

    public Sprite[] GetImages()
    {
        return documentPages;
    }

    public string GetImageDescription(int index)
    {
        if (pageDescriptions != null && index >= 0 && index < pageDescriptions.Length)
        {
            return pageDescriptions[index];
        }
        return "Page " + (index + 1);
    }
}
