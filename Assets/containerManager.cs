using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class containerManager : MonoBehaviour
{
    // General script for managing ALL containers. Every container has another one script, that uses this one
    public GameObject[] ExtraParts; // Some details that also should be shown or disabled by container
    public GameObject[] ExceptParts;
    GameObject containerPanel;
    public GameObject ArmorStand;
    bool opened = false;
    //public Inventroy.ContainerItem[] Items;
    public Item[] Items; // All items that must be in container
    public int[] amounts; // The amounts of all items, attached to Items field
    public Transform containerStuff; // The actual list of items in container
    //Transform[] containerStuffItems;
    public GameObject InventoryObject;
    Inventroy inv;
    int capacity = 16;
    public Transform AllSlots;
    public Slot[] slots;
    public GameObject playerViz; // Player vizualization in inventory
    public container CurrentContainer; // The last one opened container. It becomes null exactly whenever it becomes closed
    private void Start()
    {
        containerPanel = transform.GetChild(0).gameObject;
        slots = new Slot[capacity];
        for (int i = 0; i < capacity; i++)
        {
            slots[i] = AllSlots.GetChild(i).GetComponent<Slot>();
        }
        Items = new Item[capacity];
        amounts = new int[capacity];
    }
    public void Open()
    {
        playerViz.SetActive(false);
        /*for (int i = 0; i < capacity; i++)
        {
            Item ii = Items[i];
            Items[i] = null;
            Items[i] = Instantiate(ii, ii.transform.position, Quaternion.identity, containerStuff);
        }*/
        containerPanel.SetActive(true);

        foreach (GameObject obj in ExtraParts)
        {
            obj.SetActive(true);
        }
        foreach (GameObject obj in ExceptParts)
        {
            obj.SetActive(false);
        }
        ArmorStand.SetActive(false);
    }
    public void Close()
    {
        playerViz.SetActive(true); // Снова включаем визуализацию в любом случае при закрытие сундука
        containerPanel.SetActive(false);
        foreach (GameObject obj in ExtraParts)
        {
            obj.SetActive(false);
        }
        foreach (GameObject obj in ExceptParts)
        {
            obj.SetActive(true);
        }
        ArmorStand.SetActive(true);
        Clear();
    }
    public void Clear()
    {
        //containerStuffItems = new Transform[];
       /* Items = new Item[capacity];
        amounts = new int[capacity];

        foreach (Slot s in slots) if (!s.Empty) { Destroy(s.Items.gameObject); print("Object cleared"); }
        foreach (Slot s in slots) s.Clear();*/
        foreach (Slot i in slots)
        {
            if (!i.Empty) { Destroy(i.Items.gameObject); i.Clear(); }
        }
        amounts = new int[capacity];
    }
    public void Fill()
    {
        int i;
        for (i = 0; i < capacity; i++)
        {
            if (amounts[i] <= 0) continue;
            print(i);
            slots[i].Add(Items[i], amounts[i]);
            if (!slots[i].Empty) slots[i].Items.transform.SetParent(containerStuff);
            /*slots[i].Items = Items[i];
            slots[i].amount = amounts[i];*/
        }
    }
}
