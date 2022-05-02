using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        public class Owner
        {
            string m_Name;
            string m_PhoneNumber;

            public string Name
            {
                get
                {
                    return m_Name;
                }

                set
                {
                    m_Name = value;
                }
            }

            public string PhoneNumber
            { 
                get
                {
                    return m_PhoneNumber;
                }

                set
                {
                    m_PhoneNumber = value;
                }
            }
        }

        string m_Model;
        string m_LicenseNumber;
        float m_EnergyLeft;
        Wheel[] m_Wheels;
        Owner m_Owner;
        EnergySource m_EnergySource;

        public Vehicle(string i_VehicleModel, string i_LicenseNumber, Wheel[] i_Wheels, EnergySource i_EnergySource)
        {
            m_Model = i_VehicleModel;
            m_LicenseNumber = i_LicenseNumber;
            m_EnergyLeft = i_EnergySource.EnergyPercentage;
            m_Wheels = i_Wheels;
            m_Owner = null;
            m_EnergySource = i_EnergySource;
        }

        public Owner VehicleOwner
        {
            get
            {
                return m_Owner;
            }

            set
            {
                m_Owner = value;
            }
        }

        public string LicenseNumber
        {
            get
            {
                return m_LicenseNumber;
            }
        }
    }
}