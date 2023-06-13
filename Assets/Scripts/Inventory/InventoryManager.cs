using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public Player player;
    //For now, this will store information of the Items that can be added to the inventory
    public List<ItemData> itemDatabase;

    //Store all the inventory slots in the scene here
    public List<InventorySlot> inventorySlots;

    //Store all the equipment slots in the scene here
    public List<EquipmentSlot> equipmentSlots;

    //Singleton implementation. Do not change anything within this region.
    #region SingletonImplementation
    private static InventoryManager instance = null;
    public static InventoryManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<InventoryManager>();
                if (instance == null)
                {
                    GameObject go = new GameObject();
                    go.name = "Inventory";
                    instance = go.AddComponent<InventoryManager>();
                    DontDestroyOnLoad(go);
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #endregion

    
    public void UseItem(ItemData data)
    {
        // TODO
        // If the item is a consumable, simply add the attributes of the item to the player.
        // If it is equippable, get the equipment slot that matches the item's slot.
        // Set the equipment slot's item as that of the used item
        if(data.type == ItemType.Consumable)
        {
            //increaase Hp/Mp based on Hp_Potion/Mp_Potion value
            for (int att = 0; att < itemDatabase.Count - 1; att++)
            {
                for (int i = 0; i < itemDatabase.Count - 1; i++)
                {
                    if (data.id == itemDatabase[i].id && player.attributes[att].type == itemDatabase[i].attributes[0].type)
                    {
                        player.attributes[att].value += itemDatabase[i].attributes[0].value;
                        if (player.attributes[att].value > 100) player.attributes[att].value = 100;
                    }
                }
            }
            
            Debug.Log($"Used {data.id}");
        }
        else if(data.type == ItemType.Equipabble)//equipables
        {
            //Check for equipment slot type
            int slot = GetEquipmentSlot(data.slotType), inventorySlot = GetEmptyInventorySlot();//For swapping
            if(equipmentSlots[slot].HasItem())
            {
                if(inventorySlot != -1)
                {
                    equipmentSlots[slot].Unequip();
                    equipmentSlots[slot].SetItem(data);
                }
                else Debug.Log("No More Inventory Spaces");
            }
            else
            {
                equipmentSlots[slot].SetItem(data);
            }
            //Increase Player Attribute based Equipment Attribute
            for (int att = 0; att < itemDatabase.Count - 1; att++)
            {
                for (int i = 0; i < itemDatabase.Count - 1; i++)
                {
                    if (data.id == itemDatabase[i].id && player.attributes[att].type == itemDatabase[i].attributes[0].type)
                    {
                        player.attributes[att].value += itemDatabase[i].attributes[0].value;
                    }
                }
            }

            Debug.Log($"Equiped {data.id} into equipmentSlot index {slot}");
        }
        //key
        else { }
    }


    public void AddItem(string itemID)
    {
        //1. Cycle through every item in the database until you find the item with the same id.
        //2. Get the index of the InventorySlot that does not have any Item and set its Item to the Item found

        int index = GetEmptyInventorySlot();
        Debug.Log($"Empty inventory slot index: {index}");

        for (int i = 0; i < itemDatabase.Count; i++)
        {
            if (itemDatabase[i].id == itemID)
            {
                Debug.Log($"Setting inventorySlot index: {index} with itemDatabase: {itemDatabase[i]}");
                inventorySlots[index].SetItem(itemDatabase[i]);
            }
        }
    }

    public int GetEmptyInventorySlot()
    {
        //Check which inventory slot doesn't have an Item and return its index
        for(int i = 0; i < itemDatabase.Count - 1; i++)
        {
            if (!inventorySlots[i].HasItem())
                return i;
        }
        return -1;
    }

    public int GetEquipmentSlot(EquipmentSlotType type)
    {
        //Check which equipment slot matches the slot type and return its index
        for (int i = 0; i < equipmentSlots.Count; i++)
        {
            if (equipmentSlots[i].type == type)
                return i;
        }
        return -1;
    }

    public bool WithKey()
    {
        int index = GetEmptyInventorySlot();
        if (index < 0) index = itemDatabase.Count - 1;
        for (int i = 0; i < index; i++)
        {
            if(inventorySlots[i].checkKey() == true)
            return true;
        }
        return false;
    }
}
