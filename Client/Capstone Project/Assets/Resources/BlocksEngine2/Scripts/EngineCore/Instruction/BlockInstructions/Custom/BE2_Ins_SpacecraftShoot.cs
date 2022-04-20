using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BE2_Ins_SpacecraftShoot : BE2_InstructionBase, I_BE2_Instruction
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

    //protected override void OnPlay()
    //{
    //    
    //}

    public void Function()
    {
        if (TargetObject is BE2_TargetObjectSpacecraft3D)
        {
            (TargetObject as BE2_TargetObjectSpacecraft3D).Shoot();
        }
        ExecuteNextInstruction();
    }
}
