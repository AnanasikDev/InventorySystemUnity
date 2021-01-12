using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public class Inventroy : MonoBehaviour
{
    public GameObject InventoryPanel;
    public Transform AllSlots;
    public Transform HotbarSlots;
    public Slot[] slots;
    [HideInInspector]
    public bool opened = false;
    public GameObject ItemBase;
    ItemBase itemBase;
    Canvas canvas;
    public Transform stuff;
    //public Image ActiveSlotHighLighter; 
    void Start()
    {
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        itemBase = ItemBase.GetComponent<ItemBase>();
        slots = new Slot[AllSlots.childCount-1 + HotbarSlots.childCount-1];
        int add = 0;
        for (int i = 0; i < AllSlots.childCount - 1; i++)
        {
            slots[add] = AllSlots.GetChild(i).GetComponent<Slot>();
            add++;
        }
        for (int i = 0; i < HotbarSlots.childCount-1; i++)
        {
            slots[add] = HotbarSlots.GetChild(i).GetComponent<Slot>();
            add++;
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
        //ActiveSlotHighLighter.transform.position = slots[slots.Length - 1].transform.position;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            opened = !opened;
            if (opened)
            {
                InventoryPanel.SetActive(true);
                Time.timeScale = 0;
            }

            else if (!opened) 
            { 
                InventoryPanel.SetActive(false);
                Time.timeScale = 1;
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
