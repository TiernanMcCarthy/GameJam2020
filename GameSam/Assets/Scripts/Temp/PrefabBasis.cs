using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabBasis : MonoBehaviour
{
    public NetworkObjectScript Obj;

    public Rigidbody Rigid;

    public int ID;

    public Component Access;
    private void Start()
    {
        Obj = GetComponent<NetworkObjectScript>();
        Rigid.AddForce(0, 0, 15);
    }



}
