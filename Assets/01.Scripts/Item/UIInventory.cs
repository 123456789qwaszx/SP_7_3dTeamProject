using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIInventory : MonoBehaviour
{
    public ItemSlot[] slots;

    public Transform slotPanel;


    EquipmentData selectedItem;
    int selectedItemIndex = 0;

    public Resource resource;


    void Start()
    {
        Managers.Player.Player.addItem += AddItem;


        slots = new ItemSlot[slotPanel.childCount];
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = slotPanel.GetChild(i).GetComponent<ItemSlot>();
            slots[i].index = i;
            slots[i].inventory = this;
        }
    }


    void AddItem()
    {
        EquipmentData data = Managers.Player.Player.itemData;

        ItemSlot emptySlot = GetEmptySlot(data);

        // 있다면
        if (emptySlot != null)
        {
            emptySlot.item = data;
            emptySlot.quantity = 1;
            UpdateUI();
            Managers.Player.Player.itemData = null;
            return;
        }

        Managers.Player.Player.itemData = null;
    }


    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null)
            {
                slots[i].Set();
            }
            else
            {
                slots[i].Clear();
            }
        }
    }


    ItemSlot GetItemStack(EquipmentData data)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == data)
            {
                return slots[i];
            }
        }
        return null;
    }


    ItemSlot GetEmptySlot(EquipmentData data)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                return slots[i];
            }
        }

        return null;
    }


    public void SelectItem(int index)
    {
        if (slots[index].item == null)
        {
            return;
        }

        selectedItem = slots[index].item;
        selectedItemIndex = index;
    }

    public void OnUseButton()
    {
        if (selectedItem.type == EquipmenType.Weapon)
        {
            for (int i = 0; i < selectedItem.weaponDamages.Length; i++)
            {
                switch (selectedItem.weaponDamages[i].type)
                {
                    case DamageType.ForMonster:
                        resource.resourceCondition.Add(selectedItem.weaponDamages[i].value);
                        break;
                    case DamageType.ForResource:
                        resource.resourceCondition.Add(selectedItem.weaponDamages[i].value);
                        break;
                }
            }
        }
    }
}
