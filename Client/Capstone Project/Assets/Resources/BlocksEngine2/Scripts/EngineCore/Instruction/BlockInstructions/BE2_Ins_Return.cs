using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BE2_Ins_Return : BE2_InstructionBase, I_BE2_Instruction
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

    public void Function()
    {
        BlocksStack.Pointer = 9999;
    }
}
