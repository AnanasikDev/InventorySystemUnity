using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class container : MonoBehaviour
{
    // General script for managing ALL containers. Every container has another one script, that uses this one
    public GameObject[] ExtraParts; // Some details that also should be shown or disabled by container
    public GameObject[] ExceptParts;
    GameObject containerPanel;
    public GameObject ArmorStand;
    bool opened = false;
    public Inventroy.ContainerItem[] Items;
    public Transform containerStuff; // The actual list of items in container
    Transform[] containerStuffItems;
    public GameObject InventoryObject;
    Inventroy inv;
    int capacity = 16;
    public Transform AllSlots;
    public Slot[] slots;
    private void Start()
    {
        containerPanel = transform.GetChild(0).gameObject;
        slots = new Slot[capacity];
        for (int i = 0; i < capacity; i++)
        {
            slots[i] = AllSlots.GetChild(i).GetComponent<Slot>();
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            opened = !opened;
            if (opened) Open();
            if (!opened) Close();
        }
    }
    public void Open()
    {
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
    }
    /*public void Clear()
    {
        containerStuffItems = new 
    }*/
}
