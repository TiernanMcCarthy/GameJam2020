using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class Button : MonoBehaviour
{

    Rigidbody Rigid;

    private bool Firing = false;


    public List<Turret> Turrets;


    public float FireRate;

    private float FireTime;

    public int Shots;

    private int OriginalShots;

    public Goal EndTarget;

    void Awake()
    {
        Turrets = new List<Turret>();
        OriginalShots = Shots;
    }


    // Start is called before the first frame update
    void Start()
    {
        Rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Firing && Shots>=1)
        {
            if (Time.time - FireTime >= FireRate)
            {
                
                int Index = Random.Range(0, Turrets.Count);
                Debug.Log(Index);
                Turrets[Index].FireProjectile();

                FireTime = Time.time;
                Shots--;
            }
        }
        else if(Firing)
        {
            Debug.Log("WINNNNn");
            EndTarget.ExecuteGoal();
            Firing = false;
        }

    }


    void FireGroup()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "ground")
        {
            if(!Firing)
            {
                Firing = true;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag != "ground")
        {
            if (!Firing)
            {
                Firing = true;
            }

            Firing = false;
            if (Shots != 0)
            {
                Shots = OriginalShots;
            }
        }
    }



}
