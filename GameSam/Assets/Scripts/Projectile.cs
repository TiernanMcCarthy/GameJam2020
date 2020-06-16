using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Start is called before the first frame update

    private Rigidbody Rigid;

    public float Speed;

    void Start()
    {
        Rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Rigid.velocity = transform.up * -1 * Speed;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<characterMovement>())
        {
            collision.gameObject.GetComponent<characterMovement>().Respawn();
        }

        Destroy(gameObject);
    }
}
