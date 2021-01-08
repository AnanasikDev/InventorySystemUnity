using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    public bool Empty = true;
    public List<Item> Items;
    public int amount;
    public Transform stuff;
    public int index;
    //public GameObject emptyGameObj;
    public Transform inv;
    Inventroy inventroy;
    private void Start()
    {
        inventroy = inv.GetComponent<Inventroy>();
    }
    public void DestroyLast()
    {
        print("Удаляю без проверки");
        if (!Empty)
        {
            print("Удаляю");
            amount--;
            if (Items.Count > 0)
            {
                Items.RemoveAt(Items.Count - 1);
            }
            if (amount <= 0)
            {
                amount = 0;
                Empty = true;
                Destroy(transform.GetChild(0).gameObject);
                //item = null;
                //Destroy(Items[0].gameObject);
                //Destroy(stuff.GetChild(index).gameObject);
                //Instantiate(emptyGameObj, stuff.GetChild(index-1).transform.position, Quaternion.identity, stuff);
                //Destroy(stuff.GetChild(index).gameObject);

            }
            /*else
            {
                Items[0].gameObject.SetActive(true);
            }*/
            
        }
    }
    public bool AddItem(Item i)
    {
        if (amount < i.maxAmount)
        {
            amount++;
            Items.Add(i);
            if (amount > 1) { }
            else
            {
                i = Instantiate(i);
                //stuff.GetChild(index) = Instantiate(i, stuff.transform);
                /*Instantiate(i, stuff.GetChild(index).transform.position, Quaternion.identity, stuff);
                DestroyImmediate(stuff.GetChild(index).gameObject);*/
                i.transform.position = transform.position;
                i.transform.SetParent(inventroy.slots[index].transform);
            }
            Empty = false;
            Items[0].gameObject.SetActive(true);
            return true;
        }
        return false;
    }
}
