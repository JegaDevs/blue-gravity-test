using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Jega.BlueGravity.Inventory;

namespace Jega.BlueGravity
{
    public class ClothingInventory : Inventory
    {
        private const int HeadSlotIndex = 0;
        private const int BodySlotIndex = 1;
        protected override void OnEnable()
        {
            base.OnEnable();
            sessionService.RegisterClothingInventory(this);
        }

        public bool CheckIfSwitchIsValid(InventoryItem item, int slotIndex)
        {
            if (item is not ClothingItem clothing) return false;
            if(clothing.Type == ClothingItem.ClothingType.Head)
                return slotIndex == HeadSlotIndex;
            else
                return slotIndex == BodySlotIndex;
        }

        protected override void GainItemAmount(InventoryItem item, int amount)
        {
            int previousOwned = item.GetCustomSavedAmount(InventorySaveKey, 0);
            int newOwned = previousOwned + amount;
            item.SetCustomSavedAmount(InventorySaveKey, newOwned);
            ItemPair itemPair = new ItemPair(item);
            if (item is ClothingItem clothingItem)
            {
                if (clothingItem.Type == ClothingItem.ClothingType.Body)
                    SetSlot(BodySlotIndex, itemPair);
                else
                    SetSlot(HeadSlotIndex, itemPair);
            }

            void SetSlot(int slotIndex, ItemPair itemPair)
            {
                Slot currentSlot = slots[slotIndex];
                int storedItemIndex = ItemCollection.IndexOf(itemPair.Item);
                slots[slotIndex] = new Slot(currentSlot.UISlot, currentSlot.Index, itemPair, InventorySaveKey, storedItemIndex);
                UpdateSlotVisual(slots[BodySlotIndex].UISlot, itemPair, slotIndex);
            }
        }
    }
}
