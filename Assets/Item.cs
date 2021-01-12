using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Linq;
public class Item : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    public GameObject self;
    public Image icon;
    public int maxAmount = 4;
    bool attachedToCursor = false;
    //[HideInInspector]
    public int attachedIndex;
    public GameObject inv;
    Inventroy inventroy;
    public Transform stuff;
    public int ID = -1;
    void Start()
    {
        inventroy = inv.GetComponent<Inventroy>();
    }
    private void Update()
    {
        if (attachedToCursor) 
        {
            transform.position = Input.mousePosition;
            transform.SetAsLastSibling();
        }
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        attachedToCursor = false;
        AttachNew();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        attachedToCursor = true;
    }

    void AttachNew()
    {
        print("Отпустил");
        attachedToCursor = false;
        Slot nearest = inventroy.slots.OrderBy(x => Vector3.Distance(transform.position, x.transform.position)).FirstOrDefault();
        Slot self = GetSlot();
        if (nearest.index != attachedIndex) 
        {
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
            print("Здесь пусто!");
            if (nearest.SlotLimitMode == Slot.LimitationMode.None) { ReturnBack(); return; }
            else if (nearest.SlotLimitMode == Slot.LimitationMode.OnlyChoosen)
            {
                if (!nearest.IDs.Contains(ID)) { ReturnBack(); return; }
            }
            else if (nearest.SlotLimitMode == Slot.LimitationMode.AvoidChoosen)
            {
                if (nearest.IDs.Contains(ID)) { ReturnBack(); return; } //nearest.Items.ID
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
}
