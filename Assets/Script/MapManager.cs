using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public GameObject[] maps;
    public List<int> gold = new List<int>();
    // Start is called before the first frame update
    void Start()
    {
        gold.Clear();
        SelectGold();
    }

    void SelectGold()
    {
        while(gold.Count < 5)
        {
            int rand = Random.Range(0, 49);

            if (gold.Contains(rand))
            {
                continue;
            }
            else
            {
                gold.Add(rand);
            }
        }

        foreach(int g in gold)
        {
            maps[g].tag = "gold";
        }
    }
}
