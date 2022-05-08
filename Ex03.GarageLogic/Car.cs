using System;

namespace Ex03.GarageLogic
{
    public class Car : Vehicle
    {
        private string m_Color;
        private short m_DoorCount;

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

        private bool isValidColor(string i_Color)
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

        private bool isValidDoorCount(string i_DoorCount)
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

        public override void SetUniqueData(string[] i_UniqueData)
        {
            m_Color = i_UniqueData[0];
            m_DoorCount = Convert.ToInt16(i_UniqueData[1]);
        }

        public override string[] GetUniqueData
        {
            get
            {
                string[] UniqueDataMembers = { "Car color (red/white/blue/green): ", "Number of Doors (2/3/4/5): "};   //when reciving

                return UniqueDataMembers;
            }
        }

        public override string[] PrintUniqueData()
        {
            string[] uniqueDataMembers = { string.Format("Car color: {0}.\n Number Of Doors: {1}", m_Color, m_DoorCount)};

            return uniqueDataMembers;
        }
    }
}
