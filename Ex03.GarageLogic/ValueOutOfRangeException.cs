using System;

namespace Ex03.GarageLogic
{
    public class ValueOutOfRangeException : Exception
    {
        string m_VehicelModle;
        string m_RangeIdentifier;
        float m_MaxRange;
        float m_MinRange = 0;

        public ValueOutOfRangeException(string i_Message)
        {
            Console.WriteLine(i_Message);
        }
    }
}
