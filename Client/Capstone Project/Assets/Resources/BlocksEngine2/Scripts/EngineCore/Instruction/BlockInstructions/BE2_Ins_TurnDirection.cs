﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BE2_Ins_TurnDirection : BE2_InstructionBase, I_BE2_Instruction
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
    string _value;
    Vector3 _axis = Vector3.up;
    bool clockWise = true;

    //protected override void OnStop()
    //{
    //    
    //}

    public new void Function()
    {
        _input0 = Section0Inputs[0];
        _value = _input0.StringValue;
        if (_value == "Left")
        {
            TargetObject.Turn(!clockWise);
        }
        else if (_value == "Right")
        {
            TargetObject.Turn(clockWise);
        }

        ExecuteNextInstruction();
    }
}
