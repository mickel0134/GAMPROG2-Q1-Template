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
        // TODO
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
            inventoryManager.AddItem(id);
        }
        else Debug.Log($"Cant unequip {id} due to lack of inventory space");
        

    }
    public bool HasItem()
    {
        return itemIcon != null;
    }
}
