using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicScr : MonoBehaviour
{
    // Start is called before the first frame update

    public AudioClip Intro;
    public AudioClip Loop;


    public AudioSource Player;

    bool LoopTime = false;
    void Start()
    {
        Player.clip = Intro;
        Player.Play();

        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        
        if(!Player.isPlaying)
        {
            Player.clip = Loop;
            Player.Play();
        }

    }
}
