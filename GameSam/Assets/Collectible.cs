using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    // Start is called before the first frame update

    public CollectibleChecker Checker;


    void Start()
    {
        Checker.Collectables.Add(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<characterMovement>())
        {

        }
    }
}
