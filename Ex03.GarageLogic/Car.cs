using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Car : Vehicle
    {
        public enum eColor { Red, White, Green, Blue }

        string m_Color;
        short m_DoorCount;

        public Car(string i_VehicleModel, string i_LicenseNumber, EnergySource i_EnergySource) : 
            base(i_VehicleModel, i_LicenseNumber, i_EnergySource)
        {
            m_Wheels = new Wheel[4];
            for(int i = 0; i < 4; i++)
            {
                m_Wheels[i] = new Wheel();
            }
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

        bool isValidDoorCount(string i_DoorCount)
        {
            bool valid = short.TryParse(i_DoorCount, out short doorCount);

            if(valid)
            {
                valid = (doorCount <= 5 && doorCount > 0);
            }

            return valid;
        }

        public override short ValidateUniqueData(string[] i_UniqueData)
        {
            short errorIndex = -1;

            if (!isValidColor(i_UniqueData[0]))
            {
                errorIndex = 0;
            }

            else if(!isValidDoorCount(i_UniqueData[1]))
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

        public override string[] GetUniqueData
        {
            get
            {
                string[] UniqueDataMembers = { "Car color: " + m_Color, "Number of Doors: " + m_DoorCount };   //when reciving
                return UniqueDataMembers;
            }
        }
    }
}
