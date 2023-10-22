using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{

    public static InventoryManager instance; 

    public int horizontalItemCount = 8;
    public int verticalItemCount = 3;
    public List<ItemPrefab> itemList = new List<ItemPrefab>();
    public List<Item> items;
    public GameObject itemPrefab;
    public Transform content;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
        FillInventory();
        GeneradeCellID();
    }


    void GeneradeCellID()
    {
        for(int i = 0; i < content.childCount; i++)
        {
            ItemPrefab cellPrefab = content.GetChild(i).GetComponent<ItemPrefab>();
            cellPrefab.id = "Cell " + i; 
        }
    }

    public void Create(Item item)
    {
        Tuple<bool, int> tuple = GetFreeCell();
        if (tuple.Item1)
        {
            itemList[tuple.Item2].currentItem = item;
            UpdateCells();
        }
    }

    public Tuple<bool, int> GetFreeCell()
    {
        for (int i = 0; i < itemList.Count; i++)
        {
            if (itemList[i].currentItem == null)
            {
                return Tuple.Create(true, i);
            }
        }
        return Tuple.Create(false, 0);
    }

    public void AddItem(Item item, int cellID)
    {
        itemList[cellID].currentItem = item;
    }

    public void FillInventory()
    {
        for (int i = 0; i < verticalItemCount * horizontalItemCount; i++)
        {
            ItemPrefab prefab = Instantiate(itemPrefab, content).GetComponent<ItemPrefab>();
            itemList.Add(prefab);
        }
    }

    public void UpdateCells()
    {
        for (int i = 0; i < itemList.Count; i++)
        {
            itemList[i].UpdateCell();
        }
    }
}
