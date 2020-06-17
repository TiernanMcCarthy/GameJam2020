using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{



    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<characterMovement>())
        {
            other.gameObject.GetComponent<characterMovement>().Respawn();
        }
    }
}
