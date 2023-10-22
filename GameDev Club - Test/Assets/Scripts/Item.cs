using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="InventoryItem", menuName ="ScriptableObjects/InventoryItem", order = 1)]
public class Item : ScriptableObject
{
    public string itemName = "Item";
    public Sprite itemSprite;
    public GameObject itemPrefab;
}
