using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panic : State
{
    public override State RunCurrentState()
    {
        return this;
    }
}
