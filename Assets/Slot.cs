using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Slot : MonoBehaviour
{
    public bool Empty = true;
    public Item Items;
    public int amount;
    public Transform stuff; // Inventory array of items
    public Transform elems; // Hotbar array of items
    public Transform container; // Container array of items
    public Transform armor; // Armor stand - array of armor
    public int index;
    public Transform inv;
    Inventroy inventroy;
    public LimitationMode SlotLimitMode;
    //public int[] IDs; // Элементы, согласно SlotLimitMode, которые могут или не могут находиться в этом слоте
    public Item.ItemType[] TYPEs; // Типы элементов, которые, согласно SlotLimitMode, могут или не могут находиться в этом слоте
    public ContainerType Type;
    private void Start()
    {
        inventroy = inv.GetComponent<Inventroy>();
    }
    public void DestroyLast()
    {
        if (!Empty)
        {
            amount--;
            if (amount <= 0) Clear();
        }
    }
    public bool AddItem(Item i)
    {
        if (SlotLimitMode == LimitationMode.None) return false;
        else if (SlotLimitMode == LimitationMode.OnlyChoosen)
        {
            if (!TYPEs.Contains(i.Type)) return false;
        }
        else if (SlotLimitMode == LimitationMode.AvoidChoosen)
        {
            if (TYPEs.Contains(i.Type)) return false;
        }

        if (amount < i.maxAmount)
        {
            if (Empty || !Empty && i.ID == Items.ID)
            {
                amount++;
                if (amount <= 1)
                {
                    i = Instantiate(i);
                    i.transform.position = transform.position;
                    if (Type == ContainerType.Inventory)
                        i.transform.SetParent(stuff);
                    else if (Type == ContainerType.Hotbar)
                        i.transform.SetParent(elems);
                    i.attachedIndex = index;
                    Items = i;
                }
                Empty = false;
                Items.gameObject.SetActive(true);
                Items.amountText.text = amount.ToString();
                Items.amountText.gameObject.SetActive(true);
                return true;
            }
        } 
        
        return false;
    }
    public void Swap(Slot slot1, Slot slot2)
    {
        if (!inventroy.opened) return;

        if (SlotLimitMode == LimitationMode.None) return;
        else if (SlotLimitMode == LimitationMode.OnlyChoosen)
        {
            if (!TYPEs.Contains(slot2.Items.Type)) return;
        }
        else if (SlotLimitMode == LimitationMode.AvoidChoosen)
        {
            if (TYPEs.Contains(slot2.Items.Type)) return;
        }

        (slot1.Items.transform.position, slot2.Items.transform.position) = (slot2.transform.position, slot1.transform.position);
        (slot1.Items, slot2.Items) = (slot2.Items, slot1.Items);
        (slot1.amount, slot2.amount) = (slot2.amount, slot1.amount);
        (slot1.Empty, slot2.Empty) = (slot2.Empty, slot1.Empty);
        (slot1.Items.attachedIndex, slot2.Items.attachedIndex) = (slot2.Items.attachedIndex, slot1.Items.attachedIndex);

        if (slot1.Type != slot2.Type)
        {
            if (slot1.Type == ContainerType.Inventory) slot1.Items.transform.SetParent(stuff);
            if (slot1.Type == ContainerType.Hotbar) slot1.Items.transform.SetParent(elems);

            if (slot2.Type == ContainerType.Inventory) slot2.Items.transform.SetParent(stuff);
            if (slot2.Type == ContainerType.Hotbar) slot2.Items.transform.SetParent(elems);
        }
        if (slot1.amount > 0)
        {
            slot1.Items.gameObject.SetActive(true);
            slot1.Items.amountText.gameObject.SetActive(true);
        }
        else
        {
            slot1.Items.gameObject.SetActive(false);
            slot1.Items.amountText.gameObject.SetActive(false);
        }
        if (slot2.amount > 0)
        {
            slot2.Items.gameObject.SetActive(true);
            slot2.Items.amountText.gameObject.SetActive(true);
        }
        else
        {
            slot2.Items.gameObject.SetActive(false);
            slot2.Items.amountText.gameObject.SetActive(false);
        }
    }   
    public void Clear()
    {
        Items = null;
        Empty = true;
        amount = 0;
    }
    public void Add(Item i, int n)
    {
        if (n + amount > i.maxAmount) return;
        for (int _ = 0; _ < n; _++)
        {
            AddItem(i);
        }
    }

    public enum LimitationMode
    {
        All, // Доступны все элементы
        OnlyChoosen,  // Доступны все элементы, что указаны в IDs
        AvoidChoosen, // Доступны все элементы, кроме тех, что указаны в IDs
        None // Не может находиться ни один элемент. Нужен для блокировки дополнительных слотов и т.п.
    }
    public enum ContainerType // От этого зависит объект, от к которому привязан предмет в этом слоте
    {
        Hotbar,
        Inventory,
        Container,
        ArmorStand
    }
}
