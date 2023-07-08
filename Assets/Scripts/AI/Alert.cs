using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alert : State
{
    public State _Idle;

    public State _Panic;
    public State _Attack;

    public override State RunCurrentState()
    {
        return this;
    }
}
