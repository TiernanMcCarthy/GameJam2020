using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundZone : MonoBehaviour
{

    public AudioSource Soundy;
    public float soundPosX;
    public float playerPosX;
    public float totalRange = 28f;
    // Start is called before the first frame update
    void Start()
    {
        soundPosX = Soundy.transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<characterMovement>() && other.gameObject.GetComponent<characterMovement>().Player1)
        {
            Soundy.Play();

        }

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<characterMovement>() && other.gameObject.GetComponent<characterMovement>().Player1)
        {
            playerPosX = other.gameObject.transform.position.x;
            float temp = soundPosX - playerPosX;
            float temp2 = temp / totalRange;
            Soundy.volume = 1 - temp2;
            Debug.Log("temp1: " + temp + " temp2: " + temp2 + "volume: " + Soundy.volume);
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
