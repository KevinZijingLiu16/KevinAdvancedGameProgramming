using System.Collections.Generic;
using Inventory.Model;
using Inventory.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory.UI
{
    public class InventoryUIController : MonoBehaviour
    {
        [Header("Prefabs & References")]
        public GameObject slotPrefab; //actual prefab for the slot
        public Transform slotParent;  //the panel that contains all the slots
        public ItemTooltip tooltipReference; //reference to the tooltip in the scene
        [Header("Icons")]
        public Sprite emptySlotIcon;  
        private InventoryModel _inventoryModel;     
        private List<InventoryUISlot> _uiSlots;

        public UIDragIcon dragIcon; 
        //Set everything up
        //Store the model reference
        //loop through all InventorySlot s in the model
        //Initiate the prefab and set the index
        public void Initialize(InventoryModel model)
        {
            // Store the reference to the InventoryModel instance
            _inventoryModel = model;
            // Initialize the list that will hold the UI slots
            _uiSlots = new List<InventoryUISlot>();
            // Loop through each InventorySlot in the InventoryModel
            foreach (InventorySlot logicSlot in _inventoryModel.Slots)
            {
                // Instantiate a new slot UI element from the prefab and set the transform as the parent
                GameObject slotGO = Instantiate(slotPrefab, slotParent);
                // Get the InventoryUISlot component from the instantiated GameObject
                InventoryUISlot uiSlot = slotGO.GetComponent<InventoryUISlot>();
                uiSlot.SetIndex(logicSlot.Index);
                uiSlot.tooltip = tooltipReference;
                // Add the UI slot to the list of UI slots

                uiSlot.SetDragIcon(dragIcon);
                _uiSlots.Add(uiSlot);
                uiSlot.SetUIController(this);

            }

            RefreshUI();
        }

        public void RefreshUI()
        {
            for (int i = 0; i < _inventoryModel.Slots.Count; i++)
            {
                InventorySlot logicSlot = _inventoryModel.Slots[i];
                InventoryUISlot uiSlot = _uiSlots[i];

                if (logicSlot.IsEmpty)
                {
                    uiSlot.SetEmpty(emptySlotIcon);
                }
                else
                {
                    // If the logic slot is not empty, get the item data and quantity from the logic slot
                    var itemData = logicSlot.ItemStack.ItemData;
                    var quantity = logicSlot.ItemStack.Quantity;
                    uiSlot.SetItem(itemData.icon, quantity, itemData, logicSlot);

                }
            }
        }

        public void SwapItems(int fromIndex, int toIndex)
        {
            var fromSlot = _inventoryModel.Slots[fromIndex];
            var toSlot = _inventoryModel.Slots[toIndex];

            if (fromSlot.IsEmpty) return;

            
            if (!toSlot.IsEmpty &&
                fromSlot.ItemStack.ItemData == toSlot.ItemStack.ItemData &&
                !toSlot.ItemStack.IsFull)
            {
                int space = toSlot.ItemStack.ItemData.maxStackSize - toSlot.ItemStack.Quantity;
                int transferAmount = Mathf.Min(space, fromSlot.ItemStack.Quantity);

                toSlot.ItemStack.Add(transferAmount);
                fromSlot.Reduce(transferAmount);

                if (fromSlot.ItemStack.Quantity <= 0)
                    fromSlot.Clear();
            }
            
            else
            {
                var temp = fromSlot.ItemStack;
                fromSlot.AssignItem(toSlot.ItemStack);
                toSlot.AssignItem(temp);
            }

            RefreshUI();
        }

    }
}
