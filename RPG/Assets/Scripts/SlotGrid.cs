using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotGrid : MonoBehaviour
{
    [SerializeField] private Item[] allItems;
    [SerializeField] private GameObject slotPrefab;
    private int slotNumber = 4;
    private List<Slot> allslots;
    void Start()
    {
        allslots = new List<Slot>();
        for (int i = 0; i < slotNumber; i++)
        {
            GameObject slotobj = Instantiate(slotPrefab, this.transform);
            Slot slot = slotobj.GetComponent<Slot>();
            allslots.Add(slot);
            //if (i < allItems.Length)
            //{
            //    slot.SetItem(allItems[i]);
            //}
            //else
            //{
            //    slot.SetItem(null);
            //}
        }
    }

    public bool AddItem(Item item)
    {
        foreach (var slot in allslots)
        {
            if (slot.MyItem == null)
            {
                slot.SetItem(item);
                return true;
            }
        }
        return false;
    }
}
