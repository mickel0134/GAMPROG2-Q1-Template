using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentSlot : MonoBehaviour
{
    [SerializeField] private Image defaultIcon;
    [SerializeField] private Image itemIcon;
    public EquipmentSlotType type;
    private string id;

    public InventoryManager inventoryManager;

    public void SetItem(ItemData data)
    {
        // TODO
        // Set the item data the and icons here
        // Make sure to apply the attributes once an item is equipped
        Debug.Log($"Equiping {data.icon} to {itemIcon.sprite}");
        itemIcon.sprite = data.icon;
        itemIcon.enabled = true;
        defaultIcon.enabled = false;

        id = data.id;
    }

    public void Unequip()
    {
        // Check if there is an available inventory slot before removing the item.
        // Make sure to return the equipment to the inventory when there is an available slot.
        // Reset the item data and icons here
        int checker = inventoryManager.GetEmptyInventorySlot();
        if (checker != -1)
        {
            Debug.Log($"Unequiping {id}");
            itemIcon.sprite = null;
            itemIcon.enabled = false;
            defaultIcon.enabled = true;

            //Removes equipment attribute from player
            for (int att = 0; att < inventoryManager.itemDatabase.Count; att++)
            {
                for (int i = 0; i < inventoryManager.itemDatabase.Count; i++)
                {
                    if (id == inventoryManager.itemDatabase[i].id && inventoryManager.player.attributes[att].type == 
                        inventoryManager.itemDatabase[i].attributes[0].type)
                    {
                        inventoryManager.player.attributes[att].value -= inventoryManager.itemDatabase[i].attributes[0].value;
                    }
                }
            }

            inventoryManager.AddItem(id);
        }
        else Debug.Log($"Cant unequip {id} due to lack of inventory space");
        

    }
    public bool HasItem()
    {
        return itemIcon.sprite != null;
    }
}
