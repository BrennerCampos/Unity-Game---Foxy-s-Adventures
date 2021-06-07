using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectShrink : Command
{

    public override void Execute(Transform instance)
    {
        Trans(instance);
    }

    public override void Trans(Transform instance)
    {

        // Setting lowest 'shrink' value
        if (instance.localScale.x > 1.1f)
        {
            instance.transform.localScale -= new Vector3(transformAmount, transformAmount, 1);
        }


    }

    /*// Undo has opposite of the original command (reverse process)
    public override void Undo(Transform instance)
    {
        instance.transform.localScale += new Vector3(transformAmount, transformAmount, 1);
    }*/

}

