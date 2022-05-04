using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    class Motorcycle : Vehicle
    {
        public enum eLicenseType { A, A1, B1, BB }

        string m_LicenseType;
        int m_EngineCapacity;

        public Motorcycle(string i_VehicleModel, string i_LicenseNumber, EnergySource i_EnergySource) :
            base(i_VehicleModel, i_LicenseNumber, i_EnergySource)
        {
            m_Wheels = new Wheel[2];

            m_Wheels[0].MaxAirPressure = m_Wheels[1].MaxAirPressure = Convert.ToSingle(31);
        }

        //-----------unique data check
        bool isValidLicenseType(string i_Color)
        {
            bool validColor = false;

            switch (i_Color.ToLower())
            {
                case "red":
                case "white":
                case "blue":
                case "green":
                    validColor = true;
                    break;
            }

            return validColor;
        }

        bool isValidEnginCapacity(string i_EnginCapacity, out int o_EnginCapacity)
        {
            return int.TryParse(i_EnginCapacity, out o_EnginCapacity);
        }

        public override short ValididateUniqueData(string[] i_UniqueData, out int o_EnginCapacity)
        {
            short errorIndex = -1;
            o_EnginCapacity = -1;

            if (!isValidLicenseType(i_UniqueData[0]))
            {
                errorIndex = 0;
            }

            else if (!isValidEnginCapacity(i_UniqueData[1],out o_EnginCapacity))
            {
                errorIndex = 1;
            }

            return errorIndex;
        }

        //----------------unique data------------------
        public override void SetUniqueData(string[] i_UniqueData)
        {
            m_LicenseType = i_UniqueData[0];
            m_EngineCapacity = Convert.ToInt16(i_UniqueData[1]);
        }

        public override string[] GetUniqeData
        {
            get
            {
                string[] UniqeDataMembers = { "license type", "engin capacity" };   //when reciving
                return UniqeDataMembers;
            }
        }
    }
}
