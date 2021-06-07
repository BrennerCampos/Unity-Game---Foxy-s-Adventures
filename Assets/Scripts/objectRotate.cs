using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectRotate: Command
{

    public override void Execute(Transform instance)
    {
        Trans(instance);
    }

    public override void Trans(Transform instance)
    {

            instance.transform.Rotate(45, 45, 0);

    }



}

