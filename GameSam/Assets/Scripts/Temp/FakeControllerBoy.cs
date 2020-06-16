using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeControllerBoy : MonoBehaviour
{
    Rigidbody rigid;
    float speed = 20;
    public bool Playing = false;

    public NetworkObjectScript NTWK;
    // Start is called before the first frame update
    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        DontDestroyOnLoad(this);
        NTWK = GetComponent<NetworkObjectScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Playing)
        {
            if (Input.GetAxis("Vertical") != 0)
            {
                rigid.AddForce(0, speed * Input.GetAxis("Vertical"), 0);
            }

        }
        
    }
}
