using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class NetworkObjectScript : MonoBehaviour
{


    float TimeCheck = 0.0f;


    public GameObjects NetworkInstance;

    public Rigidbody rigid;

    public int ID; //Assign this to be distinct for each object and the same between the two scenes

    //Individual distances for determining if the object needs sending
    public float SyncDistance;

    public float SyncRotation;

    public float SyncVelocity;

    public float UpdateRate =2.0f; //Two seconds

    public bool Player = false;

    public bool Transmit = true;

    //Only transmit if the positions have changed enough to make it worthwhile
    SerialVector3 PreviousPosition, PreviousVelocity, PreviousRotation;

    private PlayerController PlayerAccess;

    public bool ServerOwned; //True implies the server owns this object, else implies the client controls this e.g. their own player 

    // Start is called before the first frame update
    void Start()
    {
        NetworkInstance.Position = transform.position;
        NetworkInstance.Velocity = new SerialVector3(0, 0, 0);
        NetworkInstance.RotationEuler = transform.rotation.eulerAngles;
        NetworkInstance.ObjectID = ID;
        NetworkInstance.Player = Player;
        NetworkInstance.ServerOwned = ServerOwned;

        PreviousPosition = transform.position;
        PreviousVelocity = new SerialVector3(0, 0, 0);
        PreviousRotation = transform.rotation.eulerAngles;

        if (Player)
            PlayerAccess = GetComponent<PlayerController>();

        rigid = GetComponent<Rigidbody>();
    }

    void UpdateClock()
    {
        TimeCheck = Time.time;
        PreviousPosition = NetworkInstance.Position;
        PreviousRotation = NetworkInstance.RotationEuler;
        PreviousVelocity = NetworkInstance.Velocity;
    }

    public bool CheckIfSendWorthWhile()
    {
        //If too different from the other then update, or possibly look at different implementations

        if (Transmit)
        {
            //If one of these parameters are met then send immedietly ? or bias it to send sooner, otherwise just update the position if there's some varience after two seconds or some other period
            if (SyncDistance <= Vector3.Distance(NetworkInstance.Position, PreviousPosition))
            {
                UpdateClock();
                return true;
            }

            else if (SyncRotation <= Vector3.Distance(NetworkInstance.RotationEuler, PreviousRotation))
            {
                UpdateClock();
                return true;
            }

            else if (SyncVelocity <= Vector3.Distance(NetworkInstance.Velocity, PreviousVelocity))
            {
                UpdateClock();
                return true;
            }

            //If a two second period has been met from when this object was last updated, update this object.
            if (Time.time - TimeCheck >= UpdateRate)
            {
                UpdateClock();
                return true;
            }
        }
        //None of these parameters have been met, ignore it
        return false;
    }


    // Update is called once per frame
    void Update()
    {
        //Most likely not worth updating on frame by frame basis, possible a fixed update to sync with the physics system
        NetworkInstance.Position = transform.position;
        if (Player && PlayerAccess!=null)
        {
            //NetworkInstance.Velocity = PlayerAccess.ImitateVelocity;
        }
        else
        {
            NetworkInstance.Velocity = rigid.velocity;
        }
        NetworkInstance.RotationEuler = transform.rotation.eulerAngles;
        //NetworkInstance.Transmit=CheckIfSendWorthWhile();
        NetworkInstance.ObjectID = ID;
       // NetworkInstance.ServerOwned = ServerOwned;
        
        //if(NetworkInstance.)

    }
}
