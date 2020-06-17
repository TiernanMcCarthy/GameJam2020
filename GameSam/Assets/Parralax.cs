using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parralax : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject Foreground;

    public GameObject Background;

    public GameObject Middleground;

    public Rigidbody ActivePlayer;

    public float PreviousX;

    float PreviousY;


    public float ForegroundMovementX = -0.8f;

    public float ForegroundMovementY = -0.1f;

    public float BackgroundMovementX = -0.4f;

    public float BackgroundMovementY = -0.05f;

    public float MiddlegroundX = -0.4f;

    public float MiddlegroundY = -0.05f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if(ActivePlayer!=null)
        {

            float DifferenceX = ActivePlayer.transform.position.x - PreviousX;
            float DifferenceY = ActivePlayer.transform.position.y - PreviousY;
            Foreground.transform.position = Foreground.transform.position += new Vector3(DifferenceX * ForegroundMovementX, DifferenceY*ForegroundMovementY,0);

            Background.transform.position = Background.transform.position += new Vector3(DifferenceX * BackgroundMovementX, DifferenceY * BackgroundMovementY, 0);

            Middleground.transform.position = Middleground.transform.position += new Vector3(DifferenceX * MiddlegroundX, DifferenceY * MiddlegroundY, 0);

        }
        PreviousX = ActivePlayer.transform.position.x;
        PreviousY = ActivePlayer.transform.position.y;
    }
}
