using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    abstract class Vehicle
    {
        string m_Model;
        string m_LicenseNumber;
        float m_EnergyLeft;
        Wheel m_Wheels; // I think it should be list [Zvika]

        public Vehicle(string i_VehicleModel, string i_LicenseNumber, float i_CurrentEnergyLevel, Wheel i_Wheels)
        {
            m_Model = i_VehicleModel;
            m_LicenseNumber = i_LicenseNumber;
            m_EnergyLeft = i_CurrentEnergyLevel;
            m_Wheels = i_Wheels;
        }
    }
}