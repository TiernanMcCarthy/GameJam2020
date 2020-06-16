using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeObjectType : PrefabBasis
{
    // Start is called before the first frame update

    
    void Start()
    {
        ID = 0;
        Access = gameObject.GetComponent<FakeObjectType>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
