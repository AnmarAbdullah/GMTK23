﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : State
{
    public State _Idle;

    public override State RunCurrentState()
    {
        // instantiate shit...
        return this;
    }
}
