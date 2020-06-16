using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : Goal
{
    public List<Blocks> BlockList;

    bool Toggle = false;
    // Start is called before the first frame update
    void Awake()
    {
        BlockList = new List<Blocks>();

    }


    
    // Update is called once per frame
    void Update()
    {

    }

    public void ToggleBlocks()
    {
        for(int i=0; i<BlockList.Count; i++)
        {
            if(BlockList[i].State==Toggle)
            {
                BlockList[i].gameObject.SetActive(false);
            }
            else
            {
                BlockList[i].gameObject.SetActive(true);
            
            }
        }
        Toggle = !Toggle;
    }

    public override void ExecuteGoal()
    {
        ToggleBlocks();
        //throw new System.NotImplementedException();

    }
}
