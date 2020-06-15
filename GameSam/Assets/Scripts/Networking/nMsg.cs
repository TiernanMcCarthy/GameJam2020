using UnityEngine;
public static class NetOP
{
    public const int None = 0; //Unrecognised packet

    public const int Position = 1; //Position of game objects

    public const int SyncSceneObjects = 2; //Sync of Physics heavy scene objects that will be on both screens

    public const int NewObject = 3; //Creation of new object to sync

    public const int NewPlayer = 4;

    public const int Action = 5;
}


[System.Serializable]
public abstract class nMsg
{
    public byte OP { set; get; }

    public nMsg()
    {
        OP = NetOP.None;
    }
}
