using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventroy : MonoBehaviour
{
    public GameObject InventoryPanel;
    public Transform AllSlots;
    public Slot[] slots;
    bool opened = true;
    public GameObject ItemBase;
    ItemBase itemBase;
    Canvas canvas;
    public GameObject Empty;
    public Transform stuff;
    void Start()
    {
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        itemBase = ItemBase.GetComponent<ItemBase>();
        slots = new Slot[AllSlots.childCount-1];
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = AllSlots.GetChild(i).GetComponent<Slot>();
        }
        int id = 0;
        foreach (Slot slot in slots)
        {
            slot.index = id;
            id++;
            if (!slot.Empty)
            {
                slot.AddItem(slot.Items);
            }
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            opened = !opened;
            if (opened)
            {
                InventoryPanel.SetActive(true);
            }

            else if (!opened) 
            { 
                InventoryPanel.SetActive(false); 
            }
            
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            AddItem(itemBase.items[Random.Range(0, 2)]);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            DestroyItem(1);
        }
    }
    void AddItem(Item item)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].AddItem(item)) return;
        }
    }
    void DestroyItem(int index)
    {
        slots[index].DestroyLast();
    }
}
