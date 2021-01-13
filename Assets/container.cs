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
    private void Start()
    {
        //items = new Item[capacity];
        //amounts = new int[capacity];
        managerSc = manager.GetComponent<containerManager>();
        /*Item apple = Instantiate(AllItemsObject.GetChild(0)).GetComponent<Item>();
        apple.transform.SetParent(Containerstuff);
        items[0] = apple;
        amounts[0] = 1;*/
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
        opened = true;
        managerSc.Items = items;
        managerSc.amounts = amounts;
        managerSc.Fill();
        managerSc.Open();
    }
    public void Close()
    {
        opened = false;
        managerSc.Clear();
        managerSc.Close();
    }
}
