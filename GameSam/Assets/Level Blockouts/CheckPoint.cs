using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{

    private void OnDrawGizmos()
    {
        //OnDrawGizmos()
        Gizmos.color = new Color(0, 255, 0, 0.1f);

        Gizmos.DrawSphere(transform.position, 5);
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if(other.gameObject.GetComponent<characterMovement>())
        {
            characterMovement temp = other.gameObject.GetComponent<characterMovement>();
            temp.SpawnPoint = this;
        }
    }
}
