using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleChecker : MonoBehaviour
{

    public List<Collectible> Collectables;

    public Goal Door;

    // Start is called before the first frame update
    void Awake()
    {
        Collectables = new List<Collectible>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Collectables.Count==0)
        {
            Door.ExecuteGoal();
            Destroy(gameObject);
        }
        

    }
}
