using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundZone : MonoBehaviour
{

    public AudioSource Soundy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<characterMovement>() && other.gameObject.GetComponent<characterMovement>().Player1)
        {
            Soundy.Play();
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<characterMovement>() && other.gameObject.GetComponent<characterMovement>().Player1)
        {
            Soundy.Stop();
        }

    }
}
