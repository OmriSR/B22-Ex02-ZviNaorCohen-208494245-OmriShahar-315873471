using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    class Bimba : Vehicle
    {
        bool m_HasUnderSeatStorage;
        short m_NumberOfSeats;

        public Bimba(string i_VehicleModel, string i_LicenseNumber, EnergySource i_EnergySource) :
            base(i_VehicleModel, i_LicenseNumber, i_EnergySource)
        {
            m_Wheels = new Wheel[5];
            for (int i = 0; i < 5; i++)
            {
                m_Wheels[i] = new Wheel();
            }
            foreach (Wheel wheel in m_Wheels)
            {
                wheel.MaxAirPressure = 15;
            }
        }

        //-----------unique data check
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
            return float.TryParse(i_TrunkCapacity, out float TrunkCapacity);
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

        //----------------unique data------------------
        public override void SetUniqueData(string[] i_UniqueData)
        {
            m_HasUnderSeatStorage = i_UniqueData[0].ToLower() == "yes" ;
            m_NumberOfSeats = Convert.ToInt16(i_UniqueData[1]);
        }

        public override string[] PrintUniqueData()
        {
            //   string[] UniqeDataMembers = { "'Yes' if the truck has a cooling unit, 'No' otherwise ", "the trunck capacity" };  
            string[] uniqueDataMembers = { string.Format("Cooling unit: {0}.\n Trunk capacity: {1}", m_HasUnderSeatStorage, m_NumberOfSeats) };
            return uniqueDataMembers;
        }
        public override string[] GetUniqueData
        {
            get
            {
                //   string[] UniqeDataMembers = { "'Yes' if the truck has a cooling unit, 'No' otherwise ", "the trunck capacity" };  
                string[] uniqueDataMembers = { "storage (YES/NO): ", "number of seats: " };
                return uniqueDataMembers;
            }
        }
    }
}
