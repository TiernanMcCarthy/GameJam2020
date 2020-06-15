using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crystalizePlayer : MonoBehaviour
{
    Rigidbody rigidBody;
    public GameObject platformObject;
    Animator animator;
    bool isCrystal = false;
    characterMovement movement;
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        movement = GetComponent<characterMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            crystalize();
        }
    }

    void crystalize()
    {
        if(!isCrystal)
        {
            rigidBody.velocity = new Vector3(0, 0, 0);
            rigidBody.useGravity = false;
            rigidBody.isKinematic = true;
            platformObject.SetActive(true);
            isCrystal = true;
            animator.enabled = false;
            movement.canMove = false;
        }
        else
        {
            rigidBody.useGravity = true;
            rigidBody.isKinematic = false;
            platformObject.SetActive(false);
            isCrystal = false;
            animator.enabled = true;
            movement.canMove = true;
        }
     
    }
}
