using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class container : MonoBehaviour
{
    public Item[] items = new Item[16];
    public int[] amounts = new int[16];
    public bool opened = false;
    public GameObject manager;
    containerManager managerSc;
    public int capacity = 16;
    public Transform AllItemsObject;
    Item[] AllItems;
    public Transform Containerstuff;
    public GameObject ItemBase;
    ItemBase itemBaseScript;
    private void Start()
    {
        //items = new Item[capacity];
        //amounts = new int[capacity];
        managerSc = manager.GetComponent<containerManager>();
        /*Item apple = Instantiate(AllItemsObject.GetChild(0)).GetComponent<Item>();
        apple.transform.SetParent(Containerstuff);
        items[0] = apple;
        amounts[0] = 1;*/
        itemBaseScript = ItemBase.GetComponent<ItemBase>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            opened = !opened;
            if (opened) Open();
            else Close();
        }
    }
    public void Open()
    {
        managerSc.CurrentContainer = this;
        opened = true;
        Item[] deepCopy = items;
        /*for (int i = 0; i < deepCopy.Length; i++)
        {
            if (items[i] != null) deepCopy[i] = Instantiate(items[i], managerSc.slots[i].transform.position, Quaternion.identity, Containerstuff);
        }*/
        print(string.Join<Item>(", ", deepCopy));
        managerSc.Items = deepCopy;
        managerSc.amounts = amounts;
        managerSc.Fill();
        managerSc.Open();
    }
    public void Close()
    {
        managerSc.CurrentContainer = null;
        opened = false;
        //managerSc.Clear();
        managerSc.Close();
    }
    public bool Add(Item item, int amount, int index) // Что, сколько и куда
    {
        if (amounts[index] != 0) return false;

        print("Adding");
        print(itemBaseScript.items);
        items[index] = itemBaseScript.items[item.ID]; //Instantiate(item, item.transform.position, Quaternion.identity, item.transform.parent);
        //managerSc.slots[index].Items = items[index];
        //Destroy(item.gameObject);
        amounts[index] = amount;
        return true;
    }
    public void Remove(int index)
    {
        print("Removing");
        items[index] = null;
        amounts[index] = 0;
    }
}
