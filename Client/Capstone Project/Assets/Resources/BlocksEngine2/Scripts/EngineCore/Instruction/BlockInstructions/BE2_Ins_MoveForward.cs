﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BE2_Ins_MoveForward : BE2_InstructionBase, I_BE2_Instruction
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
    float _value;

    //protected override void OnPlay()
    //{
    //    
    //}

    public new void Function()
    {
        _input0 = Section0Inputs[0];
        _value = _input0.FloatValue;
        TargetObject.Move();
        ExecuteNextInstruction();
    }
}
