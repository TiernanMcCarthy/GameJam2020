using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
[System.Serializable]
public class CheckMovingObjects : MonoBehaviour
{

    public List<NetworkObjectScript> NetworkedObjects = new List<NetworkObjectScript>();

    public UpdatedObjectContainer TransmitList;


    //Undefined until needed
    public Server Serv;

    public Client Cli;

    public bool Hosting;

    float UpdateTime;

    public float UpdateRate = 0.02f;


    public int PlayerID = 0;
    // Start is called before the first frame update
    void Start()
    {
        //Work out what mode this should be functioning in
       // if (FindObjectOfType<Server>() == true)
        //{
         //   Serv = FindObjectOfType<Server>();
          //  Hosting = true;
       // }
       // else if (FindObjectOfType<Client>() == true) //Change when you're not working with both in one scene
       // {
         //   Cli = FindObjectOfType<Client>();
         //   Hosting = false;
       // }
        DontDestroyOnLoad(this);
        
        NetworkedObjects.AddRange(FindObjectsOfType<NetworkObjectScript>());
        TransmitList = new UpdatedObjectContainer();
     //UnityEngine.GameObject[] bob = SceneManager.GetActiveScene().GetRootGameObjects();

      // foreach(GameObject c in bob)
       // {
       //     if(c.GetComponent<NetworkObjectScript>()!=null)
        //    {
         //       Debug.Log("WHY");
         //       NetworkedObjects.Add(c.GetComponent<NetworkObjectScript>());
         //   }
       // }
    }
    void FixedUpdate() //Worth checking if this is the client or the server in this instance. Importantly the 
    {
        for(int i=0; i<NetworkedObjects.Count;i++) //Add objects that need to be in the transmit list
        {
           // if(NetworkedObjects[i].NetworkInstance.Transmit==true) 
           if(NetworkedObjects[i].CheckIfSendWorthWhile()==true && NetworkedObjects[i].ServerOwned==Hosting)
            {
                //  Debug.Log((NetworkedObjects[i].NetworkInstance.Transmit));

                    TransmitList.SceneObjects.Add(NetworkedObjects[i].NetworkInstance); //Possibly worth checking if the object is not already in this list
                    NetworkedObjects[i].NetworkInstance.Transmit = false; //Change this state for next time confirming data has been prepared for sending
                

            }
        }

        if(TransmitList.SceneObjects.Count!=0 && Time.time-UpdateTime>=UpdateRate) //If the transmit list has at least one object present send this information in a data packet
        {
            //Send this data to the approtiate network object and clear the list for future use


            if (Hosting)
            {
                if (Serv != null)
                {
                    Serv.SendClients(TransmitList);
                    TransmitList.SceneObjects.Clear();
                }
            }
            else
            {
                if (Cli != null)
                {
                    Cli.SendServer(TransmitList);
                    TransmitList.SceneObjects.Clear();
                }
            }
            

           /*

           if(Serv!=null)
           {
               Serv.SendClients(TransmitList);
               TransmitList.SceneObjects.Clear();
           }
           else
           {
               Cli.SendServer(TransmitList);
               TransmitList.SceneObjects.Clear();
           }*/
           UpdateTime = Time.time;

        }
    }


    private void UpdateGameObject()
    {

    }

    //Recieve all the objects from the client/Host that have changed, make a decision about whether X or Y takes priority in these matters
    public void SyncObjects(List<GameObjects> SceneObjects)
    {
        List<GameObjects> temp = SceneObjects;

     
       // for(int i=0; i<temp.Count;i++)
      //  {
            
      //  }

        
       // foreach(GameObject in NetworkedObjects)


        for(int Local =0; Local<NetworkedObjects.Count; Local++)
        {
            for(int Recieved=0; Recieved<temp.Count; Recieved++)
            {
                //if (temp[Recieved].ObjectID == NetworkedObjects[Local].NetworkInstance.ObjectID && temp[Recieved].ObjectID != PlayerID)
                if (temp[Recieved].ObjectID == NetworkedObjects[Local].NetworkInstance.ObjectID)
                {
                    NetworkedObjects[Local].transform.position = temp[Recieved].Position;
                    NetworkedObjects[Local].transform.rotation = Quaternion.Euler(temp[Recieved].RotationEuler);
                    NetworkedObjects[Local].rigid.velocity = temp[Recieved].Velocity;
                    //NetworkedObjects[Local].Player = temp[Recieved].Player;
                    //NetworkedObjects[Local].ServerOwned = temp[Recieved].ServerOwned;
                    break;
                }
            }

        }

        /*
        for(int i = 0; i<NetworkedObjects.Count; i++)
        {
            for(int l =0; l < temp.Count; l++)
            {
                if (temp[l].ObjectID == NetworkedObjects[i].NetworkInstance.ObjectID)
                {
                    NetworkedObjects[i].transform.position = temp[i].Position;
                    NetworkedObjects[i].transform.rotation = Quaternion.Euler(temp[i].RotationEuler);
                    NetworkedObjects[i].rigid.velocity = temp[i].Velocity;
                    
                }
            }
        }*/

        //for (int i = 0; i < NetworkedObjects.Count - 1; i++)
     //   {
         //   if(Sync.ObjectID==NetworkedObjects.)
       // }
    }
}
