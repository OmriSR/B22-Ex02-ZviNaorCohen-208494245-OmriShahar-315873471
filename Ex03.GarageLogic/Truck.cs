using System;
namespace Ex03.GarageLogic
{
    public class Truck : Vehicle
    {
        private bool m_HasCoolingUnit;
        private float m_TrunkCapacity;

        public Truck(string i_VehicleModel, string i_LicenseNumber, EnergySource i_EnergySource) :
            base(i_VehicleModel, i_LicenseNumber, i_EnergySource)
        {
            m_Wheels = new Wheel[16];

            for(int i = 0; i < 16; i++)
            {
                m_Wheels[i] = new Wheel();
            }

            foreach (Wheel wheel in m_Wheels)
            {
                wheel.MaxAirPressure = 24;
            }
        }

        bool isValidAnswer(string i_Input)
        {
            bool validInput = false;

            switch (i_Input.ToLower())
            {
                case "yes":
                case "no":
                    validInput = true;
                    break;
            }

            return validInput;
        }

        bool isValidCapacity(string i_TrunkCapacity)
        {
            return float.TryParse(i_TrunkCapacity, out float trunkCapacity);
        }

        public override short ValidateUniqueData(string[] i_UniqueData)
        {
            short errorIndex = -1;

            if (!isValidAnswer(i_UniqueData[0]))
            {
                errorIndex = 0;
            }

            else if (!isValidCapacity(i_UniqueData[1]))
            {
                errorIndex = 1;
            }

            return errorIndex;
        }

        public override void SetUniqueData(string[] i_UniqueData)
        {
            m_HasCoolingUnit = i_UniqueData[0].ToLower() == "yes";
            m_TrunkCapacity = Convert.ToSingle(i_UniqueData[1]);
        }

        public override string[] PrintUniqueData()
        {
            string[] uniqueDataMembers = { string.Format("Cooling unit: {0}.{2}Trunk capacity: {1}", m_HasCoolingUnit, m_TrunkCapacity, System.Environment.NewLine) };

            return uniqueDataMembers;
        }

        public override string[] GetUniqueData
        {
            get
            {
                string[] uniqueDataMembers = { "Cooling Unit (YES/NO): ", "Trunk Capacity: "};

                return uniqueDataMembers;
            }
        }
    }
}
