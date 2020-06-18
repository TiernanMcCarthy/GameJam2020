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

    public int OriginalShots;

    public Goal EndTarget;


    public bool Turret = false;


    List<int> FiringOrder;

    void Awake()
    {
        Turrets = new List<Turret>();
        OriginalShots = Shots;
        FiringOrder = new List<int>();
        FiringOrder.Add(0);
        FiringOrder.Add(1);
        FiringOrder.Add(1);
        FiringOrder.Add(0);
        FiringOrder.Add(2);
    }


    // Start is called before the first frame update
    void Start()
    {
        Rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Turret)
        {
            if (Firing && Shots >= 1)
            {
                if (Time.time - FireTime >= FireRate)
                {

                    // int Index = Random.Range(0, Turrets.Count);
                    // Debug.Log(Index);

                    int Index = FiringOrder[Shots - 1];
                    Turrets[Index].FireProjectile();

                    FireTime = Time.time;
                    Shots--;
                }
            }
            else if (Firing)
            {
                Debug.Log("WINNNNn");
                EndTarget.ExecuteGoal();
                Firing = false;
            }

        }
        else if (Firing)
        {
            EndTarget.ExecuteGoal();
            Firing = false;
        }

    }


    void FireGroup()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer  != 8)
        {
            if (!Firing)
            {
                Firing = true;
            }
           // Debug.Log("Im FUMING");
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer != 8)
        {
            if (!Firing)
            {
                //Firing = true;
            }

            Firing = false;
            if (Shots != 0)
            {
                Shots = OriginalShots;
            }
        }
    }



}
