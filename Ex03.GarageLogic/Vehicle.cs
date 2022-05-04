using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        protected string m_Model;
        protected  string m_LicenseNumber;
        protected  float m_CurrentEnergyPrecentage;
        protected  Wheel[] m_Wheels;
        protected  EnergySource m_EnergySource;


        public float EnergyLeftPercentage
        {
            get
            {
                return m_CurrentEnergyPrecentage;
            }

            set
            {
                m_CurrentEnergyPrecentage = value;
            }
        }

        public string LicenseNumber
        {
            get
            {
                return m_LicenseNumber;
            }
        }

        public void InflateAllWheels()
        {
            // run on wheels array and inflate all its wheels to maximum value.
        }

        public void SetDynamicData(string i_WheelManufacturer, float i_WheelCurrentPressure, float i_CurrentEnginEnergy)
        {
            foreach(Wheel wheel in m_Wheels)
            {
                wheel.Manufacturer = i_WheelManufacturer;
                wheel.CurrentAirPressure = i_WheelCurrentPressure;
            }

            m_EnergySource.CurrentEnergy = i_CurrentEnginEnergy;

            m_CurrentEnergyPrecentage = m_EnergySource.EnergyPercentage;
        }

        public abstract void SetUniqueData(string[] UniqueData);

        public abstract string[] GetUniqeData
        {
            get;
        }
    }

}