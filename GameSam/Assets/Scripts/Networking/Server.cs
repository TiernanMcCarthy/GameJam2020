using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
public class Server : MonoBehaviour
{


    public GameObject ClientObject;

   

    private const int PortNumber = 5400;
    private const int WebSocketPort = 5401; //Unused 
    private const int USERS = 5;
    private const int ByteSize = 1402; //Size of packet buffer storage

    private byte ReliableChannel; //Confirmation byte for Transmission Channel

    private int hostID; //IDs for communication with client
    private int webHostId;
    private int ClientID;
    List<int> Clients;

    public FakeControllerBoy Player1;
    public characterMovement Player1Proper;
    public FakeControllerBoy Player2;
    public characterMovement Player2Proper;

    public crystalizePlayer crystalize;

    private int rechost;

    //public SpawnPlayer player;

    public bool Started = false; //Ensures the game functions correctly

    CheckMovingObjects ObjectManager;

    byte error; //Error byte that can be checked with Unity documentation

    // Start is called before the first frame update
    void Start()
    {
        ObjectManager = FindObjectOfType<CheckMovingObjects>(); //Use this for transmission and recieving
                                                                //initialise the server
        Clients = new List<int>();
        DontDestroyOnLoad(this);
        Player2.NTWK.ServerOwned = false;
        IPHolder IP = FindObjectOfType<IPHolder>();
        if (IP != null)
        {
            if (IP.IPAddress == "")
            {
                //Player1.Playing = true;
                Player1Proper.Player1 = true;
                Player2Proper.Player1 = false;
                Player1.NTWK.ServerOwned = true;
                Player2.NTWK.ServerOwned = false;
                ObjectManager.Hosting = true;
                ObjectManager.Serv = this;


                Init();
                SpawnObject(transform.position);

            }
            else
            {
                Player2Proper.Player1 = true;
                Player1Proper.Player1 = false;
                Player2.NTWK.ServerOwned = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMessagePump(); //Check All Network Events in case of tranmission

    }




    void Init()
    {
        NetworkTransport.Init();

        ConnectionConfig cc = new ConnectionConfig();

        ReliableChannel = cc.AddChannel(QosType.Unreliable); //Reliable packet for first communication
        //Unrealiable for transport of unimportant transmission

        HostTopology topology = new HostTopology(cc, 16);


        //server 
        hostID = NetworkTransport.AddHost(topology, PortNumber, null); //Null implies any IP can join

        webHostId = NetworkTransport.AddWebsocketHost(topology, WebSocketPort, null);

        Debug.Log(string.Format("Opening connection on port {0} and webport {1}", PortNumber, WebSocketPort));

        Clients = new List<int>();

        Started = true;
    }



    public void Shutdown()
    {
        Started = false;
        NetworkTransport.Shutdown();
    }


    public void UpdateMessagePump()
    {
        if(!Started) //Don't do this stuff if you don't need to
        {
            return; 
        }

        int recHostId; //Host ID
        int connectionId; //User ID
        int channelId; //Which lane is this occuring through

        byte[] recBuffer = new byte[ByteSize]; //recieving buffer
        int dataSize;
        //byte error;
        //Decode incoming data
        NetworkEventType type = NetworkTransport.Receive(out recHostId, out connectionId, out channelId, recBuffer, ByteSize, out dataSize, out error);
        switch (type)
        {
            case NetworkEventType.Nothing:
                break;
            case NetworkEventType.ConnectEvent:
                Debug.Log(string.Format("User {0} has connected from host {1}", connectionId, recHostId));
                ClientID = connectionId;
                Clients.Add(ClientID);
                rechost = recHostId;

                //player.Spawn(true,connectionId);
                n_Position SendID = new n_Position();
                SendID.Position = new SerialVector3(connectionId, 0, 0);
                SendClient(rechost, connectionId, SendID);

                nPlayerNew playerNew = new nPlayerNew();
                playerNew.ID = connectionId;

                SendClients(playerNew);
                //nPlayerNew playerNew = new nPlayerNew();

                

                //Connect = true;
               // Connection.text = "Client Connected";
                // ConnectionConfig cc = new ConnectionConfig();
                //ReliableChannel = cc.AddChannel(QosType.UnreliableSequenced);
                break;
            case NetworkEventType.DisconnectEvent:
                Debug.Log(string.Format("User {0} has disconnected", connectionId));
               // Connection.text = "Client Disconnected";
               // Connect = false;
                break;
            case NetworkEventType.DataEvent:
                BinaryFormatter formatter = new BinaryFormatter();
                MemoryStream ms = new MemoryStream(recBuffer); //designate memory from ram to load this into msg
                nMsg msg = (nMsg)formatter.Deserialize(ms);//Decode this data and deserialise it into something recognisable

                onData(connectionId, channelId, recHostId, msg);
                break;

            default:
            case NetworkEventType.BroadcastEvent:
                Debug.Log("Unexpected network event type");
                break;
        }
    }

    private void SendClient(int recHost, int connectionID, nMsg msg) //Send the Client Data of any type as long as it is serialisable
    {
        //Hold data before sending it
        byte[] buffer = new byte[ByteSize];

        //crush data into a byte array (packet)
        BinaryFormatter formatter = new BinaryFormatter();
        MemoryStream ms = new MemoryStream(buffer); //designate memory from ram to load this into msg
        formatter.Serialize(ms, msg);
        //Standalone client

      //  if(((UpdatedObjectContainer)msg).SceneObjects.Count>1)  //It doesn't seem to be the host? 
       // {
        //    Debug.Log("I'm here");
      //  }

        NetworkTransport.Send(hostID, connectionID, ReliableChannel, buffer, ByteSize, out error);
        // NetworkTransport.SendQueuedMessages(hostID, connectionID, out error);

    }

    void SpawnObject(Vector3 SpawnPoint)
    {
        GameObject temp = Instantiate(GameObject.CreatePrimitive(PrimitiveType.Cube));

        temp.transform.position = SpawnPoint;


    }


    public void SendClients(nMsg msg) //Send the Client Data of any type as long as it is serialisable
    {
        if (Clients.Count>0)
        {
            for (int i = 0; i < Clients.Count; i++)
            {
                nMsg temp = msg;
                SendClient(rechost, Clients[0], temp);
            }
        }
    }

    //public method for sending any data of your choosing to the client
    public void SendClientData(nMsg msg)
    {
        SendClient(rechost, ClientID, msg);
    }


    #region ondata
    //Data is sent here and stored as a NetOP so it can be distinguished as the correct action
    private void onData(int ConnectionId,int channelId,int recHostId, nMsg msg)
    {
        switch (msg.OP) //Server Recieves much data than the client
        {
            case NetOP.None:
                Debug.Log("Incorrect initialisation");
                break;
            case NetOP.Position:
                Debug.Log("New Position");
                Player1Proper.transform.position= ((n_Position)msg).Position;
                Player1Proper.GetComponent<standOnBoomerang>().forcedOff();
                break;
            case NetOP.SyncSceneObjects: //recieve objects and sync unity objects of the correct ID
                ObjectManager.SyncObjects(((UpdatedObjectContainer)msg).SceneObjects);
                break;
            case NetOP.Action:
                nAction ActionToggle = (nAction)msg;
                crystalize.crystalize();
                break;
        }

    }

    #endregion

}
