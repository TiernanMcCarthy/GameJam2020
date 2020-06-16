using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocks : MonoBehaviour
{

    public BlockController Block;

    public bool State;
    // Start is called before the first frame update
    void Start()
    {
        Block.BlockList.Add(this);
        Block.ToggleBlocks();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
