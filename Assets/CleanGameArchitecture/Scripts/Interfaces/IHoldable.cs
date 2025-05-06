using Assets.CleanGameArchitecture.Scripts.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHoldable
{
    void OnPickup(ItemHolder holder);
    void OnDrop();
    string GetItemName();
}
