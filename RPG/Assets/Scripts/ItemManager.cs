using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public GameObject m_itemObject;
    [SerializeField] private Item m_item;

    public void ItemDrop()
    {
        if (m_item.GetItemName() == "HPportion")
        {
            Instantiate(m_itemObject, transform.position, Quaternion.identity);
        }
    }
}
