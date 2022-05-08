using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Wheel
    {
        string m_Manufacturer;
        float m_CurrentAirPressure = 0;
        float m_MaxAirPressure;

        public float MaxAirPressure
        {
            get
            {
                return m_MaxAirPressure;
            }

            set
            {
                m_MaxAirPressure = value;
            }
        }

        public string Manufacturer
        {
            get
            {
                return m_Manufacturer;
            }

            set
            {
                m_Manufacturer = value;
            }
        }

        public float CurrentAirPressure
        {
            get
            {
                return m_CurrentAirPressure;
            }

            set
            {
                m_CurrentAirPressure = value;
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

        public void InflateWheelToMaximum()
        {
            m_CurrentAirPressure = m_MaxAirPressure;
        }
    }
}
