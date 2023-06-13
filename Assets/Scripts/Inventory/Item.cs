using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : Interactable
{
    [SerializeField] private InventoryManager inventoryManager;

    public override void Interact()
    {
        // TODO: Add the item to the inventory. Make sure to destroy the prefab once the item is collected 
        Debug.Log("Interacted with object");

        int inventorySLot = inventoryManager.GetEmptyInventorySlot();
        if (inventorySLot == -1)
        {
            Debug.Log($"Cant pickup {id} due to lack of inventory space");
        }
        else
        {
            inventoryManager.AddItem(id);
            Destroy(this.gameObject);
        }
        

        
    }
}
