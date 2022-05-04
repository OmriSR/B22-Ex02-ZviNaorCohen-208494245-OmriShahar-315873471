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

        public Car(string i_VehicleModel, string i_LicenseNumber, EnergySource i_EnergySource) : 
            base(i_VehicleModel, i_LicenseNumber, i_EnergySource)
        {
            m_Wheels = new Wheel[4];
            foreach (Wheel wheel in m_Wheels)
            {
                wheel.MaxAirPressure = 29;
            }
        }

        public short DoorCount
        {
            set
            {
                m_DoorCount = value;
            }
        }

        //-----------unique data check
        bool isValidColor(string i_Color)
        {
            bool validColor = false;

            switch (i_Color.ToLower()) // Zvika: Maybe we should use if(afterlower == red || afterlower == white etc..). We need to check if this switch works properly.
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

        bool isValidDoorCount(string i_DoorCount, out int o_EnginCapacity)
        {
            short doorCount;
            bool valid = short.TryParse(i_DoorCount, out doorCount);

            if(valid)
            {
                valid = (doorCount <= 5 && doorCount > 0);
            }

            return valid;
        }

        public override short ValididateUniqueData(string[] i_UniqueData, out int o_DoorCount)
        {
            short errorIndex = -1;
            o_DoorCount = -1;

            if (!isValidColor(i_UniqueData[0]))
            {
                errorIndex = 0;
            }

            else if(!isValidDoorCount(i_UniqueData[1], out o_DoorCount))
            {
                errorIndex = 1;
            }

            return errorIndex;
        }

        //----------------unique data------------------
        public override void SetUniqueData(string[] i_UniqueData)
        {
            m_Color = i_UniqueData[0];
            m_DoorCount = Convert.ToInt16(i_UniqueData[1]);
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
