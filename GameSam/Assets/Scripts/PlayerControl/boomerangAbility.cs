using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boomerangAbility : MonoBehaviour
{
    public float speed;
    public bool isReturning = false;
    public bool isStationary = false;
    public bool isActive;
    public Transform playerStartPos;
    public float timer;
    public float maxTime;
    public float startReturnTime;
    public GameObject owner;
    public int moveDirection;
    public characterMovement movementScript;
    Rigidbody rigidBody;
    CapsuleCollider bmCollider;
    throwBoomerang throwScript;

    CheckMovingObjects check;

    bool CollidedThisLife = false;

    void Start()
    {
        timer = 0;
        isReturning = false;
        isStationary = false;
        playerStartPos = owner.transform;
        rigidBody = GetComponent<Rigidbody>();
        bmCollider = GetComponent<CapsuleCollider>();
        throwScript = owner.GetComponent<throwBoomerang>();
        Physics.IgnoreCollision(owner.GetComponent<CapsuleCollider>(), bmCollider);
        check = FindObjectOfType<CheckMovingObjects>();
    }

    public void ResetFunction()
    {
        
        timer = 0.0f;
        isReturning = false;
        isStationary = false;
        playerStartPos = owner.transform;
        isActive = true;
        Physics.IgnoreCollision(owner.GetComponent<CapsuleCollider>(), bmCollider);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if(isActive)
        { 
            timer += Time.deltaTime;

            if (timer > maxTime && timer <= startReturnTime)
            {
                if (!isReturning)
                {
                    isStationary = true;
                    rigidBody.velocity = Vector3.zero;
                }

            }
            if (timer > startReturnTime && !isReturning)
            {
                Debug.Log("this is happening");
                isReturning = true;
                isStationary = false;

                Physics.IgnoreCollision(owner.GetComponent<CapsuleCollider>(), bmCollider, false);
            }
            if (!isStationary)
            {
                if (!isReturning)
                {
                    rigidBody.velocity = new Vector3(moveDirection * speed, 0, 0);
                }
                else
                {

                    rigidBody.velocity = new Vector3(-1 * moveDirection * speed, 0, 0);
                }
            }

            if (timer >= maxTime * 2 + startReturnTime)
            {
                throwScript.jumpOffBoomerang();
                transform.position = new Vector3(300, 300, 0);
                isActive = false;
                throwScript.canThrow = true;
            }
        }
      
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player" && !CollidedThisLife)
        {
            if (collision.gameObject == owner)
            {
                throwScript.jumpOffBoomerang();

                if (!check.Hosting)
                {
                    n_Position Pos = new n_Position();
                    Pos.OP = NetOP.Position;
                    Pos.Position = throwScript.tempStander.transform.position;

                    check.Cli.SendServer(Pos);
                }

                rigidBody.velocity = Vector3.zero;
                transform.position = new Vector3(300, 300, 0);
                isActive = false;
                //GameObject.Destroy(instantiatedPrefab);
                throwScript.canThrow = true;
            }
            else
            {
                isReturning = true;
                Physics.IgnoreCollision(owner.GetComponent<CapsuleCollider>(), bmCollider, false);
            }
        }
        else
        {
            isReturning = true;
            Physics.IgnoreCollision(owner.GetComponent<CapsuleCollider>(), bmCollider, false);
        }
    }
}
