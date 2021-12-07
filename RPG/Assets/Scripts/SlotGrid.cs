using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotGrid : MonoBehaviour
{
    [SerializeField] private Item[] allItems;
    [SerializeField] private GameObject slotPrefab;
    private int slotNumber = 20;
    void Start()
    {
        for (int i = 0; i < slotNumber; i++)
        {
            GameObject slotobj = Instantiate(slotPrefab, this.transform);
            Slot slot = slotobj.GetComponent<Slot>();
            if (i<allItems.Length)
            {
                slot.SetItem(allItems[i]);
            }
            else
            {
                slot.SetItem(null);
            }
        }
    }
}
