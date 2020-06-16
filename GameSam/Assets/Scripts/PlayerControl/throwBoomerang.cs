using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class throwBoomerang : MonoBehaviour
{
    public GameObject boomerang;
    public bool canThrow;
    characterMovement moveScript;
    public standOnBoomerang tempStander;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.X) && canThrow)
        {
            Vector3 spawnPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            moveScript = GetComponent<characterMovement>();
            boomerang.GetComponent<boomerangAbility>().moveDirection = moveScript.playerDirection;
            canThrow = false;
            boomerang.transform.position = spawnPos;
            boomerang.GetComponent<boomerangAbility>().ResetFunction();
            //GameObject coolDude = Instantiate(boomerang, spawnPos, Quaternion.identity);
            //coolDude.GetComponent<boomerangAbility>().instantiatedPrefab = coolDude;
            
        }
    }

    public void jumpOffBoomerang()
    {
        tempStander.forcedOff();
        Debug.Log("cool");
    }
}
