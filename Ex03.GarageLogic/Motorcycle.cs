using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Motorcycle : Vehicle
    {
        public enum eLicenseType { A, A1, B1, BB }

        string m_LicenseType;
        float m_EngineCapacity;

        public Motorcycle(string i_VehicleModel, string i_LicenseNumber, EnergySource i_EnergySource) :
            base(i_VehicleModel, i_LicenseNumber, i_EnergySource)
        {
            m_Wheels = new Wheel[2];
            m_Wheels[0] = new Wheel();
            m_Wheels[1] = new Wheel();
            m_Wheels[0].MaxAirPressure = m_Wheels[1].MaxAirPressure = Convert.ToSingle(31);
        }

        //-----------unique data check
        bool isValidLicenseType(string i_License)
        {
            bool validLicense = false;

            switch (i_License.ToLower())
            {
                case "aa":
                case "a1":
                case "b1":
                case "bb":
                    validLicense = true;
                    break;
            }

            return validLicense;
        }

        bool isValidEnginCapacity(string i_EnginCapacity)
        {
            return int.TryParse(i_EnginCapacity, out _);
        }

        public override short ValididateUniqueData(string[] i_UniqueData)
        {
            short errorIndex = -1;

            if (!isValidLicenseType(i_UniqueData[0]))
            {
                errorIndex = 0;
            }

            else if (!isValidEnginCapacity(i_UniqueData[1]))
            {
                errorIndex = 1;
            }

            return errorIndex;
        }

        //----------------unique data------------------
        public override void SetUniqueData(string[] i_UniqueData)
        {
            m_LicenseType = i_UniqueData[0];
            m_EngineCapacity = Convert.ToSingle(i_UniqueData[1]);
        }

        public override string[] GetUniqeData
        {
            get
            {
                // Zvika: I need to check with Omri this implementation.
                string[] UniqeDataMembers = { "License type: " + m_LicenseType + ".", "Fuel Tank Capacity: " + m_EngineCapacity + "."};  
                //when reciving
                return UniqeDataMembers;
            }
        }
    }
}
