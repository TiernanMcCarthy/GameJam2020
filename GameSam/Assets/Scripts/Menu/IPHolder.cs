﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IPHolder : MonoBehaviour
{
    // Start is called before the first frame update
    public string IPAddress;


    void Start()
    {
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
