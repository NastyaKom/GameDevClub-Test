using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteItem : MonoBehaviour
{
    public void PressButton()
    {
        ItemPrefab itemPrefab = transform.parent.gameObject.GetComponent<ItemPrefab>();
        if (itemPrefab.currentItem)
        {
            BasketButton.instance.basketButton.SetActive(true);
            BasketButton.instance.deletedItem = gameObject;
        }
        else
        {
            BasketButton.instance.basketButton.SetActive(false);
            BasketButton.instance.deletedItem = null;
        }
    }
}
