using System.Collections.Generic;

[System.Serializable]
public class UpdatedObjectContainer :nMsg
{
    //This contains many GameObjects in a custom data type that is serialisable. They can be referenced to their real scene counterparts with the objectID
    public List<GameObjects> SceneObjects = new List<GameObjects>();

    public UpdatedObjectContainer()
    {
        OP = NetOP.SyncSceneObjects;
    }

}
