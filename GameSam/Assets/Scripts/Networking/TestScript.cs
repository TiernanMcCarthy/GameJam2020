using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public NetworkObjectScript TestObject;

    public bool IsHost;


    public Server Serv;
    public Client Cli;

    // Start is called before the first frame update
    void Start()
    {
        if (IsHost == true)
        {
            Serv = gameObject.AddComponent<Server>();
        }
        else
        {
            Cli = gameObject.AddComponent<Client>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (IsHost == true)
        {
            TestObject.NetworkInstance.Transmit = true; //Just update this every frame

        }
    }
}
