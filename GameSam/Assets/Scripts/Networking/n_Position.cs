[System.Serializable]
public class n_Position:nMsg
{
   public n_Position()
    {
        OP = NetOP.Position;
    }


    public SerialVector3 Position;

}
