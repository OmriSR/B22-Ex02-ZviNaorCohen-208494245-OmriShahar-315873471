using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    class Wheel
    {
        short m_WheelsCount;
        string m_Manufacturer;
        float m_CurrentAirPressure;
        float m_MaxAirPressure;

        public Wheel(short i_NumOfWheels, string i_WheelManufaturer, float i_MaxAirPressure, float i_CurrentAirPressure)
        {
            m_WheelsCount = i_NumOfWheels;
            m_Manufacturer = i_WheelManufaturer;
            m_CurrentAirPressure = i_CurrentAirPressure;
            m_MaxAirPressure = i_MaxAirPressure;
        }

        public float CurrentAirPressure
        {
            get
            {
                return m_CurrentAirPressure;
            }
        }

        public void InflateWheel(float i_AirAmount)
        {
            if((m_CurrentAirPressure + i_AirAmount) < m_MaxAirPressure)
            {
                m_CurrentAirPressure += i_AirAmount;
            }
            else
            {
                throw new ValueOutOfRangeException();
            }
        }
    }
}
