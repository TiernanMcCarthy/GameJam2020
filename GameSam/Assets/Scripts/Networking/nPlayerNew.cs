[System.Serializable]
public class nPlayerNew : nMsg
{
    public SerialVector3 Position;

    public SerialVector3 Rotation;

    public int ID;

   


  public nPlayerNew()
    {
        OP = NetOP.NewPlayer;
    }
}
