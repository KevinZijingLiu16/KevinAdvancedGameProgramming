using UnityEngine;
using Inventory.Data;
using NUnit.Framework;
using System.Collections.Generic;
namespace Inventory.Model
{
    public class InventoryModel 
    {

        //Property store a list of inventory slots.
       public List<InventorySlot> Slots { get; private set; }
        // Constructor, initialize the list (empty) then create slotCount number of slots with unique indexes, if slotCount is 10, indexes 0 to 9
        public InventoryModel(int slotCount)
        {
            Slots = new List<InventorySlot>();

            for (int i = 0; i < slotCount; i++)
            {
                Slots.Add(new InventorySlot(i));
            }

        }
        // Function to add an item or stack of items to the inventory
        public bool TryAddItem(InventoryItemData itemData, int amount)
        {
            // Try to stack into existing slot first
            //check is not empty
            //check has the same item type
            //check is not full
            foreach (var slot in Slots)
            {
                if (!slot.IsEmpty &&
                    slot.ItemStack.ItemData == itemData &&
                    !slot.ItemStack.IsFull)
                {
                    int spaceLeft = itemData.maxStackSize - slot.ItemStack.Quantity;
                    int addAmount = System.Math.Min(spaceLeft, amount);
                    slot.ItemStack.Add(addAmount);
                    amount -= addAmount;

                    if (amount <= 0) return true;
                }
            }

            // Add to empty slots
            foreach (var slot in Slots)
            {
                if (slot.IsEmpty)
                {
                    int addAmount = System.Math.Min(itemData.maxStackSize, amount);
                    slot.AssignItem(new InventoryItemStack(itemData, addAmount));
                    amount -= addAmount;

                    if (amount <= 0) return true;
                }
            }

            return false; // not enough space
        }
        // this function will be called when the player wants to remove an item from the inventory by the index of the slot
        public void RemoveItemAt(int slotIndex)
        {
            //check if the index is valid
            if (slotIndex >= 0 && slotIndex < Slots.Count)
            {
                Slots[slotIndex].Clear();
            }
        }
    }


}
