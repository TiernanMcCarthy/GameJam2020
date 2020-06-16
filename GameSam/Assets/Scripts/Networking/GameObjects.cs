



//The Position of an object must be synced and its velocity must be synced so that its future can be extrapolated.
[System.Serializable]
public class GameObjects
{

    //Vector3s are not serialisable and therefore cannot be sent over the internet in a serial connection
    //Serial Vector3 is a custom data type that works just the same, ensure you convert a vector3 to this
    public SerialVector3 Position { set; get; }

    public SerialVector3 RotationEuler { set; get; } //Possibly better to send the Rotation as a Quaternion to prevent gimble lock. Testing will make that more obvious

    public SerialVector3 Velocity { set; get; }

    public int ObjectID { get; set; } //Unity Objects all possess an ID, if the scenes IDs are synced on start up this can be used to sync objects
    //If you add an object in runtime this must be communicated to the other connected client/host

    public bool Transmit { get; set; } //A local variable for if the object should be put in a transmit list;


    public bool Player;

    public bool ServerOwned;

    public GameObjects(SerialVector3 pos, int objectID)
    {
        Position = pos;
    }


    public GameObjects(SerialVector3 pos, int objectID,SerialVector3 V,SerialVector3 RotationE)
    {
        Position = pos;
        Velocity = V;
        RotationEuler = RotationE;
    }

    public GameObjects() { }

}
