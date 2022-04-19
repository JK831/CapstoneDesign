using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BE2_Cst_RotateYAxis : BE2_InstructionBase, I_BE2_Instruction
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

    //protected override void OnStop()
    //{
    //    
    //}

    public new void Function()
    {
        _input0 = Section0Inputs[0];
        TargetObject.Transform.Rotate(Vector3.up, _input0.FloatValue);

        ExecuteNextInstruction();
    }
}
