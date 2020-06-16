using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformIgnoreRedPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject redPlayer;
    private void Awake()
    {
        Physics.IgnoreCollision(redPlayer.GetComponent<CapsuleCollider>(), GetComponent<BoxCollider>());
        Physics.IgnoreCollision(GetComponentInParent<CapsuleCollider>(), GetComponent<BoxCollider>());
    }
  
    // Update is called once per frame
    void Update()
    {
        
    }

   
}
