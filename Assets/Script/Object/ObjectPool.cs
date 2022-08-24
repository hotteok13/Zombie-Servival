using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public RandomSpawn spawn;
    public static ObjectPool instance;

    [SerializeField] GameObject zombie;

    public Queue<GameObject> queue = new Queue<GameObject>();

    private void Start()
    {
        instance = this;

        for(int i = 0; i < 19; i++)
        {
            GameObject tempPrefab = Instantiate(zombie, spawn.RandomPosition(), Quaternion.identity);
            queue.Enqueue(tempPrefab);
            tempPrefab.SetActive(false);
        }
    }
    public Vector3 ActivePosition()
    {
        return spawn.RandomPosition();
    }
    public void InsertQueue(GameObject tobj)
    {
        queue.Enqueue(tobj);

        tobj.SetActive(false);
    }

    public GameObject GetQueue()
    {
        GameObject tempZombie = queue.Dequeue();
        tempZombie.SetActive(true);

        return tempZombie;
    }
}
