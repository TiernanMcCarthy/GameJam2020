using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class windTurbine : MonoBehaviour
{
    // Start is called before the first frame update
    public BoxCollider detectionBox;

    public float Speed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            other.attachedRigidbody.AddForce(Vector3.up * Speed);
        }
    }
}
