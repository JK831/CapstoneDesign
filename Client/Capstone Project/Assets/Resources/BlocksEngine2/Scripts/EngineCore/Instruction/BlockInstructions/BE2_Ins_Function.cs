﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BE2_Ins_Function : BE2_InstructionBase, I_BE2_Instruction
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

    //void Update()
    //{

    //}

    I_BE2_BlockSectionHeaderInput _input0;
    int _counter = 0;
    float _value;

    protected override void OnButtonStop()
    {
        _counter = 0;
        //EndLoop = false;
    }

    public override void OnStackActive()
    {
        _counter = 0;
        //EndLoop = false;
    }

    public new void Function()
    {
        _value = 1;

        if (_counter != _value)
        {
            _counter++;
            ExecuteSection(0);
        }
        else
        {
            //EndLoop = true;
            _counter = 0;
            ExecuteNextInstruction();
        }
    }
}
