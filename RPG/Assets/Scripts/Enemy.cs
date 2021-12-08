using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int m_enemyHp = 1;
    private ItemManeger item;
    
    // Start is called before the first frame update
    void Start()
    {
        item = GetComponent<ItemManeger>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Sword"))
        {
            m_enemyHp -= 1;
            if (m_enemyHp == 0)
            {
                Destroy(transform.root.gameObject);
                item.ItemDrop();
            }
        }
    }
}
