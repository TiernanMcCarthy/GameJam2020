using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabHolder : MonoBehaviour
{
    public FakeObjectType Test;

    public List<PrefabBasis> PrefabList;

    public CheckMovingObjects Checkmoving;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Assign(int ID,GameObject SpawnedObject,CheckMovingObjects cc)
    {
        Checkmoving = cc;
        NetworkObjectScript tempy = new NetworkObjectScript();
        switch (ID)
        {

            case 0:
                tempy = SpawnedObject.AddComponent<FakeObjectType>().Obj;
                break;





        }


        Checkmoving.NetworkedObjects.Add(tempy);






    }
}
