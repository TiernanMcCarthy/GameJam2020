using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class standOnBoomerang : MonoBehaviour
{
    Rigidbody rigidBody;
    CapsuleCollider collider;
    bool isRiding;
    characterMovement movement;
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        collider = GetComponent<CapsuleCollider>();
        isRiding = false;
    }

    // Update is called once per frame
    void Update()
    {
        movement = GetComponent<characterMovement>();
        if(movement.isMoving && isRiding == true)
        {
            rigidBody.isKinematic = false;
            this.transform.parent = null;
            collider.enabled = true;
            transform.position = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);
            rigidBody.AddForce(Vector3.up * 10);
            //rigidBody.AddForce(Vector3.up * 600);
            isRiding = false;
            Debug.Log("epic");
        }

        if(isRiding==false)
        {
            movement.GetComponent<NetworkObjectScript>().Transmit = true;
        }
    }

    public void forcedOff()
    {
        if(isRiding)
        {
            rigidBody.isKinematic = false;
            this.transform.parent = null;
            collider.enabled = true;
            //transform.position = Vector3.zero;
            transform.position = new Vector3(transform.position.x + 2 * movement.playerDirection, transform.position.y + 2, transform.position.z);
            rigidBody.AddForce(Vector3.up * 10);
            isRiding = false;
            movement.GetComponent<NetworkObjectScript>().Transmit = true;
        }
       
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "boomerangRideable")
        {
            Physics.IgnoreCollision(GetComponent<CapsuleCollider>(), other.GetComponentInParent<CapsuleCollider>());
            rigidBody.velocity = Vector3.zero;
            rigidBody.isKinematic = true;
            collider.enabled = false;
            //rigidBody.useGravity = false;
            transform.position = other.gameObject.GetComponentInParent<Transform>().position;
            this.transform.parent = other.gameObject.GetComponentInParent<Transform>();
            isRiding = true;
            movement.GetComponent<NetworkObjectScript>().Transmit = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "boomerangRideable")
        {
            //rigidBody.velocity = Vector3.zero;
            Physics.IgnoreCollision(GetComponent<CapsuleCollider>(), other.GetComponentInParent<CapsuleCollider>(), false);
            rigidBody.isKinematic = false;
            collider.enabled = true;
            //rigidBody.useGravity = false;
            //transform.position = other.gameObject.GetComponentInParent<Transform>().position;
            this.transform.parent = null;
            isRiding = false;
            movement.GetComponent<NetworkObjectScript>().Transmit = true;
        }
    }
}

