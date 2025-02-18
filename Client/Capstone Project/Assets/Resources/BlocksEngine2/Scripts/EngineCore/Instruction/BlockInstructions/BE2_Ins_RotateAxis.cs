﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BE2_Ins_RotateAxis : BE2_InstructionBase, I_BE2_Instruction
{
    //protected override void OnAwake()
    //{
    //
    //}
    
    //protected override void OnStart()
    //{
    //    
    //}
    
    //protected override void OnUpdate()
    //{
    //
    //}

    I_BE2_BlockSectionHeaderInput _input0;
    string _axisString;
    I_BE2_BlockSectionHeaderInput _input1;

    //protected override void OnStop()
    //{
    //    
    //}

    public new void Function()
    {
        _input0 = Section0Inputs[0];
        _input1 = Section0Inputs[1];
        _axisString = _input0.StringValue;

        switch (_axisString)
        {
        case "X axis":
            TargetObject.Transform.Rotate(Vector3.right, _input1.FloatValue);
            break;
        case "Y axis":
            TargetObject.Transform.Rotate(Vector3.up, _input1.FloatValue);
            break;
        case "Z axis":
            TargetObject.Transform.Rotate(Vector3.forward, _input1.FloatValue);
            break;
        default:
            TargetObject.Transform.Rotate(Vector3.up, _input1.FloatValue);
            break;
        }

        ExecuteNextInstruction();
    }
}
