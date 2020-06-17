using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Follow;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Follow.transform.position = transform.position;
    }
}
