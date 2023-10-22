using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemPrefab : MonoBehaviour
{
    public Item currentItem;
    public Image itemImage;


    public string id;
    public string itemName = "";

    // Start is called before the first frame update
    void Awake()
    {
        itemImage = GetComponentsInChildren<Image>()[1];
        UpdateCell();
    }

    // Update is called once per frame
    public void UpdateCell()
    {
        if(currentItem && currentItem.itemSprite)
        {
            itemImage.sprite = currentItem.itemSprite;
            itemImage.color = Color.white;
            itemName = currentItem.itemName;
        }
        else
        {
            itemImage.sprite = null;
            itemImage.color = Color.clear;
            itemName = "";
        }
    }
}
