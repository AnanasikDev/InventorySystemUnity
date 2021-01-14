using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Linq;
public class Item : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public GameObject self; // Loot object / Object to use (weapon model etc.)
    [HideInInspector]
    public Text amountText;   // Text, that shows the current amount of items
    public int maxAmount = 4; // Max amount of this item in one slot
    [HideInInspector]
    public bool attachedToCursor = false;
    [HideInInspector]
    public int attachedIndex; // Slot's index, this item is attached to
    public GameObject inv;
    Inventroy inventroy;
    public GameObject ManagerContainer; // containerManager object
    containerManager managerScript; // Script provides management of all containers
    public Transform stuff; // Inventory array of items
    public Transform elems; // Hotbar array of items
    public Transform container; // Container array of items
    public Transform armor; // Armor stand - array of armor
    [HideInInspector]
    public int ID = -1; // Id of item
    public ItemType Type;
    Camera cam;
    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.button == 0)
        {
            attachedToCursor = false;
            if (inventroy.opened)
                AttachNew();
            else ReturnBack();
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == 0)
        {
            if (!inventroy.opened) return;
            attachedToCursor = true;
        }
    }
    void Start()
    {
        amountText = transform.GetChild(0).GetComponent<Text>();
        inventroy = inv.GetComponent<Inventroy>();
        cam = Camera.main;
        managerScript = ManagerContainer.GetComponent<containerManager>();
    }
    private void Update()
    {
        if (attachedToCursor) 
        {
            transform.position = Input.mousePosition;
            transform.SetAsLastSibling();
        }
        print($"{attachedToCursor} {inventroy.opened}");
        if (attachedToCursor && !inventroy.opened)
        {
            ReturnBack();
        }
    }
    void OnDisable()
    {
        print("Disable");
        attachedToCursor = false;
        ReturnBack();
    }
    void AttachNew()
    {   
        attachedToCursor = false;
        foreach (Slot s in inventroy.slots) print("active is " + s.gameObject.activeInHierarchy);
        Slot[] possible = inventroy.slots.OrderBy(x => Vector3.Distance(transform.position, x.transform.position)).Where(x => x.gameObject.activeInHierarchy).ToArray();
        Slot nearest = possible.FirstOrDefault();
        print(string.Join<Slot>(",", possible));
        Slot self = GetSlot();
        if (nearest.index != attachedIndex) 
        {
            if (nearest.SlotLimitMode == Slot.LimitationMode.None) { ReturnBack(); return; }
            else if (nearest.SlotLimitMode == Slot.LimitationMode.OnlyChoosen)
            {
                if (!nearest.TYPEs.Contains(Type))
                {
                    ReturnBack();
                    return;
                }
            }
            else if (nearest.SlotLimitMode == Slot.LimitationMode.AvoidChoosen)
            {
                if (nearest.TYPEs.Contains(Type)) { ReturnBack(); return; } //nearest.Items.ID
            }


            if (!nearest.Empty)
            {
                if (nearest.Items.ID == ID && nearest.amount + self.amount <= maxAmount)
                {
                    self.Swap(self, nearest);
                    return;
                }
                else
                {
                    self.Swap(self, nearest);
                    return;
                }
            }

            if (self.Type != nearest.Type)
            {
                if (nearest.Type == Slot.ContainerType.Inventory) transform.SetParent(stuff);
                if (nearest.Type == Slot.ContainerType.Hotbar) transform.SetParent(elems);
                if (nearest.Type == Slot.ContainerType.Container) transform.SetParent(container);
                if (nearest.Type == Slot.ContainerType.ArmorStand) transform.SetParent(armor);
            }

            transform.position = nearest.transform.position;
            if (nearest.Type == Slot.ContainerType.Container)
            {
                if (managerScript.CurrentContainer != null)
                {
                    print("Container add");
                    managerScript.CurrentContainer.Add(this, self.amount, nearest.localID);
                }
            }

            nearest.amount = self.amount;
            nearest.Empty = false;
            nearest.Items = this;
            
            
            attachedIndex = nearest.index;

            self.Clear();
            return;
        }
        ReturnBack();
        return;
    }
    void ReturnBack()
    {
        Slot self = GetSlot();
        transform.position = self.transform.position;
    }
    Slot GetSlot()
    {
        for (int i = 0; i < inventroy.slots.Length; i++)
        {
            Slot sc = inventroy.slots[i];
            if (sc.index == attachedIndex)
            {
                return inventroy.slots[i];
            }
        }
        return null;
    }
    public enum ItemType
    {
        Any,
        Armor,
        Other
    }
}
