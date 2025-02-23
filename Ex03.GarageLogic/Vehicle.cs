﻿namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        protected  string m_Model;
        protected  string m_LicenseNumber;
        protected  float m_CurrentEnergyPercentage;
        protected  Wheel[] m_Wheels;
        protected  EnergySource m_EnergySource;

        protected Vehicle(string i_VehicleModel, string i_LicenseNumber, EnergySource i_EnergySource)
        {
            m_Model = i_VehicleModel;
            m_LicenseNumber = i_LicenseNumber;
            m_EnergySource = i_EnergySource;
        }
        public EnergySource EnergySource
        {
            get
            {
                return m_EnergySource;
            }
        }

        public Wheel[] Wheels
        {
            get
            {
                return m_Wheels;
            }
        }
        public string ModelName
        {
            get
            {
                return m_Model;
            }
        }

        public string LicenseNumber
        {
            get
            {
                return m_LicenseNumber;
            }
        }


        public void SetDynamicData(string i_WheelManufacturer, float i_WheelCurrentPressure, float i_CurrentEngineEnergy)
        {
            foreach(Wheel wheel in m_Wheels)
            {
                wheel.Manufacturer = i_WheelManufacturer;
                wheel.CurrentAirPressure = i_WheelCurrentPressure;
            }

            m_EnergySource.CurrentEnergy = i_CurrentEngineEnergy;
            m_CurrentEnergyPercentage = m_EnergySource.EnergyPercentage;
        }

        public abstract short ValidateUniqueData(string[] i_UniqueData);


        public abstract void SetUniqueData(string[] i_UniqueData);

        public abstract string[] GetUniqueData
        {
            get;
        }

        public abstract string[] PrintUniqueData();
    }

}