[System.Serializable]
public class nNewObject : nMsg
{
    public nNewObject()
    {
        OP = NetOP.NewObject;
    }
    
    SerialVector3 Position;

    SerialVector3 Rotation;

    SerialVector3 Scale;

    //SerialVector3 Velocity; //Possibly Unnecesarry

    
    int NetworkID; //If Networked then this will be greater than 0

    string ObjectName; //Define prefabs and create from this


}
