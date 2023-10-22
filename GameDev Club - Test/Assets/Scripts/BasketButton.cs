using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasketButton : MonoBehaviour
{
    public static BasketButton instance;
    [HideInInspector] public GameObject basketButton;
    [HideInInspector] public GameObject deletedItem;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this; 
        }
        else
        {
            Destroy(this); 
        }
        basketButton = transform.GetChild(0).gameObject;
    }

    public void DeleteItem()
    {
        ItemPrefab parentItem = deletedItem.gameObject.transform.parent.transform.gameObject.GetComponent<ItemPrefab>();
        parentItem.currentItem = null;
        InventoryManager.instance.UpdateCells();
    }
}
