using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Linq;
public class Item : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    public GameObject self; // Loot object / Object to use (weapon model etc.)
    public Text amountText;
    public int maxAmount = 4;
    bool attachedToCursor = false;
    //[HideInInspector]
    public int attachedIndex;
    public GameObject inv;
    Inventroy inventroy;
    public Transform stuff; // Inventory array of items
    public Transform elems; // Hotbar array of items
    public Transform container; // Container array of items
    public Transform armor; // Armor stand - array of armor
    public int ID = -1;
    public ItemType Type;
    void Start()
    {
        inventroy = inv.GetComponent<Inventroy>();
    }
    private void Update()
    {
        /*if (attachedToCursor && Input.GetMouseButtonUp(0))
        {
            print("up!");
            attachedToCursor = false;
*//*            if (inventroy.opened)
                AttachNew();
            else *//*
            ReturnBack();
        }*/
        if (attachedToCursor) 
        {
            transform.position = Input.mousePosition;
            transform.SetAsLastSibling();
        }
        print($"{attachedToCursor} {inventroy.opened}");
        if (attachedToCursor && !inventroy.opened)
        {
            /*print("efijiejiv");
            AttachNew();*/
            ReturnBack();
        }
    }
    void OnDisable()
    {
        print("Disable");
        attachedToCursor = false;
        ReturnBack();
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        attachedToCursor = false;
        if (inventroy.opened)
            AttachNew();
        else ReturnBack();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (!inventroy.opened) return;
        attachedToCursor = true;
    }

    void AttachNew()
    {
        attachedToCursor = false;
        Slot nearest = inventroy.slots.OrderBy(x => Vector3.Distance(transform.position, x.transform.position)).Where(x => x.gameObject.activeSelf).FirstOrDefault();
        Slot self = GetSlot();
        if (nearest.index != attachedIndex) 
        {
            if (nearest.SlotLimitMode == Slot.LimitationMode.None) { ReturnBack(); return; }
            else if (nearest.SlotLimitMode == Slot.LimitationMode.OnlyChoosen)
            {
                if (!nearest.TYPEs.Contains(Type))
                {
                    ReturnBack();
                    return;
                }
            }
            else if (nearest.SlotLimitMode == Slot.LimitationMode.AvoidChoosen)
            {
                if (nearest.TYPEs.Contains(Type)) { ReturnBack(); return; } //nearest.Items.ID
            }

            if (!nearest.Empty)
            {
                if (nearest.Items.ID == ID && nearest.amount + self.amount <= maxAmount)
                {
                    self.Swap(self, nearest);
                    return;
                }
                else
                {
                    self.Swap(self, nearest);
                    return;
                }
            }

            if (self.Type != nearest.Type)
            {
                if (nearest.Type == Slot.ContainerType.Inventory) transform.SetParent(stuff);
                if (nearest.Type == Slot.ContainerType.Hotbar) transform.SetParent(elems);
                if (nearest.Type == Slot.ContainerType.Container) transform.SetParent(container);
                if (nearest.Type == Slot.ContainerType.ArmorStand) transform.SetParent(armor);
            }

            nearest.amount = self.amount;
            nearest.Empty = false;
            nearest.Items = this;
            
            transform.position = nearest.transform.position;
            attachedIndex = nearest.index;

            self.Clear();
            return;
        }
        ReturnBack();
        return;
    }
    void ReturnBack()
    {
        Slot self = GetSlot();
        transform.position = self.transform.position;
    }
    Slot GetSlot()
    {
        for (int i = 0; i < inventroy.slots.Length; i++)
        {
            Slot sc = inventroy.slots[i];
            if (sc.index == attachedIndex)
            {
                return inventroy.slots[i];
            }
        }
        return null;
    }
    public enum ItemType
    {
        Any,
        Armor,
        Other
    }
}
