using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMoveMode : AIMode
{
    protected override void SetDirectionToTank()
    {
        int i = 0;
        while (_tankMove.SetDirection(TrueDot.GetRandomNormolizeDirection()) == false) 
        {
            print("Rand");
            i++;
            if (i > 10) break;
        } 
        UpdateTime();
    }
}
