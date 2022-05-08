using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Electric : EnergySource
    {
        public Electric(float i_MaxHours) : base(i_MaxHours) { }
    }
}
