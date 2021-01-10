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
    public int ID;
    void Start()
    {
        inventroy = inv.GetComponent<Inventroy>();
    }
    /*public void OnPointerClick(PointerEventData eventData)
    {
        print("Clicked");
        attachedToCursor = true;
    }*/
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
        attachedToCursor = false;
        Slot nearest = inventroy.slots.OrderBy(x => Vector3.Distance(transform.position, x.transform.position)).FirstOrDefault();
        Slot self = GetSlot();
        if (nearest.index != attachedIndex) 
        {
            if (!nearest.Empty)
            {
                /*transform.position = inventroy.slots[attachedIndex].transform.position;
                return;*/
                if (nearest.Items.ID == ID && nearest.amount + self.amount <= maxAmount)
                {
                    print("Добавляюсь");
                    //nearest.Add(this, slot.amount);
                    //print(self.amount);
                    //nearest.MoveItem(this, self.amount);
                    self.Swap(self, nearest);
                    //self.Clear();
                    /*Destroy(gameObject);
                    Destroy(this);*/
                    attachedIndex = nearest.index;
                    //transform.position = inventroy.slots[attachedIndex].transform.position;
                    gameObject.SetActive(false);
                    return;
                }
                else
                {
                    print("Возвращаюсь");
                    transform.position = inventroy.slots[attachedIndex].transform.position;
                    return;
                }
            }
            print("Здесь пусто!");
            //nearest.MoveItem(this, self.amount);
            self.Swap(self, nearest);
            attachedIndex = nearest.index;
            print(self.amount);
            transform.position = nearest.transform.position;
            //self.Clear();

            
            //inv.transform.GetChild(attachedIndex).GetComponent<Slot>().AddItem(this);
            //nearest.Clear();
            //nearest.amount = amount;
            
            //nearest.AddItem(this);
            return;
        }
        transform.position = inventroy.slots[attachedIndex].transform.position;
        return;
    }
    Slot GetSlot()
    {
        for (int i = 0; i < inventroy.slots.Length; i++) //inv.transform.childCount
        {
            //var sc = inv.transform.GetChild(i).GetComponent<Slot>();
            Slot sc = inventroy.slots[i];
            if (sc.index == attachedIndex)
            {
                return inventroy.slots[i];
            }
        }
        return null;
    }
}
