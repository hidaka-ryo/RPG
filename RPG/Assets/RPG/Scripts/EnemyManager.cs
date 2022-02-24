using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    ItemManager itemManager;
    Enemy enemy1;
    [SerializeField]
    private float waitTime = 3f;
    private float elapsedTime;
    Enemy m_enemy;
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] float interval;
    [SerializeField] float time = 0f;
    int maxEnemys = 5;
    int currentEnemys;
    [SerializeField] Transform player;
    [SerializeField] Transform enemy;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 playerPos = player.position;
        Vector3 enemyPos = enemy.position;
        maxEnemys = currentEnemys;
        elapsedTime = 0f;
        itemManager = GetComponent<ItemManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Spawn();
    }

    public void Spawn()
    {
        //itemManager.ItemDrop();
        GameObject enemy = GameObject.Find(enemyPrefab.name);
        if (enemy == null)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime > waitTime)
            {
                //enemy.transform.position = new Vector3(0, 0, 0);
                GameObject newEnemy = Instantiate(enemyPrefab);
                newEnemy.name = enemyPrefab.name;
                elapsedTime = 0f;
            }
        }
    }
}
