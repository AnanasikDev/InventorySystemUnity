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
    public Transform ContainerSlots;
    public Slot[] ArmorSlots;
    public Slot[] slots;
    [HideInInspector]
    public bool opened = true;
    public GameObject ItemBase;
    ItemBase itemBase;
    Canvas canvas;
    public Transform stuff;
    //public Image ActiveSlotHighLighter;
    public int add = 0;
    void Start()
    {
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        itemBase = ItemBase.GetComponent<ItemBase>();
        slots = new Slot[AllSlots.childCount-1 + HotbarSlots.childCount-1 + ArmorSlots.Length-1 + ContainerSlots.childCount];
        
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
        for (int i = 0; i < ArmorSlots.Length - 1; i++)
        {
            slots[add] = ArmorSlots[i];
            add++;
        }
        for (int i = 0; i < ContainerSlots.childCount - 1; i++)
        {
            slots[add] = ContainerSlots.GetChild(i).GetComponent<Slot>();
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
            if (opened) Open();
            else if (!opened) Close();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            AddItem(itemBase.items[Random.Range(0, 3)]);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            DestroyItem(1);
        }
    }
    public void AddItem(Item item)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].AddItem(item)) return;
        }
    }
    public void DestroyItem(int index)
    {
        slots[index].DestroyLast();
    }
    public void Open()
    {
        InventoryPanel.SetActive(true);
        Time.timeScale = 0;
        foreach (Slot s in ArmorSlots)
            s.gameObject.SetActive(true);
    }
    public void Close()
    {
        InventoryPanel.SetActive(false);
        Time.timeScale = 1;
        foreach (Slot s in ArmorSlots)
            s.gameObject.SetActive(false);
    }
/*    public int GetFirstEmptySlot()
    {
        return AllSlots.childCount - 1 + HotbarSlots.childCount - 1 + ArmorSlots.Length - 1;
    }*/
    public class ContainerItem
    {
        public Item item;
        public int amount;

        public ContainerItem(Item item, int amount)
        {
            this.item = item;
            this.amount = amount;
        }
    }

}