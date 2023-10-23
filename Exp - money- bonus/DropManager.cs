using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropManager : MonoBehaviour
{
    [System.Serializable] //To see at Inspector
    public class Drops
    {
        public string name;
        public GameObject itemPrefab;
        public float dropRate;
        public int expGained;
    }

    public List<Drops> drops; //List of items (Blue gems/ Green/Red)

    public void OnEnemyDiactivation()
    {
        float randomNumber = UnityEngine.Random.Range(0f, 100f);
        List<Drops> possibleDrops = new List<Drops>(); //To avoid when destroy enemy, spawn 2 item at the same rates (50%blue 50%green)

        foreach (Drops rate in drops)
        {
            if(randomNumber <= rate.dropRate)
            {
                possibleDrops.Add(rate);
            }
        }
        // Check if there are possible drops
        if(possibleDrops.Count > 0)
        {
            Drops drops = possibleDrops[UnityEngine.Random.Range(0, possibleDrops.Count)];
            Instantiate(drops.itemPrefab, transform.position, Quaternion.identity);
        }
    }
}
