using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Networking;
public class Client : MonoBehaviour
{

    public GameObject Test;
    Vector3 TestObject;


    public CheckMovingObjects checkMoving; //Object responsible for syncing objects
    public Vector3 OffsetDistance;

    private const int PortNumber = 5400;
    private const int WebSocketPort = 5401;
    private const int ByteSize = 1402; //Size of packet buffer storage
    private const int USERS = 2;
   
    public string ServerIP = "148.197.44.174"; //Only Public So It can be entered in the editor

    bool connected = false;

    private byte error; //Recieves error as a byte and compare to the codes on Unity Documentation

    private byte ReliableChannel; //Confirmation byte for Transmission Channel
    //IDs for sending data to the host
    private int hostID;
    private int connectionId;

    bool Started = false;

    public NetworkObjectScript Player;

    public SpawnPlayer spawn;

    public crystalizePlayer crystalize;

    // Start is called before the first frame update
    void Start()
    {
        IPHolder IP = FindObjectOfType<IPHolder>();
        if (IP.IPAddress != "")
        {
            
            //IPHolder IP = FindObjectOfType<IPHolder>();
            ServerIP = IP.IPAddress;
            Init();
            spawn = FindObjectOfType<SpawnPlayer>();
            SerialVector3 bob = new Vector3(0, 0, 0);

            checkMoving.Hosting = false;
            checkMoving.Cli = this;
            //Player.ServerOwned = false;
            if (new SerialVector3(0, 0, 0) == bob)
            {

            }
            DontDestroyOnLoad(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMessagePump(); //Check All Network Events in case of tranmission

    }
    private void FixedUpdate()
    {
        if (Started == true)
        {
           // CheckTestObject();
        }
    }

    void Init()
    {
        //Uniform code
        NetworkTransport.Init();
      
        ConnectionConfig cc = new ConnectionConfig();
        ReliableChannel = cc.AddChannel(QosType.Unreliable); //Unreliable for non essential like location

        HostTopology topology = new HostTopology(cc, 16);

        //Client
        hostID = NetworkTransport.AddHost(topology, 0);

        connectionId = NetworkTransport.Connect(hostID, ServerIP, PortNumber, 0, out error);

        Started = true;
    }

    public void Shutdown()
    {
        Started = false; //Stop Network function from attempting execution
        NetworkTransport.Shutdown();
    }

    void UpdateMessagePump() //Check All Network Events in case of tranmission
    { 
        if (!Started)
        {
            return;
        }
        int recHostId; //Standalone/web e.t.c
    int connectionId; //Which user
    int channelId; //Which lane

    byte[] recBuffer = new byte[ByteSize]; //recieving buffer
    int dataSize;
    byte error;

    NetworkEventType type = NetworkTransport.Receive(out recHostId, out connectionId, out channelId, recBuffer, ByteSize, out dataSize, out error);
        switch (type)
        {
            case NetworkEventType.Nothing:
                break;
            case NetworkEventType.ConnectEvent:
                connected = true;
                break;
            case NetworkEventType.DisconnectEvent:
                Debug.Log(string.Format("Disconnected", connectionId));
                connected = false;
                break;
            case NetworkEventType.DataEvent:
                BinaryFormatter formatter = new BinaryFormatter();
                MemoryStream ms = new MemoryStream(recBuffer); //designate memory from ram to load this into msg
                nMsg msg = (nMsg)formatter.Deserialize(ms);
                onData(connectionId, channelId, recHostId, msg); //Decode this data and execute appropriately
                break;

            default:
            case NetworkEventType.BroadcastEvent:
                Debug.Log("Unexpected network event type");
                break;
        }
    }
    private void onData(int ConnectionId, int channelId, int recHostId, nMsg msg)
    {
        switch (msg.OP) //Server Recieves much data than the client
        {
            case NetOP.None:
                Debug.Log("Incorrect initialisation");
                break;
            case NetOP.Position:
                n_Position Decode = (n_Position)msg;
                //Player.NetworkInstance.ObjectID = (int)Decode.Position.x;
                //Player.ID = (int)Decode.Position.x;
                //checkMoving.PlayerID = Player.ID;
                //Test.transform.position = ((n_Position)msg).Position+OffsetDistance;
                break;
            case NetOP.SyncSceneObjects:
                Debug.Log("SceneObjects");
                UpdatedObjectContainer temp = (UpdatedObjectContainer)msg;
                checkMoving.SyncObjects(temp.SceneObjects);
                break;
            case NetOP.NewPlayer:
                nPlayerNew PlayerType = (nPlayerNew)msg;
              //  spawn.Spawn(true, PlayerType.ID);
                break;
            case NetOP.Action:
                nAction ActionToggle = (nAction)msg;
                crystalize.crystalize();
                break;

        }

    }

    void SpawnObject(Vector3 SpawnPoint)
    {

        //Instantiate<FakeObjectType>()

        GameObject temp=Instantiate(GameObject.CreatePrimitive(PrimitiveType.Cube));

        temp.transform.position = SpawnPoint;
    }


    void CheckTestObject()
    {
        if(TestObject!=Test.transform.position)
        {
            n_Position pos = new n_Position();
            TestObject = Test.transform.position;
            pos.Position = TestObject;
            SendServer(pos);
        }

    }

    #region send
    public void SendServer(nMsg msg)
    {
        //Hold data before sending it
        byte[] buffer = new byte[ByteSize];

        //crush data into a byte array (packet)
        BinaryFormatter formatter = new BinaryFormatter();
        MemoryStream ms = new MemoryStream(buffer); //designate memory from ram to load this into msg
        formatter.Serialize(ms, msg);
        NetworkTransport.Send(hostID, connectionId, ReliableChannel, buffer, ByteSize, out error);
        
        //NetworkTransport.SendQueuedMessages(hostID, connectionId, out error);
    }
    #endregion

}
