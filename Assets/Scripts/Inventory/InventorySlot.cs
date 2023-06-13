using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    private ItemData itemData;
    public Image itemIcon;

    public void SetItem(ItemData data)
    {
        // TODO
        // Set the item data the and icons here
        
        itemData = data;
        itemIcon.sprite = data.icon;
        itemIcon.enabled = true;
        Debug.Log($"Setting itemIcon sprite: {data.icon}");
        //slot.SetItem(itemData);
    }

    public void UseItem()
    {
        int slotChecker = InventoryManager.Instance.GetEmptyInventorySlot(),
            equipmentSlot = InventoryManager.Instance.GetEquipmentSlot(itemData.slotType);

        
        if(itemData.type == ItemType.Key)
        {
            Debug.Log("Cannot be consumed or equip");
        }
        else if(itemData.type == ItemType.Consumable)
        {
            InventoryManager.Instance.UseItem(itemData);
            itemData = null;
            itemIcon.sprite = null;
            itemIcon.enabled = false;
        }
        else
        {
            if(slotChecker == -1 && InventoryManager.Instance.equipmentSlots[equipmentSlot].HasItem())
                Debug.Log("Cannot be consumed or equip");
            else
            {
                InventoryManager.Instance.UseItem(itemData);
                itemData = null;
                itemIcon.sprite = null;
                itemIcon.enabled = false;
            }
        }
    }

    public bool HasItem()
    {
        return itemData != null;
    }

    public bool checkKey()
    {
        if (itemData.type == ItemType.Key) return true;
        return false;
    }
}
