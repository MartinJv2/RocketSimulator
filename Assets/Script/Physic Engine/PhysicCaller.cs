using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicCaller : MonoBehaviour
{
   public PhysicEngine physicengine;
   public boolvariable isrunning;
   public void FixedUpdate()
   {
      if (isrunning.value)
      {
         physicengine.SimulateFrame(Time.fixedDeltaTime);
      }
   }
}
