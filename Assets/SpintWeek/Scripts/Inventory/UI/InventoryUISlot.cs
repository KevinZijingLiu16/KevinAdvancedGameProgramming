using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using Inventory.Data;
using Inventory.Model;



namespace Inventory.UI
{
    public class InventoryUISlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
       
        [SerializeField] private Image iconImage; 
        [SerializeField] private TextMeshProUGUI quantityText; 
        public ItemTooltip tooltip;
        private UIDragIcon _dragIcon;

        private InventoryItemData _itemData;
        private InventorySlot _logicSlot;
        private InventoryUIController _uiController;


        public static InventoryUISlot DraggedSlot { get; private set; } // Static reference to store the dragged slot

        // This is the index of the slot in the inventory
        private int _index;
        //this function will be called in the InventoryUIController to set the index of the slot when the actual slot prefab is created
        public void SetIndex(int index)
        {
            _index = index;
        }
        // This function will be called in the InventoryUIController when refreshing the UI
        public void SetItem(Sprite icon, int quantity, InventoryItemData itemData, InventorySlot logicSlot)
        {
            iconImage.sprite = icon;
            iconImage.color = Color.white;
            quantityText.text = quantity >= 1 ? quantity.ToString() : "";
            _itemData = itemData;
            _logicSlot = logicSlot;  
        }

        public void SetEmpty(Sprite emptyIcon)
        {
            iconImage.sprite = emptyIcon;
            iconImage.color = new Color(1, 1, 1, 0.3f);
            quantityText.text = "";
            _itemData = null;
        }
        // GetIndex will be called in the InventoryUIController when the player wants to remove an item from the inventory or drag and drop
        public int GetIndex()
        {
            return _index;
        }
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (_itemData != null && tooltip != null)
            {
                tooltip.Show(_itemData);
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (tooltip != null)
            {
                tooltip.Hide();
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            DraggedSlot = this; // Store the dragged slot reference
            if (_logicSlot == null || _logicSlot.IsEmpty) return;

            var itemData = _logicSlot.ItemStack.ItemData;
            int quantity = _logicSlot.ItemStack.Quantity;

            _dragIcon.Show(itemData.icon, quantity);
        }


        public void OnDrag(PointerEventData eventData)
        {
           _dragIcon.UpdatePosition();
        }
        public void OnEndDrag(PointerEventData eventData)
        {
            DraggedSlot = null; // Clear the dragged slot reference
           
            _dragIcon.Hide();

            if (eventData.pointerEnter == null)
                return;

            var targetSlot = eventData.pointerEnter.GetComponentInParent<InventoryUISlot>();

            if (targetSlot == null || targetSlot == this)
                return;

            _uiController.SwapItems(GetIndex(), targetSlot.GetIndex());

        }

        public void SetUIController(InventoryUIController controller)
        {
            _uiController = controller;
        }


        public void SetDragIcon(UIDragIcon icon)
        {
            _dragIcon = icon;
        }


    }
}
