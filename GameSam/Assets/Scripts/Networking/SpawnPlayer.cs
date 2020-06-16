using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{

    CheckMovingObjects check;

    public NetworkObjectScript SecondaryPlayer;
    public NetworkObjectScript Player;

    // Start is called before the first frame update
    void Start()
    {
        check = FindObjectOfType<CheckMovingObjects>();
    }

    public void Spawn(bool Host,int ConnectionID)
    {
        if (ConnectionID != check.PlayerID)
        {
            if (Host)
            {
                NetworkObjectScript temp = Instantiate(SecondaryPlayer, transform, true);
                temp.ID = ConnectionID;
                temp.NetworkInstance.ObjectID = ConnectionID;
                check.NetworkedObjects.Add(temp);
            }
            else
            {
                NetworkObjectScript temp = Instantiate(Player, transform, true);
                temp.ID = ConnectionID;
                temp.NetworkInstance.ObjectID = ConnectionID;
                check.NetworkedObjects.Add(temp);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
