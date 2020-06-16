using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door:Goal
{
    public Transform EndPosition;
    Vector3 StartPostiton;
    bool Move = false;
    float StartTime;
    public float Openspeed;
    float JourneyLength;
    // Start is called before the first frame update
    void Start()
    {
        JourneyLength = Vector3.Distance(transform.position, EndPosition.position);
        StartPostiton = transform.position;
      //  Move = true;
        StartTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(Move)
        {

            float distCovered = (Time.time - StartTime) * Openspeed;

            float fractionOfJor = distCovered / JourneyLength;

            if (Vector3.Distance(transform.position, EndPosition.position) < 0.1f)
            {
                Move = false;
            }
            else
            {
                transform.position = Vector3.Lerp(StartPostiton, EndPosition.position, fractionOfJor);
            }
        }
    }

    public override void ExecuteGoal()
    {
        Move = true;
        StartPostiton = transform.position;
        JourneyLength = Vector3.Distance(transform.position, EndPosition.position);
        StartTime = Time.time;
    }
}
