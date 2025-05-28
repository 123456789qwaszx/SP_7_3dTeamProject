using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIInventory : MonoBehaviour
{
    public ItemSlot[] slots;
    public GameObject inventoryWindow;
    public Transform slotPanel;
    public Transform dropPosition;

    public Resource resource;

    [Header("Select Item")]
    public TextMeshProUGUI selectedItemName;
    public TextMeshProUGUI selectedItemDescription;
    public TextMeshProUGUI selectedStatName;
    public TextMeshProUGUI selectedStatValue;
    public GameObject useButton;
    public GameObject equipButton;
    public GameObject unequipButton;
    public GameObject dropButton;

    EquipmentData selectedItem;
    int selectedItemIndex = 0;

    
    int curEquipIndex;

    void Start()
    {
        Managers.Player.Player.addItem += AddItem;


        inventoryWindow.SetActive(false);
        slots = new ItemSlot[slotPanel.childCount];

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = slotPanel.GetChild(i).GetComponent<ItemSlot>();
            slots[i].index = i;
            slots[i].inventory = this;
        }

        
        ClearSelectedItemWindow();
    }


    void ClearSelectedItemWindow()
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


    public void Toggle()
    {
        if (IsOpen())
        {
            inventoryWindow.SetActive(false);
        }
        else
        {
            inventoryWindow.SetActive(true);
        }
    }
    public bool IsOpen()
    {
        return inventoryWindow.activeInHierarchy;
    }

    void AddItem()
    {
        EquipmentData data = Managers.Player.Player.itemData;

        // if (data.canStack)
        // {
        //     ItemSlot slot = GetItemStack(data);
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
       // }

        //ThrowItem(data);
        //Managers.Player.Player.itemData = null;
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
        if (selectedItem.type == EquipmentType.Weapon)
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

    public void OnDropButton()
    {
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
        //CharacterManager.Instance.Player.equip.EquipNew(selectedItem);
        UpdateUI();

        SelectItem(selectedItemIndex);
    }

    void UnEquip(int index)
    {
        slots[index].equipped = false;
        //CharacterManager.Instance.Player.equip.UnEquip();
        UpdateUI();

        if (selectedItemIndex == index)
        {
            SelectItem(selectedItemIndex);
        }
    }

    public void OnUnEquipButton()
    {
        UnEquip(selectedItemIndex);
    }
}
