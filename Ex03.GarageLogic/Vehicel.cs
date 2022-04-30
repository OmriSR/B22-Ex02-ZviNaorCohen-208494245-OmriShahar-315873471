using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    abstract class Vehicel
    {
        string m_Modle;
        string m_LicenseNumber;
        float m_EnergyLeft;
        Wheel m_Wheels;

        public Vehicel(string i_VehicelModle, string i_LicenseNumber, float i_CurrentEnergyLevel, Wheel i_Wheels)
        {
            m_Modle = i_VehicelModle;
            m_LicenseNumber = i_LicenseNumber;
            m_EnergyLeft = i_CurrentEnergyLevel;
            m_Wheels = i_Wheels;
        }
    }
}
