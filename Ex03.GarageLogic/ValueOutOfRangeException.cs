using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    class ValueOutOfRangeException : Exception
    {
        string m_VehicelModle;
        string m_RangeIdentifier;
        float m_MaxRange;
        float m_MinRange = 0;

        public ValueOutOfRangeException()
        {
        }
    }
}
