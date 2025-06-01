using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TMPro;
using UnityEngine;

public class UIInventory : MonoBehaviour
{
    public GameObject inventoryWindow;

    public ItemSlot[] slots;
    public Transform slotPanel;
    public Transform dropPosition;

    [Header("Select Item")]
    public TextMeshProUGUI selectedItemName;
    public TextMeshProUGUI selectedItemDescription;
    public TextMeshProUGUI selectedStatName;
    public TextMeshProUGUI selectedStatValue;
    public GameObject useButton;
    public GameObject equipButton;
    public GameObject unequipButton;
    public GameObject dropButton;

    bool equipped = false;


    private PlayerStats condition;
    private PlayerController controller;

    public EquipmentData selectedItem;
    int selectedItemIndex = 0;


    int curEquipIndex;

    void Start()
    {
        condition = Managers.Player.Player.PlayerStats;
        controller = Managers.Player.Player.Controller;

        Managers.Player.Player.addItem += AddItem;

        slots = new ItemSlot[slotPanel.childCount];

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = slotPanel.GetChild(i).GetComponent<ItemSlot>();
            slots[i].index = i;
            slots[i].inventory = this;
        }

        inventoryWindow.SetActive(false);


        ClearSelectedItemWindow();
    }

    bool canbuild = false;

    void Canbuild(UIInventory uIInventory, EquipmentData data, BuildData build)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].icon == data.icon && slots[i].quantity < build.cost)
                canbuild = true;
        }
    }

    void UseStackedItem(EquipmentData data, BuildData build)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].icon == data.icon && slots[i].quantity < build.cost)
            {
                slots[i].item.quantity -= build.cost;

                if (slots[i].item.quantity == 0)
                {
                    selectedItem = null;
                    slots[selectedItemIndex].item = null;
                    selectedItemIndex = -1;
                    ClearSelectedItemWindow();
                }
            }

            UpdateUI();
        }
    }


    public void ClearSelectedItemWindow()
    {
        selectedItemName.text = string.Empty;
        selectedItemDescription.text = string.Empty;
        selectedStatName.text = string.Empty;
        selectedStatValue.text = string.Empty;

        useButton.SetActive(false);
        equipButton.SetActive(false);
        unequipButton.SetActive(false);
        dropButton.SetActive(false);
    }


    void AddItem()
    {
        EquipmentData data = Managers.Player.Player.itemData;

        if (data.canStack)
        {
            ItemSlot slot = GetItemStack(data);
            if (slot != null)
            {
                slot.quantity++;
                slot.resourceType = data.resourcetype;
                UpdateUI();
                Managers.Player.Player.itemData = null;
                return;
            }
        }
        ItemSlot emptySlot = GetEmptySlot(data);

        // 있다면
        if (emptySlot != null)
        {
            emptySlot.item = data;
            emptySlot.resourceType = data.resourcetype;
            emptySlot.quantity = 1;
            UpdateUI();
            Managers.Player.Player.itemData = null;
            return;
        }


        ThrowItem(data);
        Managers.Player.Player.itemData = null;
    }


    public void UpdateUI()
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

    void ThrowItem(EquipmentData data)
    {
        Instantiate(data.dropPrefab, dropPosition.position, Quaternion.Euler(Vector3.one * Random.value * 360));
    }


    public void SelectItem(int index)
    {
        if (slots[index].item == null)
        {
            return;
        }


        selectedItem = slots[index].item;
        selectedItemIndex = index;

        selectedItemName.text = selectedItem.displayName;
        selectedItemDescription.text = selectedItem.description;

        selectedStatName.text = string.Empty;
        selectedStatValue.text = string.Empty;

        for (int i = 0; i < selectedItem.weaponDamages.Length; i++)
        {
            selectedStatName.text += selectedItem.weaponDamages[i].type.ToString() + "\n";
            selectedStatValue.text += selectedItem.weaponDamages[i].value.ToString() + "\n";
        }

        useButton.SetActive(selectedItem.type == EquipmentType.Food);
        equipButton.SetActive(selectedItem.type == EquipmentType.Weapon && !slots[index].equipped);
        unequipButton.SetActive(selectedItem.type == EquipmentType.Weapon && slots[index].equipped);
        dropButton.SetActive(true);
    }

    public void OnUseButton()
    {
        if (selectedItem.type == EquipmentType.Food)
        {
            for (int i = 0; i < selectedItem.consumables.Length; i++)
            {
                switch (selectedItem.consumables[i].type)
                {
                    case ConsumableType.Health:
                        controller.Heal(selectedItem.consumables[i].value);
                        break;
                    case ConsumableType.Hunger:
                        controller.Eat(selectedItem.consumables[i].value);
                        break;
                }
            }
            RemoveSelectedItem();
        }
    }

    public void OnDropButton()
    {
        if (equipped)
        {
            UnEquip(selectedItemIndex);
            equipped = false;

        }
        ThrowItem(selectedItem);
        RemoveSelectedItem();

    }

    void RemoveSelectedItem()
    {
        slots[selectedItemIndex].quantity--;

        if (slots[selectedItemIndex].quantity <= 0)
        {
            selectedItem = null;
            slots[selectedItemIndex].item = null;
            selectedItemIndex = -1;
            ClearSelectedItemWindow();
        }

        UpdateUI();
    }

    public void OnEquipButton()
    {
        if (slots[curEquipIndex].equipped)
        {
            UnEquip(curEquipIndex);
        }

        slots[selectedItemIndex].equipped = true;
        curEquipIndex = selectedItemIndex;
        Managers.Player.Player.interaction.EquipNew(selectedItem);
        UpdateUI();

        SelectItem(selectedItemIndex);
        equipped = true;
    }

    void UnEquip(int index)
    {
        slots[index].equipped = false;
        Managers.Player.Player.interaction.UnEquip();
        UpdateUI();

        if (selectedItemIndex == index)
        {
            SelectItem(selectedItemIndex);
        }
    }

    public void OnUnEquipButton()
    {
        UnEquip(selectedItemIndex);
        equipped = false;
    }
}
