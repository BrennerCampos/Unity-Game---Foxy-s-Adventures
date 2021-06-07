using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Base class other commands are extended from
public abstract class Command
{

    protected float transformAmount = 1.1f;

    // abstract tells that it's incomplete, extension classes are expected to finish this
    public abstract void Execute(Transform instance);


    public virtual void Trans(Transform instance)
    {

    }

    /*public virtual void Undo(Transform instance)
    {

    }*/

}
