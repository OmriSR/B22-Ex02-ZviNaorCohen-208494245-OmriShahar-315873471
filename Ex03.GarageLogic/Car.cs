using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    class Car : Vehicle
    {
        public enum eColor { Red, White, Green, Blue }

        string m_Color;
        short m_DoorCount;

        public Car(string i_VehicleModel, string i_LicenseNumber, EnergySource i_EnergySource, Wheel[] i_Wheels)
        {
            m_Model = i_VehicleModel;
            m_LicenseNumber = i_LicenseNumber;
            m_EnergySource = i_EnergySource;
            m_Wheels = i_Wheels;
        }

        public short DoorCount
        {
            set
            {
                m_DoorCount = value;
            }
        }

        public override void SetUniqueData(string[] UniqueData)
        {
            m_Color = UniqueData[0];
            m_DoorCount = Convert.ToInt16(UniqueData[1]);
        }

        public override string[] GetUniqeData
        {
            get
            {
                string[] UniqeDataMembers = { "car color", "number of Doors" };   //when reciving
                return UniqeDataMembers;
            }
        }
    }
}
