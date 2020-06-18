using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirToggle : Goal
{
    public windTurbine Toggle;

    bool State = false;
    public override void ExecuteGoal()
    {
        Toggle.gameObject.SetActive(State);
        State = !State;

    }

}
