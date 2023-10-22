using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveInventory : MonoBehaviour, IDataPersistence
{
    List<ItemPrefab> itemList = new List<ItemPrefab>();
    public List<Item> items = new List<Item>();

    // Start is called before the first frame update
    void Awake()
    {
        itemList = InventoryManager.instance.itemList;
        items = InventoryManager.instance.items;
    }

    public void LoadData(GameData data)
    {
        Debug.Log("Score " + data.score);
        //Debug.Log("LoadData");
        for (int i = 0; i < itemList.Count; i++)
        {
            Debug.Log(itemList.Count + "item list count");
            foreach (var pair in data.inventoryCollection)
            {
                Debug.Log(pair.Key + "before KEY");
                Debug.Log(itemList[i].id + "before ID");
                if (pair.Key == itemList[i].id && data.inventoryCollection.ContainsKey(itemList[i].id))
                {
                    Debug.Log(pair.Key + "eeee");
                    Item addSavedItem;
                    for (int j = 0; j < items.Count; j++)
                    {

                        if (pair.Value == items[j].itemName)
                        {
                            addSavedItem = items[j];
                            InventoryManager.instance.Create(addSavedItem);
                        }
                    }

                }
            }
        }
    }

    public void SaveData(ref GameData data)
    {
        data.score = 111;
        //Debug.Log("SaveData");
        for (int i = 0; i < itemList.Count; i++)
        {
            if (data.inventoryCollection.ContainsKey(itemList[i].id))
            {
                data.inventoryCollection.Remove(itemList[i].id);
            }
            data.inventoryCollection.Add(itemList[i].id, itemList[i].itemName);
        }
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }
}
