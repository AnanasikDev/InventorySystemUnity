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
                slot.AddItem(slot.Items[0]);
            }
        }
        /*foreach (Slot slot in slots)
        {
            print("Добавляю");
            Instantiate(Empty, slot.transform.position, Quaternion.identity, stuff); //slot.transform.position, Quaternion.identity, 
        }*/
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            opened = !opened;
            if (opened)
            {
                InventoryPanel.SetActive(true);
                ShowItems();
            }

            else if (!opened) 
            { 
                InventoryPanel.SetActive(false); 
            }
            
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            print("Adding");
            //Item item = Instantiate(itemBase.items[0]);
            AddItem(itemBase.items[0]);
            ShowItems();
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
            if (AddItem(item, i)) return;
        }
    }
    bool AddItem(Item item, int index)
    {
        return slots[index].AddItem(item);
    }
    void ShowItems()
    {
        foreach (Slot slot in slots)
        {
            print(slot.amount);
            if (slot.amount > 0)
            {
                //slot.Holded.transform.position = slot.transform.position;
                //slot.Holded.transform.SetParent(slot.transform);
                //slot.Holded.transform.localPosition = Vector3.zero;
                //slot.Holded.transform.SetParent(canvas.transform);


                //slot.Holded.transform.position = slot.transform.position;
                // slot.Holded.transform.SetParent(slot.transform);
            }
        }
    }
    void DestroyItem(int index)
    {
        slots[index].DestroyLast();
        /*if (slots[index].amount <= 1 && !slots[index].Empty)
        {
            slots[index].amount = 0;
            slots[index].Empty = true;
            //Destroy(slots[index].transform.GetChild(slots[index].amount).gameObject);
            slots[index].DestroyLast();
            print("Слот стал пустым");
            return;
        }
        else if (slots[index].amount > 1 && !slots[index].Empty)
        {
            print("Удаляю один предмет в этом слоте");
            slots[index].amount--;
            Destroy(slots[index].transform.GetChild(slots[index].amount).gameObject);
            Destroy(slots[index].Holded.gameObject);
        }*/
    }
}
