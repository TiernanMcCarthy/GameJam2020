using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTest : MonoBehaviour
{

    public float speed;
    public float sensitivity;
    float jumpForce;
    public Rigidbody rig;



    float rotationamount;
    public GameObject Camera;
    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 Forward = Vectormaths.ForwardDirection(transform.rotation.eulerAngles);
        if (Input.GetAxisRaw("Vertical") != 0)
        {
            // RunnyBoi.SetBool("Running", true);
            //rig.velocity = transform.forward* speed * Input.GetAxisRaw("Vertical") * Time.deltaTime;
            //rig.velocity= transform.forward * speed * Input.GetAxisRaw("Vertical");

            transform.position += transform.forward * speed * Input.GetAxisRaw("Vertical") * Time.deltaTime;
        }

        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            Vector3 Temp = Vectormaths.VectorCrossProduct(transform.forward * Input.GetAxisRaw("Horizontal") * speed, transform.up * -1);
            //rig.velocity += Temp;
             transform.position += Temp * Time.deltaTime;
        }

        if (Input.GetAxisRaw("Mouse X") != 0)
        {
            Vector3 Euler = transform.rotation.eulerAngles;
            transform.rotation = Quaternion.Euler(Euler.x, Euler.y + Input.GetAxisRaw("Mouse X") * sensitivity, Euler.z);
            // Camera.transform.rotation = Quaternion.Euler(Camera.transform.rotation.x,transform.rotation.eulerAngles.y, Camera.transform.rotation.z);

        }

        if (Input.GetAxisRaw("Mouse Y") != 0)
        {
            Vector3 Euler = Camera.transform.rotation.eulerAngles;
            //transform.rotation = Quaternion.Euler(Euler.x+ Input.GetAxisRaw("Mouse Y") * -sensitivity,Euler.y, Euler.z);
            if (rotationamount + sensitivity * Input.GetAxisRaw("Mouse Y") > 90)
            {
                rotationamount = 90;
            }
            else if (rotationamount + sensitivity * Input.GetAxisRaw("Mouse Y") < -90)
            {
                rotationamount = -90;
            }
            else
            {
                rotationamount += sensitivity * Input.GetAxisRaw("Mouse Y");
            }

            Camera.transform.rotation = Quaternion.Euler(-rotationamount, Euler.y, Euler.z);
        }
     }
}
