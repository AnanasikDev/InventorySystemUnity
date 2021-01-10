using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    public bool Empty = true;
    public Item Items;
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
            if (amount <= 0)
            {
                amount = 0;
                Empty = true;
                Items = null;
               /* //Item[] selfItems = new Item[stuff.childCount];
                for (int i = 0; i < stuff.childCount; i++)
                {
                    var sc = stuff.GetChild(i).GetComponent<Item>();
                    if (sc.attachedIndex == index && sc.transform.gameObject.activeSelf)
                    {
                        Transform tr = stuff.GetChild(i);
                        //tr = null;
                        tr.gameObject.SetActive(false);
                        //DestroyImmediate(tr);
                    }
                }*/
                //Destroy(transform.GetChild(0).gameObject);
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
            //Items.Add(i);
            Items = i;
            if (amount > 1) { }
            else
            {
                print("Новый элемент");
                i = Instantiate(i);
                //stuff.GetChild(index) = Instantiate(i, stuff.transform);
                /*Instantiate(i, stuff.GetChild(index).transform.position, Quaternion.identity, stuff);
                DestroyImmediate(stuff.GetChild(index).gameObject);*/
                i.transform.position = transform.position;
                i.transform.SetParent(stuff);
                i.attachedIndex = index;
            }
            Empty = false;
            Items.gameObject.SetActive(true);
            return true;
        }
        return false;
    }
    public void MoveItem(Item i, int a)
    {
        if (amount + a < i.maxAmount)
        {
            amount+=a;
            //Items.Add(i);
            Items = i;
            if (amount > 1) { }
            else
            {

                print("Двигаю элемент");
                //stuff.GetChild(index) = Instantiate(i, stuff.transform);
                /*Instantiate(i, stuff.GetChild(index).transform.position, Quaternion.identity, stuff);
                DestroyImmediate(stuff.GetChild(index).gameObject);*/
                i.transform.position = transform.position;
                //i.transform.SetParent(stuff);
                i.attachedIndex = index;
            }
            Empty = false;
            Items.gameObject.SetActive(true);
        }
    }
    public void Swap(Slot slot1, Slot slot2)
    {
        //int index1 = slot1.index;
        //int index2 = slot2.index;

        //(slot1, slot2) = (slot2, slot1);

        //slot1.index = index1;
        //slot2.index = index2;

        (slot1.Items, slot2.Items) = (slot2.Items, slot1.Items);
        (slot1.amount, slot2.amount) = (slot2.amount, slot1.amount);
        (slot1.Empty, slot2.Empty) = (slot2.Empty, slot1.Empty);
        //(slot1.Items.attachedIndex, slot2.Items.attachedIndex) = (slot2.Items.attachedIndex, slot1.Items.attachedIndex);
        //(slot1.Items.attachedIndex, slot2.Items.attachedIndex) = (slot2.Items.attachedIndex, slot1.Items.attachedIndex);
        //if (slot1.amount <= 0) slot1.Clear();
        //if (slot2.amount <= 0) slot2.Clear();

    }
    public void Clear()
    {
        Items = null;
        Empty = true;
        amount = 0;
        //print(string.Join(",", Items));
    }
    public void Add(Item i, int n)
    {
        if (n + amount > i.maxAmount) return; //n = i.maxAmount;
        for (int _ = 0; _ < n; _++)
        {
            AddItem(i);
        }
    }
}
