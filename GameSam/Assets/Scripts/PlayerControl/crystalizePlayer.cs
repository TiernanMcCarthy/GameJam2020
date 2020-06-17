using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crystalizePlayer : MonoBehaviour
{
    Rigidbody rigidBody;
    public GameObject platformObject;
    Animator animator;
    bool isCrystal = false;
    public float cooldown;
    public bool canCrystalize = true;
    characterMovement movement;

    CheckMovingObjects Check;
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        movement = GetComponent<characterMovement>();

        Check = FindObjectOfType<CheckMovingObjects>();
    }

    // Update is called once per frame
    void Update()
    {
        if (movement.Player1)
        {
            if (Input.GetKeyDown(KeyCode.C) && canCrystalize)
            {
                crystalize();
                nAction act = new nAction();
                act.OP = NetOP.Action;
                if (Check.Hosting)
                {
                    Check.Serv.SendClientData(act);
                }
                else
                {
                    Check.Cli.SendServer(act);
                }
            }
        }
    }

    public void crystalize()
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
            canCrystalize = false;
            StartCoroutine(abilityCoolDown());
        }
     
    }

    IEnumerator abilityCoolDown()
    {
        yield return new WaitForSeconds(cooldown);
        canCrystalize = true;
    }
}
